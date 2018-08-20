using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //окружающие сктроки показывают контекст размещения метода RequireComponent()
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; //ссылка на объект, относительно которого будет происходить перемещение

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSepeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private Animator _animator;
    private float _vertSpeed;
    private ControllerColliderHit _contact; //нужно для сохранения данных о столкновении между фунциями

    private CharacterController _charController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _vertSpeed = minFall; // иницилаизируем скорость по вертикали, присваивая ей минимальную скорость падения в начале существования
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero; //начинаем с вектора (0,0,0) непрерывно добавляя компоненты движения

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0) //движение обрабатывается только при нажатии клавиш со стрелками
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed); //ограничиваем движение по диагонали той же скоростью, что и движение вдоль оси

            Quaternion tmp = target.rotation; //сохраняем начальную ориентацию, чтобы вернуться к ней после завершения работы с целевым объектом
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement); //преобразуем направление движения из локальных в глобальные координаты
            target.rotation = tmp;

            //transform.rotation = Quaternion.LookRotation(movement); //метод LookRotaion() вычисляет кватернион, смотрящий в этом направлении
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime); //Lerp = Linear interpolation - интерполяция - плавный переход от одного значения к дургому.
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);
        bool hitGround = false;
        RaycastHit hit;

        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) //проверяем, падает ли персонаж
        {
            float check = (_charController.height + _charController.radius) / 1.9f; //расстояние, с которым производится сравнение (слегка выходит за нижнюю часть капсулы)
            hitGround = hit.distance <= check;
        }

        if (hitGround) //вместо проверки свойства isGrounded - смотрим на результат бросания луча
        {
            if (Input.GetButtonDown("Jump")) //реакция на нажатие прыжка, находясь на поверхности
            {
                _vertSpeed = jumpSepeed;
            }
            else
            {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else // если персонаж не стоит на поверхности, применяем гравитацию, пока не будет достигнута предельная скорость
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }

            if (_contact != null)
            {
                _animator.SetBool("Jumping", true);
            }

            if (_charController.isGrounded) //метод бросания луча не обнаруживает поверхности, но капсула с ней соприкасается
            {
                if (Vector3.Dot(movement, _contact.normal) < 0) //реакция слегка меняется в зависимости от того, смотрит ли персонаж в сторону точки контака
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;
        movement *= Time.deltaTime; //не зависим от фрэймрэйта
        _charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) //при распознавании столкновения данные этого столкновения сохраняются в методе обратного вызова
    {
        _contact = hit;
    }
}

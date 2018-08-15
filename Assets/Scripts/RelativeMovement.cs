using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //окружающие сктроки показывают контекст размещения метода RequireComponent()
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; //ссылка на объект, относительно которого будет происходить перемещение

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    private CharacterController _charController;

    private void Start()
    {
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

        movement *= Time.deltaTime; //не зависим от фрэймрэйта
        _charController.Move(movement);
    }
}

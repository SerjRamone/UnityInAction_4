using System.Collections;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f; //расстояние с которого персонаж может активировать устройство

    private void Update()
    {
        if (Input.GetButtonDown("Fire3")) //реакция на кнопку ввода, заданную в настройках ввода в Unity
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); //метод OverlapSphere() возвращает список ближайших объектов
            foreach (Collider hitCollider in hitColliders)
            {
                hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); //метод SendMessage() пытается вызвать именованную функцию независимо от типа целевого объекта
            }
        }
    }
}

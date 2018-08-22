using System.Collections;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos; // смещение, применяемое при открывании двери

    private bool _open; // Переменная для слежения за открытым состоянием двери

    public void Operate()
    {
        if (_open) //открываем или закрываем дверь в зависимости от её состояния
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
        }

        _open = !_open;
    }
}

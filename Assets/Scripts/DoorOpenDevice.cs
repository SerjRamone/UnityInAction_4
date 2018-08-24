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

    public void Activate()
    {
        if (!_open) //открываем дверь только если она пока не открыта
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
            _open = true;
        }
    }

    public void Deactivate()
    {
        if (_open) //закрываем дверь только если она ещё не закрыта
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
            _open = false;
        }
    }
}

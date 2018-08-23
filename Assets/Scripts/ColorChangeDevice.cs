using System.Collections;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour
{
    public void Operate()
    {
        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); //RGB значение в диапазоне от 0 до 1
        GetComponent<Renderer>().material.color = random; //задаем цвет материала
    }
}

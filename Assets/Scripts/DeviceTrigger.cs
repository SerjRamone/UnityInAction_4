using System.Collections;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targets; //список целевых объектов, которые будет активровать данный триггер

    public bool requireKey;

    private void OnTriggerEnter(Collider other) //вызывается при попадание объекта в зону триггера
    {
        foreach (GameObject target in targets)
        {
            if (requireKey && Managers.Inventory.equippedItem != "Key")
            {
                return;
            }
            target.SendMessage("Activate");
        }
    }
    
    private void OnTriggerExit(Collider other) //вызывается при выходе объекта из зоны триггера
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; } //читаем откуда угодно, но записываем только в этом сценарии

    public void Startup()
    {
        Debug.Log("Inventoru manager starting...");
        status = ManagerStatus.Started;
    }
}

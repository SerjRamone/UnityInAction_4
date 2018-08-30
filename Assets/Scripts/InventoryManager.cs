using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; } //читаем откуда угодно, но записываем только в этом сценарии

    public string equippedItem { get; private set; }

    private Dictionary<string, int> _items;

    public void Startup()
    {
        Debug.Log("Inventoru manager starting...");

        _items = new Dictionary<string, int>(); //инициализация пустого словаря элементов

        status = ManagerStatus.Started;
    }

    private void DisplayItems() //вывод в консоль сообщения о текущем инвентаре
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "(" + item.Value + ")";
        }

        Debug.Log(itemDisplay);
    }

    public void AddItem(string name) //другие сценарии не могут напрямую управлять списком элементов, но могут вызывать этот метод
    {
        if (_items.ContainsKey(name)) //проверка существующих записей перед вводом новых данных
        {
            _items[name] += 1;
        } 
        else
        {
            _items[name] = 1;
        }

        DisplayItems();
    }

    public List<string> GetItemList() //возвращает список всех ключей словаря
    {
        List<string> list = new List<string>(_items.Keys);
        return list;
    }

    public int GetItemCount(string name) //возвращает количество указанных вещей в инвентаре
    {
        if (_items.ContainsKey(name))
        {
            return _items[name];
        }

        return 0;
    }

    public bool EquipItem(string name)
    {
        if (_items.ContainsKey(name) && equippedItem != name) //проверяем наличие в инвентаре указанного элемента и тот факт, что он ещё не подготовлен к использованию
        {
            equippedItem = name;
            Debug.Log("Equipped " + name);
            return true;
        }

        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }
}

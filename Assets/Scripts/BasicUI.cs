using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    private void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 100;
        int bthHeight = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemList();
        if (itemList.Count == 0) //отображаем сообщение об отсутсвии инвентаря
        {
            GUI.Box(new Rect(posX, posY, width, height), "No items");
        }

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item); //метод, загружающий ресурсы из папки Resources
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));
            posX += width + buffer; //двигаем в сторону следующую иконку
        }

        string equipped = Managers.Inventory.equippedItem;
        if (equipped != null) //отображение подготовленного элемента
        { 
            posX = Screen.width - (width + buffer);
            Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
        }

        posX = 10;
        posY += height + buffer;

        foreach (string item in itemList)
        {
            if (GUI.Button(new Rect(posX, posY, width, bthHeight), "Equip " + item)) //запуск вложенного кода при щелчке на кнопке
            {
                Managers.Inventory.EquipItem(item);
            }

            if (item == "Health")
            {
                if (GUI.Button(new Rect(posX, posY + bthHeight + buffer, width, bthHeight), "Use health"))
                {
                    Managers.Inventory.ConsumeItem("Health");
                    Managers.Player.ChangeHealth(25);
                }
            }

            posX += width + buffer;
        }
    }

}

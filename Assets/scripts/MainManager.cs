using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainManager 
{
    private static Messenger messenger;
    public static SceneChanger sceneChanger;
    private static InventoryManager inventory;

    public static Messenger Messenger
    {
        get
        {
            if (messenger == null) // инициализация по запросу
            {
                messenger = GameObject.FindObjectOfType<Messenger>();
            }
            return messenger;
        }
        private set
        {
            messenger = value;
        }
    }


    public static InventoryManager Inventory
    {
        get
        {
            if (inventory == null)
            {
                inventory = GameObject.FindObjectOfType<InventoryManager>();
            }
            return inventory;
        }
        private set
        {
            inventory = value;
        }
    }

}

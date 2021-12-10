using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
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
                messenger = FindObjectOfType<Messenger>();
            }
            return messenger;
        }
        private set
        {
            messenger = value;
        }
    }
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        sceneChanger = GetComponent<SceneChanger>();
    }


    public static InventoryManager Inventory
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryManager>();
            }
            return inventory;
        }
        private set
        {
            inventory = value;
        }
    }

}

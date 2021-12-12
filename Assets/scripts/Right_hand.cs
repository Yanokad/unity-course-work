using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_hand : MonoBehaviour
{
    private Transform interactObject; // объект для взаимодействия
    private Transform inHand; // объект для взаимодействия
    public Transform RightHand;
    public Transform wearPos;

    private bool hasCrate;
    private bool isWeared;
    public static bool forCrate;

    [SerializeField]
    private IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation
    private bot bot;

    private TaskManager taskManager;

    // Start is called before the first frame update
    void Start()
    {
        hasCrate = false;
        forCrate = true;
        isWeared = false;
        bot = new bot();

        taskManager = new TaskManager();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThroughItem();
            hasCrate = false;
            isWeared = false;
        }
    }

    private void ThroughItem()
    {
        if (inHand != null) // если персонаж держит объект
        {
            inHand.localPosition = new Vector3(0f, 0f, 0f);
            inHand.parent = null; // отвязываем объект
            if (!inHand.gameObject.GetComponent<Rigidbody>())
                inHand.gameObject.AddComponent<Rigidbody>();
            inHand.GetComponent<Rigidbody>().useGravity = true;
            inHand.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(ReadyToTake()); // запускаем корутину
            playerIK.StopCarry(); // oстанавливаем IK-анимацию
            inHand.gameObject.tag = "itemToCarry";
        }
    }

    private IEnumerator ReadyToTake()
    {
        yield return 2; // ждем один кадр
        inHand = null; // обнуляем ссылку
    }

    private void OnTriggerEnter(Collider other) // рука попадает в триггер
    {
        // если у триггера один из этих тегов
        if (other.CompareTag("item") || other.CompareTag("itemToCarry"))
        {
            interactObject = other.transform; // записываем объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту
            if (other.gameObject.name == "записка" || other.CompareTag("itemToCarry"))
                MainManager.Messenger.WriteMessage("Чтобы подобрать, нажмите Е");
            //IKAnimation о начале взаимодействия для запуска IK - анимации
        }
    }

    private void FixedUpdate()
    {
        CheckDistance(); // проверка дистанции с объектом
    }

    private void CheckDistance()
    {
        // если происходит взаимодействие и дистанция стала больше 2-ух
        if (interactObject != null &&
            Vector3.Distance(transform.position, interactObject.position) > 2)
        {
            interactObject = null; // обнуляем ссылку на объект
            playerIK.StopInteraction(); // прекращаем IK-анимацию
        }
    }

    private void OnCollisionEnter(Collision collision) // при коллизии с коллайдером предмета
    {
        if (collision.gameObject.CompareTag("item")) // только объекты с тегом item будем удалять
        {
            TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления
        }

        // если это объект для перемещения и в руке нет другого предмета
        else if (collision.gameObject.CompareTag("itemToCarry") && !inHand)
        {
            TakeItemInHand(collision.gameObject.transform);
        }

    }

    public void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    {
        inHand = item; // запоминаем объект для взаимодействия
        inHand.parent = transform; // делаем руку, родителем объекта
        inHand.localPosition = new Vector3(-0.1104f, 0.1989f, -0.1682f);
        inHand.localEulerAngles = new Vector3(-30.594f, -3.16f, 67.719f);
        playerIK.StopInteraction(); // останавливаем IK-анимацию
        playerIK.Carry(); // устанавливаем IK-анимацию
        hasCrate = true;
        inHand.gameObject.tag = "Untagged";
        bot.MakeDone();
        MainManager.Messenger.WriteMessage("Вы подобрали " + item.name);
        if (inHand.gameObject.GetComponent<Rigidbody>())
        {
            inHand.GetComponent<Rigidbody>().isKinematic = true;
            inHand.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void TakeItemInPocket(GameObject item)
    {
        if (forCrate)
        {
            if (hasCrate)
            {
                playerIK.StopInteraction(); // прекращение IK-анимации
                Destroy(item); // удалить объект
                if (GameObject.Find("cranes").transform.childCount == 1)
                {
                    MainManager.Messenger.WriteMessage("Уберите ящик (Е) и подойдите к окну");
                    WindowScript.isDone = true;
                }
             }
            else
            {
                 MainManager.Messenger.WriteMessage("Сначала найдите ящик");
            }
        }
        else
        {
            playerIK.StopInteraction(); // прекращение IK-анимации
            MainManager.Inventory.AddItem(item);
            if (item.name == "ключ")
                JoinAnimation2.CanOpen();
            Destroy(item); // удалить объект
            taskManager.NextTask();
        }
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_hand : MonoBehaviour
{
    private Transform interactObject; // объект для взаимодействия
    private Transform inHand; // объект для взаимодействия
    public Transform RightHand;
    public Transform wearPos;
    public Transform prefab;

    public static bool isLeft;
    private bool hasCrate;
    public static bool forCrate;

    [SerializeField]
    private IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation
    private TaskManager taskManager;

    // Start is called before the first frame update
    void Start()
    {
        hasCrate = false;
        forCrate = true;
        isLeft = false;

        taskManager = new TaskManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThroughItem();
            hasCrate = false;
        }
    }

    private void ThroughItem()
    {
        if (inHand != null) // если персонаж держит объект
        {
            if (inHand.gameObject.CompareTag("itemToCarry"))
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
            else
            {
                playerIK.StopCarry(); // oстанавливаем IK-анимацию
                Destroy(inHand.gameObject);
                Instantiate(prefab, new Vector3(-41.99f, 20.697f, -42.627f), Quaternion.identity);
                inHand = null;
                isLeft = true;
            }
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
        if (other.CompareTag("item") || other.CompareTag("itemToCarry") || other.CompareTag("rod"))
        {
            interactObject = other.transform; // записываем объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту
            if (other.gameObject.name == "записка" || other.CompareTag("itemToCarry") || other.CompareTag("rod"))
                MainManager.Messenger.WriteMessage("Чтобы подобрать, нажмите Е");
            //IKAnimation о начале взаимодействия для запуска IK - анимации
        }
        if (other.CompareTag("crates"))
        {
            if (inHand != null)
                MainManager.Messenger.WriteMessage("Нажмите Q, чтобы оставить");
            else if (isLeft)
            {
                MainManager.Messenger.WriteMessage("Нажмите F, чтобы сделать приманку");
                Catcher.isReady = true;
            }
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
        else if (collision.gameObject.CompareTag("itemToCarry") || collision.gameObject.CompareTag("rod") && !inHand)
        {
            TakeItemInHand(collision.gameObject.transform);
        }

    }

    public void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    {
        inHand = item; // запоминаем объект для взаимодействия
        inHand.parent = transform; // делаем руку, родителем объекта
        if (item.CompareTag("itemToCarry"))
        {
            inHand.localPosition = new Vector3(-0.1104f, 0.1989f, -0.1682f);
            inHand.localEulerAngles = new Vector3(-30.594f, -3.16f, 67.719f);
        }
        else
        {
            inHand.localPosition = new Vector3(-0.086f, -0.312f, -0.372f);
            inHand.localEulerAngles = new Vector3(-15.868f, 73.205f, 46.842f);
            GameObject.Find("CarryPos").transform.localPosition = new Vector3(0.092f, 1.002f, 0.188f);
            GameObject.Find("CarryPos").transform.localPosition = new Vector3(-0.01f, 1.136f, 0.27f);
        }
        playerIK.StopInteraction(); // останавливаем IK-анимацию
        playerIK.Carry(); // устанавливаем IK-анимацию
        hasCrate = true;
        inHand.gameObject.tag = "Untagged";
        // bot.MakeDone();
        if (item.CompareTag("itemToCarry"))
            MainManager.Messenger.WriteMessage("Соберите журавликов");
        else MainManager.Messenger.WriteMessage("Отнесите к ящикам");
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
                    MainManager.Messenger.WriteMessage("Уберите ящик (Q) и подойдите к окну");
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

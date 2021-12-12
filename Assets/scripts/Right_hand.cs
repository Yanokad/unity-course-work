using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_hand : MonoBehaviour
{
    private Transform interactObject; // ������ ��� ��������������
    private Transform inHand; // ������ ��� ��������������
    public Transform RightHand;
    public Transform wearPos;
    public Transform prefab;

    public static bool isLeft;
    private bool hasCrate;
    public static bool forCrate;

    [SerializeField]
    private IKAnimation playerIK; // ������ �� ��������� ������� IKAnimation
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
        if (inHand != null) // ���� �������� ������ ������
        {
            if (inHand.gameObject.CompareTag("itemToCarry"))
            {
                inHand.localPosition = new Vector3(0f, 0f, 0f);
                inHand.parent = null; // ���������� ������
                if (!inHand.gameObject.GetComponent<Rigidbody>())
                    inHand.gameObject.AddComponent<Rigidbody>();
                inHand.GetComponent<Rigidbody>().useGravity = true;
                inHand.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(ReadyToTake()); // ��������� ��������
                playerIK.StopCarry(); // o������������ IK-��������
                inHand.gameObject.tag = "itemToCarry";
            }
            else
            {
                playerIK.StopCarry(); // o������������ IK-��������
                Destroy(inHand.gameObject);
                Instantiate(prefab, new Vector3(-41.99f, 20.697f, -42.627f), Quaternion.identity);
                inHand = null;
                isLeft = true;
            }
        }
    }

    private IEnumerator ReadyToTake()
    {
        yield return 2; // ���� ���� ����
        inHand = null; // �������� ������
    }

    private void OnTriggerEnter(Collider other) // ���� �������� � �������
    {
        // ���� � �������� ���� �� ���� �����
        if (other.CompareTag("item") || other.CompareTag("itemToCarry") || other.CompareTag("rod"))
        {
            interactObject = other.transform; // ���������� ������ ��� ��������������
            playerIK.StartInteraction(other.gameObject.transform.position); // �������� �������
            if (other.gameObject.name == "�������" || other.CompareTag("itemToCarry") || other.CompareTag("rod"))
                MainManager.Messenger.WriteMessage("����� ���������, ������� �");
            //IKAnimation � ������ �������������� ��� ������� IK - ��������
        }
        if (other.CompareTag("crates"))
        {
            if (inHand != null)
                MainManager.Messenger.WriteMessage("������� Q, ����� ��������");
            else if (isLeft)
            {
                MainManager.Messenger.WriteMessage("������� F, ����� ������� ��������");
                Catcher.isReady = true;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckDistance(); // �������� ��������� � ��������
    }

    private void CheckDistance()
    {
        // ���� ���������� �������������� � ��������� ����� ������ 2-��
        if (interactObject != null &&
            Vector3.Distance(transform.position, interactObject.position) > 2)
        {
            interactObject = null; // �������� ������ �� ������
            playerIK.StopInteraction(); // ���������� IK-��������
        }
    }

    private void OnCollisionEnter(Collision collision) // ��� �������� � ����������� ��������
    {
        if (collision.gameObject.CompareTag("item")) // ������ ������� � ����� item ����� �������
        {
            TakeItemInPocket(collision.gameObject); // �������� � ����� ������ ��� ��������
        }

        // ���� ��� ������ ��� ����������� � � ���� ��� ������� ��������
        else if (collision.gameObject.CompareTag("itemToCarry") || collision.gameObject.CompareTag("rod") && !inHand)
        {
            TakeItemInHand(collision.gameObject.transform);
        }

    }

    public void TakeItemInHand(Transform item) // ������� ����� ��� �������� �������
    {
        inHand = item; // ���������� ������ ��� ��������������
        inHand.parent = transform; // ������ ����, ��������� �������
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
        playerIK.StopInteraction(); // ������������� IK-��������
        playerIK.Carry(); // ������������� IK-��������
        hasCrate = true;
        inHand.gameObject.tag = "Untagged";
        // bot.MakeDone();
        if (item.CompareTag("itemToCarry"))
            MainManager.Messenger.WriteMessage("�������� ����������");
        else MainManager.Messenger.WriteMessage("�������� � ������");
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
                playerIK.StopInteraction(); // ����������� IK-��������
                Destroy(item); // ������� ������
                if (GameObject.Find("cranes").transform.childCount == 1)
                {
                    MainManager.Messenger.WriteMessage("������� ���� (Q) � ��������� � ����");
                    WindowScript.isDone = true;
                }
             }
            else
            {
                 MainManager.Messenger.WriteMessage("������� ������� ����");
            }
        }
        else
        {
            playerIK.StopInteraction(); // ����������� IK-��������
            MainManager.Inventory.AddItem(item);
            if (item.name == "����")
                JoinAnimation2.CanOpen();
            Destroy(item); // ������� ������
            taskManager.NextTask();
        }
        
    }


}

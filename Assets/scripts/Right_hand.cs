using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_hand : MonoBehaviour
{
    private Transform interactObject; // ������ ��� ��������������
    private Transform inHand; // ������ ��� ��������������
    public Transform RightHand;
    public Transform wearPos;

    private bool hasCrate;
    private bool isWeared;
    public static bool forCrate;

    [SerializeField]
    private IKAnimation playerIK; // ������ �� ��������� ������� IKAnimation
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
        if (inHand != null) // ���� �������� ������ ������
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
    }

    private IEnumerator ReadyToTake()
    {
        yield return 2; // ���� ���� ����
        inHand = null; // �������� ������
    }

    private void OnTriggerEnter(Collider other) // ���� �������� � �������
    {
        // ���� � �������� ���� �� ���� �����
        if (other.CompareTag("item") || other.CompareTag("itemToCarry"))
        {
            interactObject = other.transform; // ���������� ������ ��� ��������������
            playerIK.StartInteraction(other.gameObject.transform.position); // �������� �������
            if (other.gameObject.name == "�������" || other.CompareTag("itemToCarry"))
                MainManager.Messenger.WriteMessage("����� ���������, ������� �");
            //IKAnimation � ������ �������������� ��� ������� IK - ��������
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
        else if (collision.gameObject.CompareTag("itemToCarry") && !inHand)
        {
            TakeItemInHand(collision.gameObject.transform);
        }

    }

    public void TakeItemInHand(Transform item) // ������� ����� ��� �������� �������
    {
        inHand = item; // ���������� ������ ��� ��������������
        inHand.parent = transform; // ������ ����, ��������� �������
        inHand.localPosition = new Vector3(-0.1104f, 0.1989f, -0.1682f);
        inHand.localEulerAngles = new Vector3(-30.594f, -3.16f, 67.719f);
        playerIK.StopInteraction(); // ������������� IK-��������
        playerIK.Carry(); // ������������� IK-��������
        hasCrate = true;
        inHand.gameObject.tag = "Untagged";
        bot.MakeDone();
        MainManager.Messenger.WriteMessage("�� ��������� " + item.name);
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
                    MainManager.Messenger.WriteMessage("������� ���� (�) � ��������� � ����");
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

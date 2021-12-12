using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation2 : MonoBehaviour
{
    public Animator doorAnimator; //������ �� �������� �����  
    public Animator cabinetAnimator1; //������ �� �������� ������ ������
    public Animator cabinetAnimator2; //������ �� �������� ���� ������  
    public Transform doorTarget; //������ �� ����� ��� ������ �������� �����
    public Transform cabinetTarget; //������ �� ����� ��� ������ �������� �����
    private Animator anim; //�������� ���������
    private bool isOpened; //���������� ��� �������� �������� �����
    private static bool canOpen; //���������� ��� �������� �������� �����

    void Start()
    {
        anim = GetComponent<Animator>();//������������� ���������
        isOpened = false;
        canOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Point" && canOpen) || (other.gameObject.name == "Point (1)" && !isOpened))
            MainManager.Messenger.WriteMessage("������� R, ����� �������");
        else if (other.gameObject.name == "Point" && !canOpen)
        {
            MainManager.Messenger.WriteMessage("�������");
        }
        else if (other.gameObject.name == "Point (1)" && isOpened)
            MainManager.Messenger.WriteMessage("������� R, ����� �������");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R) && canOpen)
        {
            this.GetComponent<Rigidbody>().isKinematic = true; //��������� isKinematic, ����� �������� �� ������������
            transform.rotation = doorTarget.rotation; //������� ��� ���������� ��������
            doorAnimator.SetTrigger("door"); //������ �������� �����
            anim.SetTrigger("door"); //������ �������� ���������
            Destroy(doorTarget.gameObject); //����������� ����� ��������'
            Right_hand.forCrate = true;
        }
        else if (other.gameObject.name == "Point (1)" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = cabinetTarget.rotation;
            if (!isOpened)
            {
                AnimateCabinet("open");
                isOpened = true;
            }
            else
            {
                AnimateCabinet("close");
                isOpened = false;
            }

        }
    }

    private void AnimateCabinet(string trigger) //�������� �����
    {
        cabinetAnimator1.SetTrigger(trigger);
        cabinetAnimator2.SetTrigger(trigger);
        anim.SetTrigger(trigger);
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = false; //���������� isKinematic
    }

    public static void CanOpen()
    {
        canOpen = true;
    }
}

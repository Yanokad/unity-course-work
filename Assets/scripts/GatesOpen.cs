using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesOpen : MonoBehaviour
{
    public Animator gatesAnimator1; //������ �� �������� ������ ������
    public Animator gatesAnimator2; //������ �� �������� ���� ������ 
    public Transform gatesTarget; //������ �� ����� ��� ������ �������� �����
    private Animator anim; //�������� ���������
    private bool isOpened; //���������� ��� �������� ��������

    void Start()
    {
        anim = GetComponent<Animator>();//������������� ���������
        isOpened = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpened && other.gameObject.name == "Point")
            MainManager.Messenger.WriteMessage("������� R, ����� �������");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true; //��������� isKinematic, ����� �������� �� ������������
            transform.rotation = gatesTarget.rotation; //������� ��� ���������� ��������
            gatesAnimator1.SetTrigger("gates"); //������ �������� �����
            gatesAnimator2.SetTrigger("gates"); //������ �������� �����
            anim.SetTrigger("open"); //������ �������� ���������
            Destroy(gatesTarget.gameObject); //����������� ����� ��������'
            Catcher.isReady = true;
            MainManager.Messenger.WriteMessage("�������� ������� ����� ������");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = false; //���������� isKinematic
    }
}

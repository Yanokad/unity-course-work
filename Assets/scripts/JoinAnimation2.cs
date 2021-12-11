using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation2 : MonoBehaviour
{
    public Animator doorAnimator;//������ �� �������� �����  
    public Animator cabinetAnimator1;//������ �� �������� �����  
    public Animator cabinetAnimator2;//������ �� �������� �����  
    public Transform doorTarget;//������ �� ����� ��� ������ ��������
    public Transform cabinetTarget;//������ �� ����� ��� ������ ��������
    private Animator anim;//�������� ���������
    private bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//�������������� ��������
        isOpened = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = doorTarget.rotation;
            doorAnimator.SetTrigger("door");//������ �������� �����
            anim.SetTrigger("door");//������ �������� ���������
            Destroy(doorTarget.gameObject);
        }

        if (other.gameObject.name == "Point (1)" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = cabinetTarget.rotation;
            if (!isOpened)
            {
                cabinetAnimator1.SetTrigger("open");//������ �������� �����
                cabinetAnimator2.SetTrigger("open");//������ �������� �����
                anim.SetTrigger("open");//������ �������� ���������
                isOpened = true;
            }
            else
            {
                cabinetAnimator1.SetTrigger("close");//������ �������� �����
                cabinetAnimator2.SetTrigger("close");//������ �������� �����
                anim.SetTrigger("close");//������ �������� ���������
                isOpened = false;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
}

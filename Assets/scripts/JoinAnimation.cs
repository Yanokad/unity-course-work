using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation : MonoBehaviour
{
    public Animator doorAnimator;//������ �� �������� �����  
    public Transform target;//������ �� ����� ��� ������ ��������
    private Quaternion newRot;//��������� �������   
    private Animator anim;//�������� ���������
    private bool secondTurn = false;
    private States state;//������� ���������   

    private Quaternion helpRot;

    private enum States//������������ ��������� ���������
    {
        wait,//��������
        turn,//�������
        walk//�����������
    }

    private void Start()
    {
        anim = GetComponent<Animator>();//�������������� ��������
        state = States.wait;     //���������� ��������� ��������
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GoToPoint();
        }
        switch (state)//����������� � ����������� �� ���������
        {
            case States.turn://��� �������� � �����
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * 2);//������������� ����� ��������� ��������� � ���������
                    if (Mathf.Abs(Mathf.Round(newRot.y * 100)) == Mathf.Abs(Mathf.Round(transform.rotation.y * 100)))//��������� ����� �������� ����������
                    {
                        transform.rotation = newRot;//��� ��������� �����������
                        if (!secondTurn)
                        {
                            state = States.walk;//����������� ��������� �� �����������
                            anim.SetBool("walk", true);      //�������� �������� ������   
                        }
                        else
                        {
                            doorAnimator.SetTrigger("door");//������ �������� �����
                            anim.SetTrigger("door");//������ �������� ���������
                            secondTurn = !secondTurn;
                            state = States.wait;
                        }
                    }
                    break;
                }
            case States.walk:
                {
                    transform.position = transform.position + transform.forward * Time.deltaTime;//���������� ��������� �����                  
                    if (Vector3.Distance(transform.position, target.position) <= 0.5)//�����
                    {
                        transform.position = new Vector3(target.position.x, transform.position.y, target.position.x);//��� ���������� ����������� ������ � ��������� �����
                        anim.SetBool("walk", false);//��������� �������� ������
                        secondTurn = true;
                        state = States.wait;
                        GoToPoint();
                    }
                    break;
                }
        }
    }
    public void GoToPoint()//������� ��� ������ ����������
    {
        if (state == States.wait)//���� ����
        {
            state = States.turn;//��������� � ��������� �������� � �����
            Vector3 relativePos = new Vector3();
            if (!secondTurn)
            {
                relativePos = target.position - transform.position;//��������� ���������� ���� ����� ����� �����������
            }
            else
            {
                Vector3 forward = target.transform.position + target.transform.forward;
                relativePos = new Vector3(forward.x, transform.position.y, forward.z) - transform.position;
            }
            helpRot = Quaternion.LookRotation(relativePos);//��������� ������ �������
            newRot = new Quaternion(helpRot.z, helpRot.x, helpRot.y, helpRot.w);
        }
    }
}

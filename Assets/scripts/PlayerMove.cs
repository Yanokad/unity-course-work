using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator anim; // ���������� ��� ������ �� ���������� ��������
    private bool move; // ���������� ��� �������� ��������� (�������� ��� ���)
    private bool run; // ���������� ��� �������� ��������� (�������� ��� ���)
    private bool walk_back; // ���������� ��� �������� ��������� (�������� ��� ���)

    private CharacterController controller; // ���������� ��� ��������� � �����������
    private float speedMove = 3f; // ���������� ��� ���������� ��������� ����������� 
    private float speedTurn = 40f; // ���������� ��� ���������� ��������� ����������� 

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        Move(verticalInput);
        Turn(horizontalInput);

        Animate(verticalInput);

        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Fire3") > 0)
        {
            Run(verticalInput);
            run = true;
        }
        else { 
            run = false;
            speedMove = 3f;
        }
        Animate_run(run); // ���� �������� ������

        if (Input.GetKeyDown("space"))
        {
            anim.SetTrigger("useranim");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("yesanim");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("noanim");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("hand");
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("take");
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("take");
        }

    }
    private void Animate(float input)
    {
        
            if ((input > 0) != move) //���� ��������� ����������
            {
                move = input > 0; // ��������� �������� ��� ���
                anim.SetBool("walk", move); // ����������� ��������
            }
        
            if ((input < 0) != walk_back) //���� ��������� ����������
            {
                walk_back = input < 0; // ��������� �������� ��� ���
                anim.SetBool("walk_back", walk_back); // ����������� ��������
            }

    }

    private void Animate_run(bool input)
    {
        anim.SetBool("run", run); // ����������� ��������
    }
 
    private void Move(float input)
    {
        // ��������� ������ ����������� �������� (-1f ��� ������� ����������)
        var movement = new Vector3(0f, -1f, input);
        movement = movement * speedMove * Time.deltaTime; // ��������� �������� � �����
                                                          // ��������� �������� � ����������� ��� ������������
        controller.Move(transform.TransformDirection(movement));
    }

    private void Run(float input)
    {
        // ��������� ������ ����������� �������� (-1f ��� ������� ����������)
        var movement = new Vector3(0f, -1f, input);
        movement = movement * speedMove * 2* Time.deltaTime; // ��������� �������� � �����
                                                          // ��������� �������� � ����������� ��� ������������
        controller.Move(transform.TransformDirection(movement));
    }

  
    private void Turn(float input)
    {
        var turn = input * speedTurn * Time.deltaTime; // ���-� �������� ��������
        transform.Rotate(0f, turn, 0f);
    }
}

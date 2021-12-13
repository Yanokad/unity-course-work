using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAdditional : MonoBehaviour
{
    private NavMeshAgent botagent; // ������ �� ����� ���������
    private Animator animbot; // ������ �� �������� ����
    [SerializeField]
    private GameObject[] points; // ������ ����� ��� ���������
                                 //������������ ��������� ����
    private GameObject player;
    private float weight;

    public GameObject destination;

    private enum states
    {
        waiting, // �������
        going, // ���
        dialog,
        following,
    }
    states state = states.waiting; // ����������� ��������� ��������

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        animbot = GetComponent<Animator>(); // ����� ��������� ���������
        botagent = GetComponent<NavMeshAgent>(); // ����� ��������� ������
        weight = 0;
        StartCoroutine(Wait()); // ��������� �������� ��������
    }

    private void FixedUpdate()
    {
        switch (state)
            {
                case (states.waiting):
                    {
                        if (PlayerNear())
                        {
                            PrepareToDialog();
                        }
                        break;
                    }

                case states.going:
                    {
                        if (PlayerNear())
                        {
                            PrepareToDialog();
                        }
                        // ���� ��������� �� ������ ���������� ������ ��������� ���������� (�.�. ��� ����� �� �������� ��� �����)
                        else if ((Vector3.Distance(transform.position, botagent.destination)) < 3)
                        {
                            StartCoroutine(Wait()); // �������� �������� ��������
                        }
                        break;
                    }
                case states.following:
                    {
                        if (PlayerNear())
                        {
                            botagent.SetDestination(player.transform.position);
                        }
                        if (!PlayerNear())
                        {
                            StartCoroutine(Wait());
                        }
                        break;
                    }
            }
    }

    private bool PlayerNear()
    {
        return (Vector3.Distance(gameObject.transform.position, player.transform.position) < 7);
    }

    private void PrepareToDialog()
    {
        botagent.SetDestination(transform.position); // �������� �����, ����� ��� ������ �� ���
        animbot.SetBool("walk", true); // ������������� �������� ������
        state = states.following; // ������������� ��������� ������� � ������� � ������� ������ �����       
    }


    private IEnumerator Wait() // �������� ��������
    {
        botagent.SetDestination(transform.position); // �������� �����, ����� ��� ������ �� ���
        animbot.SetBool("walk", false); // ������������� �������� ������
        state = states.waiting; // ���������, ��� ��� ������� � ����� ��������

        yield return new WaitForSeconds(3f); // ���� 3 ������

        botagent.SetDestination(points[Random.Range(0, points.Length)].transform.position);
        // destination � ���� ���� ����, �������� ��� �������� ���� �� ����� �����
        animbot.SetBool("walk", true); // �������� �������� ������
        state = states.going; // ���������, ��� ��� ��������� � �������� 
    }
}

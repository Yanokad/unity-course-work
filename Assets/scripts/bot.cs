using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bot : MonoBehaviour
{
    private NavMeshAgent botagent; // ссылка на агент навигации
    private Animator animbot; // ссылка на аниматор бота
    [SerializeField]
    private GameObject[] points; // массив точек для переходов
                                 //перечисление состояний бота
    private GameObject player;
    private float weight;

    private bool isDone;
    public GameObject destination;

    private enum states
    {
        waiting, // ожидает
        going, // идёт
        dialog,
        following,
    }
    states state = states.waiting; // изначальное состояние ожидания

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        animbot = GetComponent<Animator>(); // берем компонент аниматора
        botagent = GetComponent<NavMeshAgent>(); // берем компонент агента
        weight = 0;
        isDone = false;
        StartCoroutine(Wait()); // запускаем корутину ожидания
    }

    private void FixedUpdate()
    {
        if (!isDone)
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
                        // если дистанция до пункта назначения меньше заданного расстояния (т.е. бот дошел до выданной ему точки)
                        else if ((Vector3.Distance(transform.position, botagent.destination)) < 3)
                        {
                            StartCoroutine(Wait()); // вызываем корутину ожидания
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
        else
        {
            if (transform.position != destination.transform.position)
            {
                animbot.SetBool("walk", true);
                botagent.SetDestination(destination.transform.position);
                animbot.SetBool("walk", false); // останавливаем анимацию ходьбы
            }
            else
            {
                state = states.waiting; // указываем, что бот перешел в режим ожидания
            }
            
        }
    }

    private bool PlayerNear()
    {
        return (Vector3.Distance(gameObject.transform.position, player.transform.position) < 7);
    }

    private void PrepareToDialog()
    {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("walk", true); // останавливаем анимацию ходьбы
        state = states.following; // устанавливаем состояние подхода к объекту в который попали лучом       
    }


    private IEnumerator Wait() // корутина ожидания
    {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("walk", false); // останавливаем анимацию ходьбы
        state = states.waiting; // указываем, что бот перешел в режим ожидания

        yield return new WaitForSeconds(3f); // ждем 3 секунд

        botagent.SetDestination(points[Random.Range(0, points.Length)].transform.position);
        // destination – куда идти боту, передаем ему рандомно одну из наших точек
        animbot.SetBool("walk", true); // включаем анимацию ходьбы
        state = states.going; // указываем, что бот находится в движении 
    }

    public void MakeDone()
    {
        isDone = true;
    }
}

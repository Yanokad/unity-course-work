using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator anim; // переменная для ссылки на контроллер анимации
    private bool move; // переменная для хранения состояния (движемся или нет)
    private bool run; // переменная для хранения состояния (движемся или нет)
    private bool walk_back; // переменная для хранения состояния (движемся или нет)

    private CharacterController controller; // переменная для обращения к контроллеру
    private float speedMove = 3f; // переменная для управления скоростью перемещения 
    private float speedTurn = 40f; // переменная для управления скоростью перемещения 

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
        Animate_run(run); // если движемся вперед

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
        
            if ((input > 0) != move) //если состояние изменилось
            {
                move = input > 0; // сохраняем движемся или нет
                anim.SetBool("walk", move); // переключаем анимацию
            }
        
            if ((input < 0) != walk_back) //если состояние изменилось
            {
                walk_back = input < 0; // сохраняем движемся или нет
                anim.SetBool("walk_back", walk_back); // переключаем анимацию
            }

    }

    private void Animate_run(bool input)
    {
        anim.SetBool("run", run); // переключаем анимацию
    }
 
    private void Move(float input)
    {
        // вычисляем вектор направления движения (-1f для эффекта гравитации)
        var movement = new Vector3(0f, -1f, input);
        movement = movement * speedMove * Time.deltaTime; // учитываем скорость и время
                                                          // применяем смещение к контроллеру для передвижения
        controller.Move(transform.TransformDirection(movement));
    }

    private void Run(float input)
    {
        // вычисляем вектор направления движения (-1f для эффекта гравитации)
        var movement = new Vector3(0f, -1f, input);
        movement = movement * speedMove * 2* Time.deltaTime; // учитываем скорость и время
                                                          // применяем смещение к контроллеру для передвижения
        controller.Move(transform.TransformDirection(movement));
    }

  
    private void Turn(float input)
    {
        var turn = input * speedTurn * Time.deltaTime; // выч-м величину поворота
        transform.Rotate(0f, turn, 0f);
    }
}

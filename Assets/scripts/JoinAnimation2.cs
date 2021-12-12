using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation2 : MonoBehaviour
{
    public Animator doorAnimator; //ссылка на аниматор двери  
    public Animator cabinetAnimator1; //ссылка на аниматор правой дверцы
    public Animator cabinetAnimator2; //ссылка на аниматор леой дверцы  
    public Transform doorTarget; //ссылка на точку дл€ начала анимации двери
    public Transform cabinetTarget; //ссылка на точку дл€ начала анимации шкафа
    private Animator anim; //аниматор персонажа
    private bool isOpened; //переменна€ дл€ проверки открыти€ шкафа
    private static bool canOpen; //переменна€ дл€ проверки открыти€ шкафа

    void Start()
    {
        anim = GetComponent<Animator>();//инициализаци€ аниматора
        isOpened = false;
        canOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Point" && canOpen) || (other.gameObject.name == "Point (1)" && !isOpened))
            MainManager.Messenger.WriteMessage("Ќажмите R, чтобы открыть");
        else if (other.gameObject.name == "Point" && !canOpen)
        {
            MainManager.Messenger.WriteMessage("«акрыто");
        }
        else if (other.gameObject.name == "Point (1)" && isOpened)
            MainManager.Messenger.WriteMessage("Ќажмите R, чтобы закрыть");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R) && canOpen)
        {
            this.GetComponent<Rigidbody>().isKinematic = true; //включение isKinematic, чтобы персонаж не проваливалс€
            transform.rotation = doorTarget.rotation; //поворот дл€ правильной анимации
            doorAnimator.SetTrigger("door"); //запуск анимации двери
            anim.SetTrigger("door"); //запуск анимации персонажа
            Destroy(doorTarget.gameObject); //уничтожение точки анимации'
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

    private void AnimateCabinet(string trigger) //анимаци€ шкафа
    {
        cabinetAnimator1.SetTrigger(trigger);
        cabinetAnimator2.SetTrigger(trigger);
        anim.SetTrigger(trigger);
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = false; //отключение isKinematic
    }

    public static void CanOpen()
    {
        canOpen = true;
    }
}

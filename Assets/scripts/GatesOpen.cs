using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesOpen : MonoBehaviour
{
    public Animator gatesAnimator1; //ссылка на аниматор правой дверцы
    public Animator gatesAnimator2; //ссылка на аниматор леой дверцы 
    public Transform gatesTarget; //ссылка на точку для начала анимации шкафа
    private Animator anim; //аниматор персонажа
    private bool isOpened; //переменная для проверки открытия

    void Start()
    {
        anim = GetComponent<Animator>();//инициализация аниматора
        isOpened = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpened && other.gameObject.name == "Point")
            MainManager.Messenger.WriteMessage("Нажмите R, чтобы открыть");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true; //включение isKinematic, чтобы персонаж не проваливался
            transform.rotation = gatesTarget.rotation; //поворот для правильной анимации
            gatesAnimator1.SetTrigger("gates"); //запуск анимации двери
            gatesAnimator2.SetTrigger("gates"); //запуск анимации двери
            anim.SetTrigger("open"); //запуск анимации персонажа
            Destroy(gatesTarget.gameObject); //уничтожение точки анимации'
            Catcher.isReady = true;
            MainManager.Messenger.WriteMessage("Сделайте ловушку среди ящиков");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = false; //отключение isKinematic
    }
}

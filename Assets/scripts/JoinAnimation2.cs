using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation2 : MonoBehaviour
{
    public Animator doorAnimator;//ссылка на аниматор двери  
    public Animator cabinetAnimator1;//ссылка на аниматор двери  
    public Animator cabinetAnimator2;//ссылка на аниматор двери  
    public Transform doorTarget;//ссылка на точку для начала анимации
    public Transform cabinetTarget;//ссылка на точку для начала анимации
    private Animator anim;//аниматор персонажа
    private bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//инициализируем аниматор
        isOpened = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = doorTarget.rotation;
            doorAnimator.SetTrigger("door");//запуск анимации двери
            anim.SetTrigger("door");//запуск анимации персонажа
            Destroy(doorTarget.gameObject);
        }

        if (other.gameObject.name == "Point (1)" && Input.GetKeyDown(KeyCode.R))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = cabinetTarget.rotation;
            if (!isOpened)
            {
                cabinetAnimator1.SetTrigger("open");//запуск анимации двери
                cabinetAnimator2.SetTrigger("open");//запуск анимации двери
                anim.SetTrigger("open");//запуск анимации персонажа
                isOpened = true;
            }
            else
            {
                cabinetAnimator1.SetTrigger("close");//запуск анимации двери
                cabinetAnimator2.SetTrigger("close");//запуск анимации двери
                anim.SetTrigger("close");//запуск анимации персонажа
                isOpened = false;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
}

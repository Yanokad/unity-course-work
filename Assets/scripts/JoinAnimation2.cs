using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation2 : MonoBehaviour
{
    public Animator doorAnimator;//ссылка на аниматор двери  
    public Transform target;//ссылка на точку для начала анимации
    private Animator anim;//аниматор персонажа

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//инициализируем аниматор
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = target.rotation;
            doorAnimator.SetTrigger("door");//запуск анимации двери
            anim.SetTrigger("door");//запуск анимации персонажа
            Destroy(target.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class CharacterIK : MonoBehaviour
{

    private Animator anim; // переменная для ссылки на контроллер анимации

    //Our bones
    public Transform upperArm;
    public Transform forearm;
    public Transform hand;
    public Transform head;

    private Transform target;

    Quaternion upperArmStartRotation, forearmStartRotation, handStartRotation, headStartRotation;



    void Start()
    {
        anim = GetComponentInParent<Animator>();
        headStartRotation = head.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        target = other.transform;
        Quaternion rotation = Quaternion.LookRotation(target.position, Vector3.up);
        head.rotation = rotation;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    private Animator anim; //переменная для ссылки на контроллер анимации
    private bool interact; // указывает, происходит ли взаимодействие
    private bool isCarried; // указывает, происходит ли взаимодействие
    private bool isWeared; // указывает, происходит ли взаимодействие
    private Vector3 positionForIК; // позиция объекта для взаимодействия
    private float weight;
    private float carryWeight;
    public Transform carryPosRight;
    public Transform carryPosLeft;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weight = 0;
        carryWeight = 0;
        isCarried = false;
    }

    // метод подобен Update, но используется для программных анимаций

    private void OnAnimatorIK() // метод подобен Update, но используется для программных анимаций
    {
        if (interact)
        {
            // т.к. вес меняется от 0 до 1, а изначально мы задали 0,
            // то для плавного перехода руки от исходной позиции
            // достаточно плавно изменять вес ik анимации.
            if (weight < 1) weight += 0.005f;
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight); 
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
            
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForIК); //указываем куда нужно смотреть
        }
        else if (weight > 0) // добавим это условие для плавного изменения анимации при отдалении
        {
            weight -= 0.01f; // теперь нужно плавно убрать воздействие анимации
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
            
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForIК);
        }

        if (isCarried)
        {
            // т.к. вес меняется от 0 до 1, а изначально мы задали 0,
            // то для плавного перехода руки от исходной позиции
            // достаточно плавно изменять вес ik анимации.
            if (carryWeight < 1) carryWeight += 0.01f;
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, carryWeight);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, carryWeight);// заменим 1f на w
            anim.SetIKPosition(AvatarIKGoal.RightHand, carryPosRight.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, carryPosRight.rotation);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, carryPosLeft.position);
            //указываем позицию для направления левой руки
        }
        else if (carryWeight > 0) // добавим это условие для плавного изменения анимации при отдалении
        {
            carryWeight -= 0.02f; // теперь нужно плавно убрать воздействие анимации
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, carryWeight);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, carryWeight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, carryPosRight.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, carryPosRight.localRotation);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, carryPosLeft.position);
        }
        
    }


    public void StartInteraction(Vector3 pos)
    {
        positionForIК = pos;
        interact = true;
    }
    public void StopInteraction()
    {
        interact = false;
    }

    public void Carry()
    {
        isCarried = true;
    }
    public void StopCarry()
    {
        isCarried = false;
    }
    /*
    public void Wear()
    {
        isWeared = true;
    }
    public void StopWear()
    {
        isWeared = false;
    }*/
}


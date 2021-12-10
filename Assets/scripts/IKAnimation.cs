using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    private Animator anim; //���������� ��� ������ �� ���������� ��������
    private bool interact; // ���������, ���������� �� ��������������
    private bool isCarried; // ���������, ���������� �� ��������������
    private bool isWeared; // ���������, ���������� �� ��������������
    private Vector3 positionForI�; // ������� ������� ��� ��������������
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

    // ����� ������� Update, �� ������������ ��� ����������� ��������

    private void OnAnimatorIK() // ����� ������� Update, �� ������������ ��� ����������� ��������
    {
        if (interact)
        {
            // �.�. ��� �������� �� 0 �� 1, � ���������� �� ������ 0,
            // �� ��� �������� �������� ���� �� �������� �������
            // ���������� ������ �������� ��� ik ��������.
            if (weight < 1) weight += 0.005f;
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight); 
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForI�);
            
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForI�); //��������� ���� ����� ��������
        }
        else if (weight > 0) // ������� ��� ������� ��� �������� ��������� �������� ��� ���������
        {
            weight -= 0.01f; // ������ ����� ������ ������ ����������� ��������
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForI�);
            
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForI�);
        }

        if (isCarried)
        {
            // �.�. ��� �������� �� 0 �� 1, � ���������� �� ������ 0,
            // �� ��� �������� �������� ���� �� �������� �������
            // ���������� ������ �������� ��� ik ��������.
            if (carryWeight < 1) carryWeight += 0.01f;
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, carryWeight);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, carryWeight);// ������� 1f �� w
            anim.SetIKPosition(AvatarIKGoal.RightHand, carryPosRight.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, carryPosRight.rotation);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, carryPosLeft.position);
            //��������� ������� ��� ����������� ����� ����
        }
        else if (carryWeight > 0) // ������� ��� ������� ��� �������� ��������� �������� ��� ���������
        {
            carryWeight -= 0.02f; // ������ ����� ������ ������ ����������� ��������
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, carryWeight);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, carryWeight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, carryPosRight.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, carryPosRight.localRotation);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, carryPosLeft.position);
        }
        
    }


    public void StartInteraction(Vector3 pos)
    {
        positionForI� = pos;
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


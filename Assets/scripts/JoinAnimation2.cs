using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation2 : MonoBehaviour
{
    public Animator doorAnimator;//������ �� �������� �����  
    public Transform target;//������ �� ����� ��� ������ ��������
    private Animator anim;//�������� ���������

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//�������������� ��������
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Point" && Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = target.rotation;
            doorAnimator.SetTrigger("door");//������ �������� �����
            anim.SetTrigger("door");//������ �������� ���������
            Destroy(target.gameObject);
        }
    }
}

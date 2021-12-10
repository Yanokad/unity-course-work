using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjects : MonoBehaviour
{

    public GameObject objectInScene; // ��������������� ������ �� �����
    [SerializeField]
    private Image imagePlace; // ����� ��� ��������
    [SerializeField]
    public Sprite image; // ��������
    private Image borderplace; // ����� ��� �������
                               // ������ �� �������� ��� �������
    [SerializeField]
    public Sprite red; // ������� �������
    [SerializeField]
    public Sprite green; // ������� �������
    public bool State { get; set; } // ��������� �������� ��������� ��������/�� �������� ������

    private void OnEnable()
    {
        borderplace = gameObject.GetComponent<Image>();
        // ������������� ������ ��������� �� ���������� �������,
        // ������� OnEnable, � �� Start       
    }

    public void UpdateImage() // �������� �������� � ����������� �� ���������
    {
        if (State) // ���� ������ ������� (��������)
        {
            imagePlace.sprite = image; // ���������� ��������
            borderplace.sprite = green; // ������� ������� �������
        }
        else // ���� ������ ��� �� ��������
        {
            imagePlace.sprite = null; // �� ���������� ��������
            borderplace.sprite = red; // ������� ������� �������
        }
    }

}

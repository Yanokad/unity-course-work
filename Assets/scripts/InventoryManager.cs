using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Inventory; // ������ �� ������ � ���������

    [SerializeField]
    private UIObjects[] objects; //������ ��������� UI, ������������ ��������

    private void Start()
    {
        Inventory.SetActive(false); // �������� ���������
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ����������� ������� ������� �I�
        {
            Inventory.SetActive(!Inventory.activeSelf); // ����������� ��������� ���������

            // ��������� �������� � ���������, ���� ��������� ��������
            if (Inventory.activeSelf)
            {
                UpdateUI();
            }
        }
    }

    // ��������� ����� ��� ���������� ������� � ���������
    public void AddItem(GameObject objectInScene)
    {
        foreach (UIObjects obj in objects) // ������� ������ UI ��������
        {
            if (objectInScene.Equals(obj.objectInScene))
            // ���� ��� ������������ ������� ��������� � ������ ������ �� �������� � �������
            {
                obj.State = true; // �������� ������ � ������� ��� �������� (�����������)
                break; // ������� �� �����, ���� ����� ���������� ������
            }
        }

        // ���� ����� ���������� �������� ��������� ��� ������ - ��������� ���
        if (Inventory.activeSelf)
        {
            UpdateUI();
        }
    }

    private void UpdateUI() // ����� ���������� ���������
    {
        foreach (UIObjects obj in objects) // ������� ������ ��������
        {
            obj.UpdateImage(); // ��������� ������ �� ��������
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messenger : MonoBehaviour
{
    private Text message; // ������ �� �����
    private static Coroutine RunMessage; // ������ �� ���������� ��������
    public GameObject panel;

    private void Start()
    {
        // ����� ��������� ������, �.�. ����� � ������ ��������� �� ����� �������

        message = GetComponent<Text>();
        WriteMessage("������� ����"); // �������� ���� ������ ��������� ��� ������������
    }

    public void WriteMessage(string text) // ����� ��� ������� �������� � ������� ���������
    {
        // �������� � ��������� ��������, ���� ��� ��� ���� ��������
        if (RunMessage != null) StopCoroutine(RunMessage);
        this.message.text = ""; // ������� ������
        panel.SetActive(true);
                                // ������ �������� � ������� ������ ���������
        RunMessage = StartCoroutine(Message(text));
    }

    private IEnumerator Message(string message) // �������� ��� ������ ���������
    {
        this.message.text = message; // ���������� ���������
        yield return new WaitForSeconds(4f); // ���� 4 �������
        this.message.text = ""; // ������� ������
        panel.SetActive(false);
    }

}

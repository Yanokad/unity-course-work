using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void OpenNewScene() // ����� ��� ����� �����
    {
        int index = SceneManager.GetActiveScene().buildIndex; // ����� ������ ���������� �����
        index = (index == 0) ? 1 : 0; // ������ ������ � 0 �� 1 ��� � 1 �� 0
        StartCoroutine(AsyncLoad(index)); // ��������� ����������� �������� �����
    }

    public void OpenNextScene() // ����� ��� ����� �����
    {
        int index = 2; // ������ ������ � 0 �� 1 ��� � 1 �� 0
        StartCoroutine(AsyncLoad(index)); // ��������� ����������� �������� �����
    }

    private IEnumerator AsyncLoad(int index)
    {
        AsyncOperation ready = null;
        ready = SceneManager.LoadSceneAsync(index);
        while (!ready.isDone) // ���� ����� �� �����������
        {
            yield return null; // ��� ��������� ����
        }
    }

}

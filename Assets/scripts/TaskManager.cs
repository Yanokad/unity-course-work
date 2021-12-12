using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static List<Task> Tasks { get; }
    private static int _numberOfTask = 0;
    public static int NumberOfTask
    {
        get => _numberOfTask;
        set
        {
            _numberOfTask = value;
            try
            {
                MainManager.Messenger.WriteMessage(Tasks[_numberOfTask].task);
                if (Tasks[_numberOfTask].obj != "�����")
                    GameObject.Find(Tasks[_numberOfTask].obj).tag = "item";
            }
            catch { }
        }
    }

    static TaskManager()
    {
        Tasks = new List<Task>(11);
        Tasks.Add(new Task() {task = "������� ���", obj = "�������"});
       // Tasks.Add(new Task() { task = "����� ������ ��� �������� �������. ������� ��", obj = "�������" });
       // Tasks.Add(new Task() { task = "\"������ ������ � ����������. ���� �� ������ ����� �������.\" ������� ������", obj = "������" });
       // Tasks.Add(new Task() { task = "������� �����������", obj = "�����������" });
        Tasks.Add(new Task() { task = "������� ����", obj = "����" });
        Tasks.Add(new Task() { task = "�������� ����� �� ������ �����", obj = "�����" });
    }

    public void NextTask()
    {
        if (_numberOfTask + 1 != Tasks.Count)
            NumberOfTask++;
    }

    public void PrevTask()
    {
        if (_numberOfTask != 0)
            NumberOfTask--;
    }
}

public class Task
{
    public string task;
    public string obj;
}
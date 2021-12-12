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
                if (Tasks[_numberOfTask].obj != "дверь")
                    GameObject.Find(Tasks[_numberOfTask].obj).tag = "item";
            }
            catch { }
        }
    }

    static TaskManager()
    {
        Tasks = new List<Task>(11);
        Tasks.Add(new Task() {task = "Найдите еду", obj = "онигири"});
       // Tasks.Add(new Task() { task = "Сунжи должен был оставить записку. Найдите ее", obj = "записка" });
       // Tasks.Add(new Task() { task = "\"Возьми спички и колкольчик. Окно на втором этаже открыто.\" Найдите спички", obj = "спички" });
       // Tasks.Add(new Task() { task = "Найдите колокольчик", obj = "колокольчик" });
        Tasks.Add(new Task() { task = "Найдите ключ", obj = "ключ" });
        Tasks.Add(new Task() { task = "Откройте дверь на втором этаже", obj = "дверь" });
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
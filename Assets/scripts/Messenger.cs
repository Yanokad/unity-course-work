using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messenger : MonoBehaviour
{
    [SerializeField] private Text message; // ссылка на текст
    private static Coroutine RunMessage; // ссылка на запущенную корутину
    public GameObject panel;

    private void Start()
    {
        WriteMessage("Найдите ящик"); // напишите сюда первое сообщение для пользователя
    }

    public void WriteMessage(string text) // метод для запуска корутины с выводом сообщения
    {
        // проверка и остановка корутины, если она уже была запущена
        if (RunMessage != null) StopCoroutine(RunMessage);
        this.message.text = ""; // очистка строки
        panel.SetActive(true);
                                // запуск корутины с выводом нового сообщения
        RunMessage = StartCoroutine(Message(text));
    }

    private IEnumerator Message(string message) // корутина для вывода сообщений
    {
        this.message.text = message; // записываем сообщение
        yield return new WaitForSeconds(4f); // ждем 4 секунды
        this.message.text = ""; // очищаем строку
        panel.SetActive(false);
    }

}

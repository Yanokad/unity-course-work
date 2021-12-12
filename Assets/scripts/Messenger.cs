using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Messenger : MonoBehaviour
{
    [SerializeField] private Text message; // ссылка на текст
    private static Coroutine RunMessage; // ссылка на запущенную корутину
    public GameObject panel;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            MainManager.Messenger.WriteMessage("Найдите еду");
        else MainManager.Messenger.WriteMessage("Найдите удочку. Осторожно! Не приближайтесь к монстру!");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void OpenNewScene() // метод для смены сцены
    {
        int index = SceneManager.GetActiveScene().buildIndex; // берем индекс запущенной сцены
        index = (index == 0) ? 1 : 0; // меняем индекс с 0 на 1 или с 1 на 0
        StartCoroutine(AsyncLoad(index)); // запускаем асинхронную загрузку сцены
    }

    public void OpenNextScene() // метод для смены сцены
    {
        int index = 2; // меняем индекс с 0 на 1 или с 1 на 0
        StartCoroutine(AsyncLoad(index)); // запускаем асинхронную загрузку сцены
    }

    private IEnumerator AsyncLoad(int index)
    {
        AsyncOperation ready = null;
        ready = SceneManager.LoadSceneAsync(index);
        while (!ready.isDone) // пока сцена не загрузилась
        {
            yield return null; // ждём следующий кадр
        }
    }

}

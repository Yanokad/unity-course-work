using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public Transform bellprefab;
    public Transform fireprefab;
    public static bool isReady;
    private bool isDone;

    private Transform fire;

    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && Right_hand.isLeft)
        {
            if (isReady && !isDone)
            {
                Instantiate(bellprefab, new Vector3(-41.9932f, 22.147f, -42.94f), Quaternion.identity);
                fire = Instantiate(fireprefab, new Vector3(-41.363f, 22.049f, -42.685f), Quaternion.identity);

                MainManager.Messenger.WriteMessage("Бегите! Не дайте другим монстрам себя поймать");
                isReady = false;
                isDone = true;
                bot.MakeDone();
                //StartCoroutine(Destroyer()); // запускаем корутину
            }
            else if (!isReady && !isDone) MainManager.Messenger.WriteMessage("Нажмите F, чтобы сделать ловушку");
        }
    }

    private IEnumerator Destroyer()
    {
        yield return 600;// ждем один кадр
        Destroy(fire.gameObject);
    }
}

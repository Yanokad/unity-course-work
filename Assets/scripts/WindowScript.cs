using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    private bool isShown;
    public static bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        isShown = true;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
            if (isShown && !isDone)
            {
                MainManager.Messenger.WriteMessage("Уберите журавликов");
                isShown = false;
            }
            else if (isDone)
                MainManager.sceneChanger.OpenNextScene();

    }
}

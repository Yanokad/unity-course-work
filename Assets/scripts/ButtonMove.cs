using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMove : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        MainManager.sceneChanger.OpenNewScene();
    }
}

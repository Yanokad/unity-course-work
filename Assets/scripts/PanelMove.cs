using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelMove : MonoBehaviour, IPointerClickHandler,
IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform UIGameobject; // трансформ UI Панели
    private float width; // ширина панели
    private float changeX; // значение содержащее смещение панели
    private float speedPanel = -100; // скорость закрытия панели

    private enum states // перечисление состояний панели
    {
        open, close, opening, closing
    }

    private states state = states.close; // изначальное состояние закрытое

    private void Start()
    {
        // инициализируем переменную трансформа
        UIGameobject = gameObject.GetComponent<RectTransform>();
        // определение ширины панели на которую надо отодвинуться вправо
        width = UIGameobject.sizeDelta.x/10;
    }

    private void FixedUpdate()
    {
        if (state == states.closing)
        {
            float x = UIGameobject.anchoredPosition.x;
            float y = UIGameobject.anchoredPosition.y;
            x -= speedPanel;
            changeX += speedPanel;
            UIGameobject.anchoredPosition = new Vector2(x, y);
            if (changeX < width)
            {
                state = states.close;
                changeX = 0;
            }
        }
        if (state == states.opening)
        {
            float x = UIGameobject.anchoredPosition.x;
            float y = UIGameobject.anchoredPosition.y;
            x += speedPanel;
            changeX += speedPanel;
            UIGameobject.anchoredPosition = new Vector2(x, y);
            if (changeX < width)
            {
                state = states.open;
                changeX = 0;
            }
        }
    }
    //по клику на панель, открываем новую сцену
    public void OnPointerClick(PointerEventData eventData)
    {
        MainManager.sceneChanger.OpenNewScene();
    }
    //при наведении на панель мышью, открываем ее
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (state == states.close)
        {
            state = states.opening;
        }
    }
    //при отведении мыши закрываем панель
    public void OnPointerExit(PointerEventData eventData)
    {
        if (state == states.open)
        {
            state = states.closing;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Action OnFingerDown;
    public static Action OnFingerUp;

    private bool isGameStarted = false;
    [SerializeField]
    private GameObject startPanel;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
            startPanel.SetActive(false);
        }
        OnFingerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnFingerUp?.Invoke();
    }

}

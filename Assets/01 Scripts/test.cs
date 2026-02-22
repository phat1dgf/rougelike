using System;
using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameRoot.Instance.GameEventBus.Subscribe<GameEvents.SceneBegin>(Print);
    }
    private void OnDisable()
    {
        GameRoot.Instance.GameEventBus.Unsubscribe<GameEvents.SceneBegin>(Print);
    }
    private void Print(GameEvents.SceneBegin e)
    {
        Debug.Log(e.A);
    } 
}

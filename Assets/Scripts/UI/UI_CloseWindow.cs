using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_CloseWindow : MonoBehaviour
{
    private Button closeWindowBtn;

    public Button CloseWindowBtn => closeWindowBtn;

    private void Awake()
    {
        closeWindowBtn = transform
            .Find("CloseWindow_btn")
            .GetComponent<Button>();
    }

    private void Start()
    {
        closeWindowBtn
            .onClick
            .AddListener(delegate () { CloseWindow(); });
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }
}

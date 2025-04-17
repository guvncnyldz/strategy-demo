using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIProductButton : MonoBehaviour
{
    private string _id;

    [SerializeField] private Button button;
    [SerializeField] private Image productImage, unavailableImage;
    [SerializeField] private TextMeshProUGUI productName;

    public UnityAction<string> OnButtonPressed;

    void Awake()
    {
        button.onClick.AddListener(() => OnButtonPressed?.Invoke(_id));
    }

    public void Initalize(string id, string name, Sprite icon)
    {
        _id = id;

        productName.text = name;
        productImage.sprite = icon;
    }

    public void FreeImages()
    {
        productImage.transform.SetParent(transform.parent);
    }

    public void FreeTexts()
    {
        productName.transform.SetParent(transform.parent);
    }

    public void GetBackComponents()
    {
        productImage.transform.SetParent(transform);
        productName.transform.SetParent(transform);
    }
}

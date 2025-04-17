using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIBuildingButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TextMeshProUGUI buildingName;

    private BuildingSO _buildingSO;

    public UnityAction<BuildingSO> OnButtonPressed;

    void Awake()
    {
        button.onClick.AddListener(() => OnButtonPressed?.Invoke(_buildingSO));
    }

    public void Initalize(BuildingSO buildingSO)
    {
        _buildingSO = buildingSO;

        buildingImage.sprite = _buildingSO.BuildingImage;
        buildingName.text = _buildingSO.BuildingName;
    }

    public void FreeTexts()
    {
        buildingImage.transform.SetParent(transform.parent);
    }

    public void FreeImages()
    {
        buildingName.transform.SetParent(transform.parent);
    }
}

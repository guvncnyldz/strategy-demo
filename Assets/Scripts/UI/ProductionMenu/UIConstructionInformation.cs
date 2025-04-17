using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIConstructionInformation : MonoBehaviour
{
    [SerializeField] private UIFadeIn _fadeIn;
    [SerializeField] private UIFadeOut _fadeOut;
    [SerializeField] private Image _buildingImage;
    [SerializeField] private TextMeshProUGUI _buildingName, _buildingSize;
    [SerializeField] private Button _cancelButton;

    public UnityAction OnCancelPressed;

    void Awake()
    {
        _cancelButton.onClick.AddListener(() => OnCancelPressed?.Invoke());
    }

    public void Activate(BuildingSO buildingSO)
    {
        _buildingImage.sprite = buildingSO.BuildingImage;
        _buildingName.text = buildingSO.BuildingName;
        _buildingSize.text = buildingSO.GetSizeText();

        _fadeIn.Transition();
    }

    public void Close()
    {
        _fadeOut.Transition();
    }
}

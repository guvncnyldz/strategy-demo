using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInformationMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactableNameText;
    [SerializeField] private Image _interactableImage;

    [SerializeField] private UIFadeIn _fadeIn;
    [SerializeField] private UIFadeOut _fadeOut;

    [SerializeField] private Button _cancelButton;
    [SerializeField] private UIProductionPanel _productionMenu;

    private bool _isActive;
    private IInteractable _currentInteractable;

    public void Start()
    {
        Services.Get<InteractionService>().InteractionEvent += OnInteractionHandler;
        _cancelButton.onClick.AddListener(InteruptInteraction);
    }

    void OnInteractionHandler(IInteractable interactable)
    {
        if (interactable == null)
        {
            InteruptInteraction();
            return;
        }

        _currentInteractable = interactable;
        _currentInteractable.InteractionInterruptEvent += OnInteractableDestroyedHandler;
        (string name, Sprite sprite) interactableInformation = interactable.GetInformation();

        _interactableNameText.text = interactableInformation.name;
        _interactableImage.sprite = interactableInformation.sprite;

        AdjustProducts(interactable);

        _isActive = true;
        _fadeIn.Transition();
    }

    private void OnInteractableDestroyedHandler(IInteractable interactable)
    {
        interactable.InteractionInterruptEvent -= OnInteractableDestroyedHandler;

        if (_currentInteractable == interactable)
            _currentInteractable = null;

        InteruptInteraction();
    }

    void AdjustProducts(IInteractable interactable)
    {
        if (interactable is IProducible producible)
        {
            _productionMenu.gameObject.SetActive(true);
            _productionMenu.Initialize(producible);
        }
        else
            _productionMenu.gameObject.SetActive(false);
    }


    void InteruptInteraction()
    {
        if (!_isActive)
            return;

        if (_currentInteractable != null)
            _currentInteractable.InteractionInterruptEvent -= OnInteractableDestroyedHandler;

        _isActive = false;
        _fadeOut.Transition();
    }
}

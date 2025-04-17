using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProductionPanel : MonoBehaviour
{
    [SerializeField] private UIProductButton _productButtonPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private ContentSizeFitter _contentSizeFitter;

    private List<UIProductButton> productButtons;
    private IProducible _currentProducible;

    void Awake()
    {
        productButtons = new List<UIProductButton>();
    }

    public void Initialize(IProducible producible)
    {
        _currentProducible = producible;
        CreateButtons();
    }

    void CreateButtons()
    {
        _gridLayoutGroup.enabled = true;
        _contentSizeFitter.enabled = true;

        Clear();

        foreach ((string id, string name, Sprite icon) product in _currentProducible.GetProductList())
        {
            UIProductButton tempProductButton = Services.Get<PoolingService>().Instantiate(_productButtonPrefab, _content);

            tempProductButton.Initalize(product.id, product.name, product.icon);
            tempProductButton.OnButtonPressed += OnProduceButtonPressed;

            productButtons.Add(tempProductButton);
        }

        ReduceDrawCalls();
    }

    void Clear()
    {
        foreach (UIProductButton productButton in productButtons)
        {
            productButton.OnButtonPressed -= OnProduceButtonPressed;
            productButton.GetBackComponents();
            Services.Get<PoolingService>().Destroy(productButton);
        }

        productButtons.Clear();
    }

    void ReduceDrawCalls()
    {
        //This operation dramatically reduces draw calls
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return null;

            _contentSizeFitter.enabled = false;
            _gridLayoutGroup.enabled = false;

            yield return null;

            foreach (UIProductButton productButton in productButtons)
            {
                productButton.FreeImages();
            }

            foreach (UIProductButton productButton in productButtons)
            {
                productButton.FreeTexts();
            }
        }
    }

    void OnProduceButtonPressed(string id)
    {
        _currentProducible.Produce(id);
    }
}

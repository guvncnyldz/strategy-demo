using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProductionMenu : MonoBehaviour
{
    [SerializeField] private BuildingContainerSO _buildingContainerSO;
    [SerializeField] private UIBuildingButton _buildingButtonPrefab;
    [SerializeField] private UIConstructionInformation _constructionInformation;
    [SerializeField] private Transform _content;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private ContentSizeFitter _contentSizeFitter;

    private List<UIBuildingButton> buildingButtons;

    void Awake()
    {
        _constructionInformation.OnCancelPressed += CloseButton;
    }

    void Start()
    {
        buildingButtons = new List<UIBuildingButton>();

        foreach (BuildingSO buildingSO in _buildingContainerSO.buildingSOList)
        {
            UIBuildingButton tempBuildingButton = Services.Get<PoolingService>().Instantiate(_buildingButtonPrefab, _content);

            tempBuildingButton.Initalize(buildingSO);
            tempBuildingButton.OnButtonPressed += OnBuildingButtonPressedHandler;

            buildingButtons.Add(tempBuildingButton);
        }

        ReduceDrawCalls();
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

            foreach (UIBuildingButton buildingButton in buildingButtons)
            {
                buildingButton.FreeImages();
            }

            foreach (UIBuildingButton buildingButton in buildingButtons)
            {
                buildingButton.FreeTexts();
            }
        }
    }

    void OnBuildingButtonPressedHandler(BuildingSO buildingSO)
    {
        ConstructionInputState constructionInputState = new ConstructionInputState(buildingSO, ConstructedCompletedHandler);
        InputManager.Instance.SetState(constructionInputState);

        _constructionInformation.Activate(buildingSO);
    }

    void CloseButton()
    {
        InputManager.Instance.SetState(null);
    }

    void ConstructedCompletedHandler()
    {
        _constructionInformation.Close();
    }
}

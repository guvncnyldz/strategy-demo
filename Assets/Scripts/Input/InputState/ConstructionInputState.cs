using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ConstructionInputState : InputStateBase, ILeftClickable
{
    private BuildingSO _buildingSO;
    private GhostBuilding _ghostBuilding;
    private UnityAction _onConstructionCompleted;

    public ConstructionInputState(BuildingSO buildingSO, UnityAction onConstructedCompleted) : base()
    {
        _buildingSO = buildingSO;
        _onConstructionCompleted = onConstructedCompleted;
    }

    public override void EnterState()
    {
        InputManager.Instance.Subscribe(this);

        BuilderBase builder = IBuilderFactory.FactoryMethod(BuilderType.ghost).CreateBuilder(_buildingSO);
        _ghostBuilding = BuildingDirector.Build(builder, 2) as GhostBuilding;
        _ghostBuilding.Initialize(_buildingSO);
        _ghostBuilding.gameObject.SetActive(true);
    }

    public void LeftClick(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!_ghostBuilding.IsConstructable)
            return;

        Vector3 mousePosition = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        mousePosition.z = 0;

        BuilderBase builder = IBuilderFactory.FactoryMethod(BuilderType.constructed).CreateBuilder(_buildingSO);
        ConstructedBuilding constructedBuilding = BuildingDirector.Build(builder, 1) as ConstructedBuilding;

        constructedBuilding.Initialize(_buildingSO);
        constructedBuilding.transform.position = mousePosition;
        constructedBuilding.OccupyGrid(true);

        InputManager.Instance.SetState(null);
    }

    public override void UpdateState()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            _ghostBuilding.gameObject.SetActive(false);
            return;
        }

        _ghostBuilding.gameObject.SetActive(true);

        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        _ghostBuilding.transform.position = mousePosition;
    }

    public override void ExitState()
    {
        InputManager.Instance.Unsubscribe(this);
        _onConstructionCompleted?.Invoke();
        Services.Get<PoolingService>().Destroy(_ghostBuilding);
    }
}


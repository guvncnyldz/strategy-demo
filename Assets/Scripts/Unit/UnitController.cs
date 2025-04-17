using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitController : MonoBehaviour, IGridContent, IInteractable, IMoveable, IHittable, IAttackable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PathAgent _pathAgent;
    [SerializeField] private UnitFSMController _unitFSMController;
    [SerializeField] private CombatSystemBase _combatSystemBase;
    [SerializeField] private UnitAnimationController _unitAnimationController;

    public PathAgent PathAgent { get => _pathAgent; }
    public UnitFSMController UnitFSMController { get => _unitFSMController; }
    public CombatSystemBase CombatSystemBase { get => _combatSystemBase; }
    public UnitAnimationController UnitAnimationController { get => _unitAnimationController; }

    public UnityAction<IHittable> OnDeathEvent { get => _onDeathEvent; set => _onDeathEvent = value; }
    private UnityAction<IHittable> _onDeathEvent;

    private UnitSO _unitSO;

    public void Initialize(UnitSO unitSO)
    {
        _unitSO = unitSO;
        _spriteRenderer.sprite = _unitSO.UnitImage;

        _pathAgent.Initialize(this, unitSO);
        _combatSystemBase.Initialize(this, unitSO);
        _unitFSMController.Initialize(this);
    }

    public (string, Sprite) GetInformation()
    {
        return (_unitSO.UnitName, _unitSO.UnitImage);
    }

    public bool IsOcuppyShareAvailable(IGridContent newGridContent)
    {
        if (newGridContent is SpawnPoint)
            return true;

        return false;
    }

    public void Move(IGridNode target)
    {
        _unitFSMController.SetCommand(new MoveCommand(target));
    }

    public void Hit(float damage)
    {
        _unitAnimationController.DamageAnimation();
    }

    public void Attack(IHittable hittable)
    {
        _unitFSMController.SetCommand(new AttackCommand(hittable));
    }

    public IGridContent GetGridContent()
    {
        return this;
    }

    public IGridNode GetClosestNode(IGridNode gridNode)
    {
        return PathAgent.CurrentNode;
    }

    public IGridNode GetClosestNodeToBeAttacked(IAttackable attackable, IGridNode gridNode, float maximumRange)
    {
        List<IGridNode> neigbors = GridManager.Instance.GetNeighbours(PathAgent.CurrentNode, true);

        IGridNode closest = null;
        float minDistance = int.MaxValue;

        foreach (IGridNode node in neigbors)
        {
            if (node.IsOccupied(attackable.GetGridContent()))
                continue;

            float distance = GridManager.Instance.GetWorldDistance(gridNode, node);
            float distanceToMe = GridManager.Instance.GetWorldDistance(PathAgent.CurrentNode, node);

            if (distanceToMe <= maximumRange && distance < minDistance)
            {
                minDistance = distance;
                closest = node;
            }
        }

        return closest;
    }

    public void Die()
    {
        
    }
}

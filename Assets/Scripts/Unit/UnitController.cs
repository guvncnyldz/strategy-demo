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
    [SerializeField] private UnitHealthSystem _unitHealthSystem;

    public PathAgent PathAgent { get => _pathAgent; }
    public UnitFSMController UnitFSMController { get => _unitFSMController; }
    public CombatSystemBase CombatSystemBase { get => _combatSystemBase; }
    public UnitAnimationController UnitAnimationController { get => _unitAnimationController; }
    public UnitHealthSystem UnitHealthSystem { get => _unitHealthSystem; }

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
        _unitHealthSystem.Initialize(this, unitSO.HitPoint);
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
        _unitHealthSystem.Hit(damage);
    }

    public void Attack(IHittable hittable)
    {
        _unitFSMController.SetCommand(new AttackCommand(hittable));
    }

    public IGridContent GetGridContent()
    {
        return this;
    }

    public void Die()
    {
        _unitFSMController.Die();
        _pathAgent.Die();
        Services.Get<PoolingService>().Destroy(this);
    }

    public List<IGridNode> GetHitBoxes(IGridNode gridNode)
    {
        return new List<IGridNode>() { PathAgent.CurrentNode };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour, IMiddleClickable
{
    private InputAction.CallbackContext _currentContext;
    private Vector2 _lastPosition;

    [SerializeField] private float _cameraSpeed = 0.05f;

    void Start()
    {
        InputManager.Instance.Subscribe(this);
    }

    public void Move()
    {
        Vector2 currentPosition = _currentContext.ReadValue<Vector2>();
        Vector2 delta = currentPosition - _lastPosition;
        _lastPosition = currentPosition;

        float normX = delta.x / Screen.width;
        float normY = delta.y / Screen.height;

        Vector3 move = new Vector3(-normX, -normY, 0f) * _cameraSpeed;

        Vector3 targetPosition = transform.position + move;

        float gridWidth = GridManager.Instance.GridWidthWorld * 0.5f;
        float gridHeight = GridManager.Instance.GridHeightWorld * 0.5f;

        if (targetPosition.x < -gridWidth || targetPosition.x > gridWidth ||
            targetPosition.y < -gridHeight || targetPosition.y > gridHeight)
        {
            return;
        }

        transform.position = targetPosition;
    }

    void Update()
    {
        if (!_currentContext.performed)
            return;

        Move();
    }

    public void MiddleClick(InputAction.CallbackContext context)
    {
        _currentContext = context;
        _lastPosition = context.ReadValue<Vector2>();
    }

    void Oestroy()
    {
        InputManager.Instance?.Unsubscribe(this);
    }
}

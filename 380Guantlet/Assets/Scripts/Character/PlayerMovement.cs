using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private EventNetwork eventNetwork;
    [SerializeField] private float speed = 6f;
    private Vector2 _movementInput;
    private bool _isMoving;

    private void Update()
    {
        if (_isMoving)
            transform.Translate(new Vector3(_movementInput.x, 0, _movementInput.y) * speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ct)
    {
        if (ct.performed)
        {
            _movementInput = ct.ReadValue<Vector2>();
            _isMoving = true;
        }

        if (ct.canceled)
            _isMoving = false;
    }
}

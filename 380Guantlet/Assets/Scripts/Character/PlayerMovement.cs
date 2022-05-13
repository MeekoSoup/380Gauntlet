using Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] private EventNetwork eventNetwork;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float checkDistance = 0.5f;
    [SerializeField] private LayerMask wallFilter;
    private Vector2 _movementInput;
    private bool _isMoving;
    private Rigidbody _rb;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _originalPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;

        _originalPosition = transform.position;
        
        Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
        moveDirection.Normalize();

        _ray = new Ray(transform.position, moveDirection);
        if (!Physics.Raycast(_ray, out _hit, checkDistance, wallFilter))
        {
            if (_rb)
                _rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
        }
        
        characterTransform.right = -moveDirection;
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

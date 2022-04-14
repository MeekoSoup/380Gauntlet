using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float _maximumVelocity = 10f;
    [SerializeField] private float _movementForce = 10f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude >= _maximumVelocity)
            return;
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(_movementForce * transform.forward);
        //-forward gives us backward
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(_movementForce * -transform.forward);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(_movementForce * transform.right);
        //-right gives us left
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(_movementForce * -transform.right);
    }

    //might work with Gamepad callback
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move!" + context.phase);

        if (context.performed)
        {
            Vector2 myVector = context.ReadValue<Vector2>();
            rb.AddForce(new Vector3(myVector.x, 0, myVector.y) * speed, ForceMode.Force);
        }
    }
}

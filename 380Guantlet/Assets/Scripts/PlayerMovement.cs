using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    /*private Rigidbody rb;
    * [SerializeField] private float _maximumVelocity = 10f;
    * [SerializeField] private float _movementForce = 10f;
    */
    private Vector2 movementInput;

    /*private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }*/

    private void Update()
    {
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ct)
    {
        movementInput = ct.ReadValue<Vector2>();
    }

    /*void FixedUpdate()
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
    }*/
}

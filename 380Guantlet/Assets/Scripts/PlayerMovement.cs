using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 _velocity;
  
    [SerializeField] private float speed = 2f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
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

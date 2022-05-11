using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 6f;
    
    [SerializeField] public float _maximumVelocity = 10f;
    [SerializeField] public float _movementForce = 10f;

    private Vector2 m;

    [SerializeField] public float turnSpeed = 3;

    [SerializeField] public float timer;
    [SerializeField] public bool timerOn;
    //[SerializeField] public Transform sP;
    //[SerializeField] public GameObject p, p1, p2;

    public Ax wp;
    public Blaster b;
    public Thor t;

    private void Awake()
    {
        timer = 0;
    }

    private void Update()
    {
        Move();
        Timer();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        m = ctx.ReadValue<Vector2>();
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            timerOn = true;
            wp.AxAttack();
        }
        if (ctx.canceled)
        {
            /*if (timer < 2)
            {
                p = Instantiate(p1, sP);
                p.GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 0);
            }
            else
            {
                p = Instantiate(p2, sP);
                p.GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 0);
            }*/
            timerOn = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            timerOn = true;
            t.BonusAttack();
            b.BlasterActive();
        }
        if (ctx.canceled)
        {
            timerOn = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        float horizontal = Input.GetAxis("Mouse X");
        transform.Rotate(horizontal * turnSpeed * Vector3.up, Space.World);
    }

    public void Timer()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }

    public void Move()
    {
        transform.Translate(new Vector3(m.x, 0, m.y) * speed * Time.deltaTime);
    }
}

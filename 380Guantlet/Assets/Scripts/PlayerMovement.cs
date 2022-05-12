using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 6f;
    [SerializeField] private bool _isMoving;

    private Vector2 m;

    //[SerializeField] public float rot = 1f;

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
       /* m.x = m1.x;
        m.z = m1.y;
        if (_isMoving)
        {
            bool v = !(m1.x == 0 && m1.y == 0);
        }*/
    }

    private void Update()
    {
        Move();
        Timer();
        //handleRotation();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            m = ctx.ReadValue<Vector2>();
            _isMoving = true;
           
        }

        if (ctx.canceled)
        {
            _isMoving = false;
        }
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

    //need to fix the rotation
   /*public void handleRotation()
    {
        Vector3 pToLookAt;
        //change in pos the player should look
        pToLookAt.x = m.x;
        pToLookAt.y = 0f;
        pToLookAt.z = m.z;
        //current rotation of the player
        Quaternion currentRotation = transform.rotation;

        if (!_isMoving)
        {
            //creates a new rotation based where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(pToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rot);
        }
       
    }*/

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
        if (_isMoving)
        {
            transform.Translate(new Vector3(m.x, 0, m.y) * speed * Time.deltaTime);
            GetComponent<Rigidbody>().velocity = m;
        }
            
    }
}

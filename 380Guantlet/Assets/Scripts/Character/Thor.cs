using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thor : MonoBehaviour
{/*Description Notes:
  * -brute character(great working with others, than solo)
  * -shot delivering 2points of 100% damage
  * -easily kills any lv2 and lv3
  * -disadvantage no magic and shots can't hit corners
  * -can only pickup keys
  */

    //only use when you have all the scripts working 
    //public PlayerMovement _move;
    //public Pickup _pickUp;
    //public GameManger gm;
    //public Enemy _enemy;

    private Rigidbody rb;
    public float swing;
    public float maxSwing;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }
}

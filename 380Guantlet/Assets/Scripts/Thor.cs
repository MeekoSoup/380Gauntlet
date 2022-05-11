using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thor : MonoBehaviour
{/*Description Notes:
  * -brute character(great working with others, than solo)
  * -shot delivering 2points of 100% damage
  * -easily kills any lv2 and lv3
  * -disadvantage no magic and shots can't hit corners
  * -can only pickup keys
  */

    public Blaster b;

    [SerializeField] public int potion, health, lives;
    [SerializeField] public PlayerMovement bA;
    [SerializeField] public bool IsAttacking = false;
    [SerializeField] public bool CanAttack = true;
    [SerializeField] public float AttackCoolDown = 1.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "potion")
        {
            potion++;
            other.gameObject.SetActive(false);
            if (potion <= 2)
            {
                BonusAttack();
            }
        }

        if (other.tag == "health")
        {
            health++;
            other.gameObject.SetActive(false);
            if (health <= 1)
            {
                lives++;
            }
        }

        if (other.tag == "Enemy")
        {
            health--;
            lives--;
            potion--;
            other.gameObject.SetActive(false);
        }
    }

    public void BonusAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        b.BlasterActive();
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCoolDown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }
}


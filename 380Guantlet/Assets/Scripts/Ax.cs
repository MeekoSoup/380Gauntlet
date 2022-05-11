using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    public GameObject _Ax;
    
    public bool IsAttacking = false;
    public bool CanAttack = true;
    public float AttackCoolDown = 1.0f;

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanAttack)
            {
                AxAttack();
            }
        }
    }*/

    public void AxAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = _Ax.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        //need to fix error
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

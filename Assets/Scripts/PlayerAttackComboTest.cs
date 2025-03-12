using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComboTest : MonoBehaviour
{
    public Animator myAnim;
    public bool isAttacking = false;
    public static PlayerAttackComboTest instance;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
        }
    }

}

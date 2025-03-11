using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComboTest : MonoBehaviour
{
    public Animator myAnim;
    public bool isAttacking = false;
    //public bool canReceiveInput;
    //public bool inputReceived;
    public static PlayerAttackComboTest instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //myAnim = GetComponent<Animator>();
    }


    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown("a") && !isAttacking)
        {
            Debug.Log("Attacking");
            isAttacking = true;
        }
    }


    //public void Attack()
    //{
    //    if (Input.GetKeyDown("Z"))
    //    {
    //        Debug.Log("AttackButtonPressed");
    //        if (canReceiveInput)
    //        {
    //            inputReceived = true;
    //            canReceiveInput = false;
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //}

    //public void InputManager()
    //{
    //    if (!canReceiveInput)
    //    {
    //        canReceiveInput = true;
    //    }
    //    else
    //    {
    //        canReceiveInput = false;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;
    public float attackSpeed;
    float originalSpeed;

    Rigidbody2D rb;

    float inputHorizontal;
    bool facingRight = true;
    public Animator headAnim;
    public Animator bodyAnim;
    public Animator capeAnim;

    public float jump;
    public float groundedY;

    public bool isAttacking = false;
    public static PlayerController2 instance;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool playingFootsteps = false;
    public float footstepSpeed = 0.5f;

    private void Awake()
    {
        instance = this;
        originalSpeed = speed;
    }

    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    //Move character and flip
    void Update()
    {
        Attack();
        AerialAttack();
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //Disable player movewment during hit knockback
        if (GetComponentInChildren<PlayerHealth>().PlayerDamaged == true)
        {
            speed = 0;
        }
        else if (GetComponentInChildren<PlayerHealth>().PlayerDamaged == false)
        {
            speed = originalSpeed;
        }

        if (IsAttackFinished())
        
        {
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            //bodyAnim.SetBool("isWalking", true);
            CheckJump();
            CheckAnimations();

            //StartFootsteps only while walking and not jumping, and stop footsteps when no longer walking or in the air
            if(inputHorizontal != 0 && !playingFootsteps && IsGrounded())
            {
                StartFootsteps();
            }
            else if(inputHorizontal == 0)
            {
                StopFootsteps();
            }
            else if (!IsGrounded())
            {
                StopFootsteps();
            }
        }

        if (!IsAttackFinished())

        {
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * attackSpeed * Time.deltaTime);            
        }

        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
        }

        if (inputHorizontal < 0 && facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }


    void CheckAnimations()
    {
        if (IsJumpFinished())
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                headAnim.Play("Head_Walk");
                bodyAnim.Play("Body_Walk");
                capeAnim.Play("CapeF_Walk");
            }
            else
            {
                headAnim.Play("Head_Idle");
                bodyAnim.Play("Body_Idle");
                capeAnim.Play("CapeB_Idle");
            }
        }
    }

    //Jump
    void CheckJump()
    {
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            
            coyoteTimeCounter = 0f;

            if (rb.velocity.y < 0)
            {
                headAnim.Play("Head_Walk");
                bodyAnim.Play("Player_JumpDown");
                capeAnim.Play("CapeF_Walk");
            }

            else if (rb.velocity.y > 0)
            {
                headAnim.Play("Head_Walk");
                bodyAnim.Play("Player_Jump");
                capeAnim.Play("CapeF_Walk");

                jumpBufferCounter = 0f;
            }



        }

        if (!IsGrounded() && rb.velocity.y > 0)
        {
            headAnim.Play("Head_Walk");
            bodyAnim.Play("Player_Jump");
            capeAnim.Play("CapeF_Walk");
        }

        else if (!IsGrounded() && rb.velocity.y < 0)
        {
            headAnim.Play("Head_Walk");
            bodyAnim.Play("Player_JumpDown");
            capeAnim.Play("CapeF_Walk");
        }

    }

    public bool IsGrounded()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, groundedY), Vector2.down, .1f);

        if (hit.collider != null)
        {
            
            return true;

        }


        return false;

    }

    //Draw line for checking grounded
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, groundedY), Vector2.down * .1f);
    }

    public bool IsJumpFinished()
    {

        if (!IsGrounded()) { return false; }

        if (!bodyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) { return true; }

        //if (bodyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < bodyAnim.GetCurrentAnimatorStateInfo(0).length) { return false; }

        return true;

    }

    //function for attacking
    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && IsJumpFinished())
        {
  
        //bodyAnim.SetTrigger("Attack");
        isAttacking = true;
            
        }
    }

    void AerialAttack()
    {
        if (Input.GetButtonDown("Fire1") && !IsGrounded() && IsAttackFinished())
        {
            bodyAnim.SetTrigger("Attack");
        }
    }

    public bool IsAttackFinished()
    {

            if (!bodyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Debug.Log("AttackFinished");
            return true;
        }

        if (bodyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < bodyAnim.GetCurrentAnimatorStateInfo(0).length)
        {
            //Debug.Log("AttackNotFinished");
            return false;
        }
        return false;
        
    }

    //functions for starting and stopping footstep audio
    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footstepSpeed);
    }

    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    void PlayFootstep()
    {
        SoundEffectManager.Play("Footstep", true);
    }
}

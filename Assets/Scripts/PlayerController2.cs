using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;
    public float attackSpeed;

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

    private void Awake()
    {
        instance = this;
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


        if (IsAttackFinished())
         
        {
                transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

                CheckJump();
                CheckAnimations();
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
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jump, ForceMode2D.Impulse);

            headAnim.Play("Head_Walk");
            bodyAnim.Play("Player_Jump");
            capeAnim.Play("CapeF_Walk");

        }

        if (!IsGrounded())
        {
            headAnim.Play("Head_Walk");
            bodyAnim.Play("Player_Jump");
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
            //Debug.Log("Attacking");
            isAttacking = true;
        }
    }

    void AerialAttack()
    {
        if (Input.GetButtonDown("Fire1") && !IsGrounded())
        {
            Debug.Log("Attacking");
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
}

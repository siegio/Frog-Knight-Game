using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    float inputHorizontal;
    bool facingRight = true;
    public Animator headAnim;
    public Animator bodyAnim;
    public Animator capeAnim;

    public float jump;
    public float groundedY;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        {
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

            CheckJump();
            CheckAnimations();
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

        }

        if (Input.GetAxis("Horizontal") > 0 && !IsGrounded())
        {
            headAnim.Play("Head_Idle");
            bodyAnim.Play("Player_Jump");
            capeAnim.Play("CapeF_Idle");
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

    public bool IsJumpFinished()
    {

        if (!IsGrounded()) { return false; }

        if (!bodyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) { return true; }

        if (bodyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < bodyAnim.GetCurrentAnimatorStateInfo(0).length) { return false; }

        Debug.Log("JumpFinished");
        return true;

    }
}

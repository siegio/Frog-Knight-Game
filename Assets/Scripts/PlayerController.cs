using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jump;
    public float groundedY;
    public float attackSpeed;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    
    float inputHorizontal;
    float inputVertical;
    bool facingRight = true;

    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public CoinManager cm;

    Animator animator;

    private void Awake()
    {

        animator = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        //if (inputHorizontal != 0)
        //{

        //}

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


    void Update()
    {
        // Move
        if (IsAttackFinished())
        {

            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

            CheckJump();

            CheckAnimations();

        }

        else if (!IsAttackFinished())
        {

            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * attackSpeed * Time.deltaTime);

        }

        // Attack
        if (Time.time >= nextAttackTime) {

            if (Input.GetButtonDown("Fire3") && (Input.GetAxis("Horizontal") is 0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetButtonDown("Fire3") && (Input.GetAxis("Horizontal") > 0))
        {

            animator.Play("PlayerAttackRight");
        }

        
        if (Input.GetButtonDown("Fire3") && (Input.GetAxis("Horizontal") < 0))
        {
 
            animator.Play("PlayerAttackLeft");
        }

    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");
        //Detect enenmies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    //Visualize attackPoint in editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void CheckAnimations()
    {
        if (IsJumpFinished() && IsAttackFinished())
        {
                if (Input.GetAxis("Horizontal") != 0)
                {

                    animator.Play("PlayerGoRight");

                }
                //if (Input.GetAxis("Horizontal") < 0 )
                //{

                //    animator.Play("PlayerGoLeft");

                ////}
                //else if (Input.GetAxis("Horizontal") > 0 )
                //{

                //    animator.Play("PlayerGoRight");

                //}
                
            else
            {

                animator.Play("PlayerIdle");

            }

        }

    }

   

    //Jump
    void CheckJump()
        {

            if (Input.GetButtonDown("Jump") && IsGrounded() && IsAttackFinished())
            {

                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jump, ForceMode2D.Impulse);

                animator.Play("PlayerJumpRight");

                //if (Input.GetAxis("Horizontal") < 0) { animator.Play("PlayerJumpLeft"); }

            }

            if (Input.GetAxis("Horizontal") > 0 && !IsGrounded() && IsAttackFinished())
            {

                animator.Play("PlayerJumpRight");

                if (Input.GetAxis("Horizontal") < 0) { animator.Play("PlayerJumpLeft"); }

            }

            if (Input.GetAxis("Horizontal") < 0 && !IsGrounded() && IsAttackFinished())
            {

                animator.Play("PlayerJumpLeft");

                if (Input.GetAxis("Horizontal") > 0) { animator.Play("PlayerJumpRight"); }

            }

        }

        //Checking if the player is grounded
        public bool IsGrounded()
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, groundedY), Vector2.down, .1f);

            if (hit.collider != null)
            {

                return true;

            }


            return false;

        }

        //Checks if Player is grounded to see if it should stop playing Jump animation
        public bool IsJumpFinished()
        {
            
            if (!IsGrounded()) { return false; }

            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) { return true; }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < animator.GetCurrentAnimatorStateInfo(0).length) { return false; }

            Debug.Log("JumpFinished");
            return true;
        }

        //Checks if Player is grounded to see if it should stop playing Jump animation    
        public bool IsAttackFinished()
        {

            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) { return true; }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < animator.GetCurrentAnimatorStateInfo(0).length) { return false; }

            return true;
        }

    //Visualizes the checking raycast for if the player is grounded
    private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position + new Vector3(0, groundedY), Vector2.down * .1f);

        }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tadpole"))
        {
            Destroy(other.gameObject);
            cm.tadpoleCount++;
        }
    }
}

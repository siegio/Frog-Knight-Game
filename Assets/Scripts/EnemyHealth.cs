using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    int currentHealth;
    public Animator animator;

    public GameObject player;
    public float KBForce = 5;

    public float KBForceMult = 1;
    public float KBCounter;
    public float KBTotalTime = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

// Enemy gets knocked back when hit
    private void FixedUpdate()
    {
        if (player.transform.position.x < transform.position.x && KBCounter > 0)
            //other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * KBForce, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().velocity = new Vector2(KBForce*KBForceMult, 0);

        else if (player.transform.position.x > transform.position.x && KBCounter > 0)
            //other.GetComponent<Rigidbody2D>().AddForce(Vector2.left * KBForce, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-KBForce * KBForceMult, 0);

        if (KBCounter <= 0 )
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            KBForceMult = 1;
        }

        if (KBCounter > 0)
        {
            KBCounter -= Time.deltaTime;
        }

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die(){

        //enable below when wasp has a death anim
        //animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        //Below should happen after finishing Death Animation
        Destroy(this.gameObject);

    }
        
}

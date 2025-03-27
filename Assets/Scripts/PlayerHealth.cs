using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public float KBForce = 5;
    public float KBCounter;
    public float KBTotalTime = 2;

    public GameObject player;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            KBCounter = KBTotalTime;

            if (other.transform.position.x < transform.position.x && KBCounter > 0)

                //GetComponent<Rigidbody2D>().AddForce(Vector2.right * KBForce, ForceMode2D.Impulse);
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(KBForce, KBForce);

            else if (other.transform.position.x > transform.position.x && KBCounter > 0)
                //GetComponent<Rigidbody2D>().AddForce(Vector2.left * KBForce, ForceMode2D.Impulse);
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-KBForce, KBForce);

            TakeDamage(20);

        }

        //Sets Game Over screen when player runs out of HP
        if (currentHealth <= 0)
        {

            SceneManager.LoadScene(1);

        }
    }



    // Player gets knocked back when hit
    private void FixedUpdate()
    {
        

        //if (KBCounter <= 0)
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        //    KBForceMult = 1;
        //}

        if (KBCounter > 0)
        {
            KBCounter -= Time.deltaTime;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public float KBForce = 5;
    public float KBCounter;
    public float KBTotalTime = 2;

    public GameObject player;

    public bool PlayerDamaged = false;
    private DamageFlash _damageFlash;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        impulseSource = GetComponent<CinemachineImpulseSource>();
        _damageFlash = GetComponentInParent<DamageFlash>();
    }

    void TakeDamage(int damage)
    {
        
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        //damage flash effect
        _damageFlash.CallDamageFlash();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            KBCounter = KBTotalTime;


            PlayerDamaged = true;

            if (other.transform.position.x < transform.position.x && KBCounter > 0)

                //GetComponent<Rigidbody2D>().AddForce(Vector2.right * KBForce, ForceMode2D.Impulse);
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(KBForce, KBForce);

            else if (other.transform.position.x > transform.position.x && KBCounter > 0)
                //GetComponent<Rigidbody2D>().AddForce(Vector2.left * KBForce, ForceMode2D.Impulse);
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-KBForce, KBForce);


            //Screenshake
            CameraShakeManager.instance.CameraShake(impulseSource);

            //Deal damage to player
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

        if (KBCounter <= 0)
        {
            PlayerDamaged = false;
        }
    }

}

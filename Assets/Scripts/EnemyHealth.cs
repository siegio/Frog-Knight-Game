using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    private CinemachineImpulseSource impulseSource;
    private DamageFlash _damageFlash;
    public ParticleSystem onHitEffect1;
    public ParticleSystem onHitEffect2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        impulseSource = GetComponent<CinemachineImpulseSource>();

        _damageFlash = GetComponent<DamageFlash>();
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

        //animator.ResetTrigger("Hurt");
    }


    public void TakeDamage(int damage)
    {

        CameraShakeManager.instance.CameraShake(impulseSource);

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

        //damage flash effect
        _damageFlash.CallDamageFlash();

        Instantiate(onHitEffect1);
        Instantiate(onHitEffect2);
    }

    void Die(){

        //enable below when enemy has a death anim
        //animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        //Below should happen after finishing Death Animation
        Destroy(this.gameObject);

    }
        
}

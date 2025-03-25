using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCollision : MonoBehaviour
{
    public int attackDamage = 1;
    public int staggerDist = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * staggerDist, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCollision : MonoBehaviour
{
    public int attackDamage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
}

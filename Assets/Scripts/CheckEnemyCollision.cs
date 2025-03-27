using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCollision : MonoBehaviour
{
    public int attackDamage = 1;
    public GameObject player;
    public float KBForceMult = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().KBCounter = other.GetComponent<EnemyHealth>().KBTotalTime;
            other.GetComponent<EnemyHealth>().KBForceMult = other.GetComponent<EnemyHealth>().KBForceMult*KBForceMult;
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
}

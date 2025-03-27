using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCollision : MonoBehaviour
{
    public int attackDamage = 1;
    public int staggerDist = 1;
    public GameObject player;
    public float knockbackTime = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);

            if (player.transform.position.x < other.transform.position.x)
                other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * staggerDist, ForceMode2D.Impulse);

            else if (player.transform.position.x > other.transform.position.x)
                other.GetComponent<Rigidbody2D>().AddForce(Vector2.left * staggerDist, ForceMode2D.Impulse);
        }
    }


}

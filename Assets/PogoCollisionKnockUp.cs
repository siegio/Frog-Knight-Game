using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoCollisionKnockUp : MonoBehaviour
{
    public GameObject player;
    public float KBForceMult = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Obstacle")
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * KBForceMult, ForceMode2D.Impulse);
        }
    }
}

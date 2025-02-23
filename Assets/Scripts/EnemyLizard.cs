using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLizard : MonoBehaviour
{
    public GameObject player;
    public float aggroRange;
    public float speed;
    public float jumpCooldown;
    public float jumpForce;

    float lastJump;

    void Update()
    {

        if (Vector2.Distance(player.transform.position, transform.position) < aggroRange)
        {
            //Makes enemy go right and left on x-axis)
            if (player.transform.position.x < transform.position.x)
            {

                transform.Translate(Vector2.left * Time.deltaTime * speed);

            }
            else
            {

                transform.Translate(Vector2.right * Time.deltaTime * speed);

            }

            if (Time.time > lastJump + jumpCooldown)
            {

                lastJump = Time.time;

                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            collision.collider.GetComponent<PlayerController>();
        }
    }

}

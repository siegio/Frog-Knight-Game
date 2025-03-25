using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWasp : MonoBehaviour
{
    public GameObject player;
    public float aggroRange;
    public float speed;
    public float jumpCooldown;
    bool facingLeft = true;

    void Update()
    {
        
        if (Vector2.Distance(player.transform.position, transform.position) < aggroRange)
        {
            //Makes enemy go right and left on x-axis)
            if (player.transform.position.x < transform.position.x)
            {

                transform.Translate(Vector2.left * Time.deltaTime * speed);

            } else
            {

                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }

            //Flips enemy depending on player position
            if (player.transform.position.x < transform.position.x && !facingLeft)
            {

                Flip();

            }
            else if (player.transform.position.x > transform.position.x && facingLeft)
            {

                Flip();
            }

            //Makes enemy go up and down on y-axis)
            if (player.transform.position.y > transform.position.y)
            {

                transform.Translate(Vector2.up * Time.deltaTime * speed);

            }
            else
            {

                transform.Translate(Vector2.down * Time.deltaTime * speed);

            }
        }

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockEvent : MonoBehaviour
{
    public GameObject fallingRock;
    public GameObject target;
    public float speed = 1.0f;

    bool rockTrigger = false;

    void Update()
    {

        if (rockTrigger == true)
        {
            var step = speed * Time.fixedDeltaTime;
            fallingRock.transform.position = Vector3.MoveTowards(fallingRock.transform.position, target.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            rockTrigger = true;
        }
    }
}

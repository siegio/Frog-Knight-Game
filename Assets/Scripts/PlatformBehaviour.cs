using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public GameObject player;
    BoxCollider2D platformCollider;

    private void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is under or over platform
        //Let player pass through platform from below
        if (player.transform.position.y < transform.position.y)
        {
            platformCollider.enabled = false;
        }

        if (player.transform.position.y >= transform.position.y)
        {
            platformCollider.enabled = true;
        }
    }


}

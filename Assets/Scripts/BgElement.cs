using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgElement : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float deadX;

    float offset;

    void Start()
    {

         offset = player.transform.position.x - transform.position.x;

    }

    void Update()
    {

        transform.localPosition = new Vector3(-player.transform.position.x * speed - offset, transform.localPosition.y);

        if (transform.localPosition.x < -deadX) { offset -= deadX * 1; }
        if (transform.localPosition.x > deadX) { offset += deadX * 1; }

    }
}

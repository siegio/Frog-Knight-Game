using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public int Zpos = -15;

    void Update()
    {

        transform.localPosition = new Vector3(player.transform.position.x * 1, transform.localPosition.y, Zpos);

    }
}

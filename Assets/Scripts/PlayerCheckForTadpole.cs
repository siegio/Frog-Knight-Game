using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckForTadpole : MonoBehaviour
{
    public CoinManager cm;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tadpole"))
        {
            Destroy(other.gameObject);
            cm.tadpoleCount++;
        }
    }
}

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
            SoundEffectManager.Play("TadpoleGet");
            Destroy(other.gameObject);
            cm.tadpoleCount++;
        }
    }
}

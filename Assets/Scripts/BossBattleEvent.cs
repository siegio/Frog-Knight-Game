using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleEvent : MonoBehaviour
{
    public GameObject closingGate;
    public GameObject gateCloseTarget;
    public GameObject gateOpenTarget;
    public float speed = 1.0f;

    bool bossTrigger = false;

    void Update()
    {

        if (bossTrigger == true)
        {
            //Vector3 TargetPos = new Vector3(transform.position.x, gateCloseTarget.transform.position.y, transform.position.z);
            var step = speed * Time.fixedDeltaTime;
            closingGate.transform.position = Vector3.MoveTowards(closingGate.transform.position, gateCloseTarget.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            bossTrigger = true;
        }
    }
}

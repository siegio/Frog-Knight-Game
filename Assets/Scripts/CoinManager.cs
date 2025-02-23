using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int tadpoleCount;
    public TextMeshProUGUI tadpoleText;
    public GameObject door;
    private bool doorDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tadpoleText.text = tadpoleCount.ToString();

        if(tadpoleCount == 4 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }
    }
}

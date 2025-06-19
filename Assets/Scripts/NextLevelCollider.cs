using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCollider : MonoBehaviour
{
    public int sceneNumber;

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            NextLevel();
        }
    }
}

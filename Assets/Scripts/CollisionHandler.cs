using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            StartCrashSequence();
        }        
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;        

        Invoke("ReloadLevel", delay);
    }
    void ReloadLevel()
    {       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

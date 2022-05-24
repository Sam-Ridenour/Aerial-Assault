using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] ParticleSystem explosionParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            StartCrashSequence();
        }        
    }

    void StartCrashSequence()
    {
        explosionParticle.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;        

        Invoke("ReloadLevel", delay);
        
    }
    void ReloadLevel()
    {       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

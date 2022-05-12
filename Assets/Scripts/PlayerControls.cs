using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")][SerializeField] float controlSpeed = 10f;
    [Tooltip("The maximum X range player can play in")][SerializeField] float xRange = 8f;
    [Tooltip("The maximum Y range player can play in")][SerializeField] float yRange = 8f;
    [Tooltip("The lasers the player shoots")][SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -1f;
    [SerializeField] float positionRollFactor = -1f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -12f;    
    [SerializeField] float controlYawFactor = -10f;   
    [SerializeField] float controlRollFactor = -.1f;

    float horizontalThrow;
    float verticalThrow;
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = horizontalThrow * controlYawFactor;

        float rollDueToPosition = transform.position.z * positionRollFactor;
        float rollDueToControlThrow = horizontalThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition + yawDueToControlThrow;
        float roll = rollDueToPosition * rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");

        float xOffset = horizontalThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = verticalThrow * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);




        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            // if pushing fire button
            SetLasersActive(true);
            // else don't print shooting
        }
        else
        {
            SetLasersActive(false);
        }

        void SetLasersActive(bool isActive)
        {
            foreach (GameObject laser in lasers)
            {
                var emissionModule = laser.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isActive;
            }
        }      
    }
}

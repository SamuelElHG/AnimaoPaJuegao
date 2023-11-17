using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWeapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private float fireRate;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Camera cam;
    [SerializeField] private RectTransform deadZone;

    private bool isButtonDown;
    private float timeElapsed;

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void LateUpdate()
    {
        ShootAtMousePosition();
    }

    public void ShootAtMousePosition()
    {
        if (cam == null)
        {
            Debug.Log("DEMOWEAPON ShootAtMousePosition: missing main Camera!");
            return;
        }

        if (projectileParticles == null)
        {
            Debug.Log("DEMOWEAPON ShootAtMousePosition: missing Particle Systems!");
            return;
        }

        Vector2 mousePosition = Input.mousePosition;

        if (deadZone != null && RectTransformUtility.RectangleContainsScreenPoint(deadZone, mousePosition))
        {
            return;
        }

        Ray ray = cam.ScreenPointToRay(mousePosition);


        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(hit.point - transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            isButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isButtonDown = false;
        }

        timeElapsed += Time.deltaTime;

        if (!isButtonDown || timeElapsed < fireRate)
        {
            return;
        }

        if (projectileParticles != null)
        {
            projectileParticles.Emit(1);
        }

        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }

        timeElapsed = 0;
    }
    
}

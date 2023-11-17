using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleCollisionFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionFXPrefab;
    [SerializeField] private float lifeTime = 1.5f;
    private ParticleSystem primaryFX;
    private readonly List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Awake()
    {
        primaryFX = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (collisionFXPrefab == null)
        {
            Debug.Log("PARTICLE COLLISION FX OnParticleCollsiion: missing Collision FX Prefab!");
            return;
        }

        int count = primaryFX.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < count)
        {
            var collisionFX = Instantiate(collisionFXPrefab, collisionEvents[i].intersection, Quaternion.identity);

            collisionFX.transform.rotation = Quaternion.LookRotation(collisionEvents[i].normal);

            ForcefieldImpact forcefieldImpact = other.GetComponent<ForcefieldImpact>();

            if (forcefieldImpact != null)
            {
                forcefieldImpact.ApplyImpact(collisionEvents[i].intersection, collisionEvents[i].normal);
            }

            Destroy(collisionFX.gameObject, lifeTime);

            i++;
        }
    }
}
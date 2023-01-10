using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeParticleCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pukeParticles;
    [SerializeField] private PresentGenerator _presentGenerator;
    private List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        _pukeParticles = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        _pukeParticles.GetCollisionEvents(other, collisionEvents);
        _presentGenerator.PlacePukeDecal(collisionEvents[0]);
    }
}

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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        //int numCollisionEvents =
        _pukeParticles.GetCollisionEvents(other, collisionEvents);
        //print(numCollisionEvents);

        ////Rigidbody rb = other.GetComponent<Rigidbody>();
        //int i = 0;

        //while (i < numCollisionEvents)
        //{
            
            _presentGenerator.PlacePukeDecal(collisionEvents[0]);
            //Vector3 pos = collisionEvents[i].intersection;


        //    i++;
        //}
    }
}

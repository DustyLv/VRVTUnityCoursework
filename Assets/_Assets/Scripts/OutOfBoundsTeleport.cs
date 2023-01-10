using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsTeleport : MonoBehaviour
{
    [SerializeField] private LayerMask checkLayers = -1;
    [SerializeField] private Transform teleportPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void TeleportTargetToPosition(Transform target)
    {
        target.position = teleportPosition.position;
        target.rotation = teleportPosition.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkLayers.Contains(other.gameObject.layer))
        {
            TeleportTargetToPosition(other.gameObject.transform);
        }
    }
}

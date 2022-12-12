using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionSeeker : MonoBehaviour
{
    [SerializeField] private Transform m_RaycastSourcePosition;
    [SerializeField] private LayerMask m_LayerMask = -1;
    [SerializeField] private float m_ActivationDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(m_RaycastSourcePosition.position, m_RaycastSourcePosition.TransformDirection(Vector3.forward), out hit, m_ActivationDistance, m_LayerMask))
        {
            UIController.Instance.CrosshairPOIActivate(true);
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.TryGetComponent(out AttentionSource source))
                {
                    source.ActivateSource();
                }
            }

        }
        else
        {
            UIController.Instance.CrosshairPOIActivate(false);
        }
    }
}

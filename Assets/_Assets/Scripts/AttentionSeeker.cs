using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionSeeker : MonoBehaviour
{
    [SerializeField] private Transform m_RaycastSourcePosition;
    [SerializeField] private LayerMask m_LayerMask = -1;
    [SerializeField] private float m_ActivationDistance = 1f;

    [SerializeField] private PlayerAnimator _playerAnimator;

    private GameObject m_HitObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.GameRunning) { return; }

        RaycastHit hit;
        if (Physics.Raycast(m_RaycastSourcePosition.position, m_RaycastSourcePosition.TransformDirection(Vector3.forward), out hit, m_ActivationDistance, m_LayerMask))
        {
            UIController.Instance.CrosshairEnable();

            if (m_HitObject != hit.collider.gameObject)
            {
                m_HitObject = hit.collider.gameObject;
            }

        }
        else
        {
            if(m_HitObject != null) { m_HitObject = null; }
            UIController.Instance.CrosshairDisable();
        }


    }

    private void Update()
    {
        if (!GameManager.Instance.GameRunning) { return; }

        if (Input.GetMouseButtonDown(0))
        {

            if (m_HitObject == null) return;
            if (m_HitObject.TryGetComponent(out AttentionSource source))
            {
                //print("Mouse clicked");
                source.TryActivateSource();
                //_playerAnimator.TriggerActionAnimation();
            }
        }
    }
}

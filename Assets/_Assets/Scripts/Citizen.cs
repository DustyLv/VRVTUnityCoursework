using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Citizen : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navAgent;

    private bool _destinationReached = false;
    private Transform _lookAtTargetTransform = null;

    // Start is called before the first frame update
    void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ReachedDestinationOrGaveUp() && !_destinationReached)
        {
            OnDestinationReached();
            _destinationReached = true;
        }
    }

    private void OnDestinationReached()
    {
        _navAgent.isStopped = false;
        transform.DOLookAt(_lookAtTargetTransform.position, 0.5f, AxisConstraint.Y);

        TriggerRandomEmote();
    }

    private void TriggerRandomEmote()
    {
        int randNumber = Random.Range(1, 6);
        _animator.SetTrigger("action" + randNumber);
    }

    public void TriggerEndEmote()
    {
        int randNumber = Random.Range(1, 4);
        _animator.SetTrigger("cheer" + randNumber);
    }

    public void SetCitizenVariables(Vector3 goal, Transform lookAtTarget)
    {
        GetReferences();

        _navAgent.destination = goal;
        _navAgent.avoidancePriority = Random.Range(0, 60);
        _animator.SetBool("walking", true);

        _lookAtTargetTransform = lookAtTarget;
    }

    public bool ReachedDestinationOrGaveUp()
    {

        if (!_navAgent.pathPending)
        {
            if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                if (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude <= 0.1f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

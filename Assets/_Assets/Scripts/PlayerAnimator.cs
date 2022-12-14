using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private string _animVar_Action = "Action";

    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerActionAnimation()
    {
        _animator.SetTrigger(_animVar_Action);
    }
}

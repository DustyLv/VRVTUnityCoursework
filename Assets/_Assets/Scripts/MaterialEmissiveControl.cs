using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissiveControl : MonoBehaviour
{
    [SerializeField] private bool _startState = false;
    [SerializeField] private Renderer _targetRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SetEmissionState(_startState);
    }

    public void SetEmissionState(bool _state)
    {
        if (_state)
        {
            _targetRenderer.material.EnableKeyword("_EMISSION");
        }
        else
        {
            _targetRenderer.material.DisableKeyword("_EMISSION");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentGenerator : MonoBehaviour
{
    [Header("Puke Stuff")]
    [SerializeField] private KeyCode _pukeKey;
    [SerializeField] private Transform m_RaycastSourcePosition;
    [SerializeField] private LayerMask m_LayerMask = -1;
    [SerializeField] private float m_ActivationDistance = 1f;

    [SerializeField] private Transform _pukeDecalPrefab;
    [SerializeField] private Vector2 _decalScaleVariance = Vector2.one;
    
    [Header("Poop Stuff")]
    [SerializeField] private KeyCode _poopKey;
    [SerializeField] private Transform _poopSource;
    [SerializeField] private Rigidbody _poopPrefab;
    [SerializeField] private Vector2 _poopScaleVariance = Vector2.one;
    [SerializeField] private float _poopForce;
    [SerializeField] private Vector2 _poopTimerMinMax = new Vector2(5f,15f);


    private SFXPlayer _sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _sfxPlayer = FindObjectOfType<SFXPlayer>();
        StartCoroutine("PanicPoops");
        GameManager.Instance.OnGameEnd += StopPooping;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.GameRunning) { return; }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(m_RaycastSourcePosition.position, m_RaycastSourcePosition.TransformDirection(Vector3.forward), out hit, m_ActivationDistance, m_LayerMask))
            {
                Puke(hit);
            }
        }

        //if (Input.GetKeyDown(_poopKey))
        //{
        //    Poop();
        //}
    }

    private void Puke(RaycastHit hit)
    {
        Transform pukeObject = Instantiate(_pukeDecalPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        pukeObject.localScale = Vector3.one * Random.Range(_decalScaleVariance.x, _decalScaleVariance.y);
        _sfxPlayer.PlayDogPukeSound();
    }

    private IEnumerator PanicPoops()
    {
        yield return new WaitForSeconds(Random.Range(_poopTimerMinMax.x, _poopTimerMinMax.y));
        Poop();
        StartCoroutine("PanicPoops");
    }

    private void StopPooping()
    {
        StopCoroutine("PanicPoops");
    }

    private void Poop()
    {
        Rigidbody poopRB = Instantiate(_poopPrefab, _poopSource.position, Quaternion.identity);
        poopRB.transform.localScale = Vector3.one * Random.Range(_poopScaleVariance.x, _poopScaleVariance.y);
        poopRB.AddForce(_poopSource.forward * _poopForce);
        _sfxPlayer.PlayDogPoopSound();
    }
}

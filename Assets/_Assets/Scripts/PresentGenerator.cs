using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PresentGenerator : MonoBehaviour
{
    [Header("General Stuff")]
    [SerializeField] private KeyCode _presentToggleKey = KeyCode.End;
    [SerializeField] private bool _presentsEnabledByDefault = true;

    [Header("Puke Stuff")]
    [SerializeField] private Vector2 _decalScaleVariance = Vector2.one;
    [SerializeField] private ParticleSystem _pukeParticles;

    [SerializeField] private LeanGameObjectPool _vomitPool;

    [Header("Poop Stuff")]
    [SerializeField] private Transform _poopSource;
    [SerializeField] private Rigidbody _poopPrefab;
    [SerializeField] private Transform _poopHolder;
    [SerializeField] private Vector2 _poopScaleVariance = Vector2.one;
    [SerializeField] private float _poopForce;
    [SerializeField] private Vector2 _poopTimerMinMax = new Vector2(5f,15f);

    private bool _presentGenerationEnabled = false;

    private SFXPlayer _sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _sfxPlayer = FindObjectOfType<SFXPlayer>();
        if (_presentsEnabledByDefault)
        {
            EnablePresents();
        }
        else
        {
            DisablePresents();
        }
        GameManager.Instance.OnGameEnd += DisablePresents;
        GameManager.Instance.OnGamePause += DisablePresents;
        GameManager.Instance.OnGameResume += EnablePresents;
    }

    void Update()
    {
        if (Input.GetKeyDown(_presentToggleKey))
        {
            if (_presentGenerationEnabled)
            {
                DisablePresents();
            }
            else
            {
                EnablePresents();
            }
        }
        if (!_presentGenerationEnabled) { return; }
        if (Input.GetMouseButtonDown(1) && !_pukeParticles.isEmitting)
        {
                Puke();
        }
    }

    private void Puke()
    {
        _pukeParticles.Play();
        _sfxPlayer.PlayDogPukeSound();
    }

    public void PlacePukeDecal(ParticleCollisionEvent collision)
    {
        GameObject vomitObj = _vomitPool.Spawn(collision.intersection, Quaternion.FromToRotation(Vector3.forward, collision.normal), _vomitPool.transform);
        vomitObj.transform.localScale = Vector3.one * Random.Range(_decalScaleVariance.x, _decalScaleVariance.y);
    }

    private void EnablePresents()
    {
        _presentGenerationEnabled = true;
        StartPooping();
    }

    private void DisablePresents()
    {
        _presentGenerationEnabled = false;
        StopPooping();
    }


    private IEnumerator PanicPoops()
    {
        yield return new WaitForSeconds(Random.Range(_poopTimerMinMax.x, _poopTimerMinMax.y));
        Poop();
        StartCoroutine("PanicPoops");
    }

    private void StartPooping()
    {
        StartCoroutine("PanicPoops");
    }

    private void StopPooping()
    {
        StopCoroutine("PanicPoops");
    }

    private void Poop()
    {
        Rigidbody poopRB = Instantiate(_poopPrefab, _poopSource.position, Quaternion.identity, _poopHolder);
        poopRB.transform.localScale = Vector3.one * Random.Range(_poopScaleVariance.x, _poopScaleVariance.y);
        poopRB.AddForce(_poopSource.forward * _poopForce);
        _sfxPlayer.PlayDogPoopSound();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{
    [SerializeField] private Citizen _citizenBasePrefab;
    [SerializeField] private List<GameObject> _citizenCharacterModelPrefabs;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Transform> _goalPoints;
    [SerializeField] private Transform _citizenLookAtTarget;

    [SerializeField] private Transform _spawnPoint_Hero;
    [SerializeField] private Transform _goalPoint_Hero;

    [SerializeField] private int _maxSpawnedCitizens = 30;

    private AttentionMeter _attentionMeter;
    private int _spawnedCitizenCount = 0;

    private List<Citizen> _spawnedCitizens = new List<Citizen>();

    // Start is called before the first frame update
    void Start()
    {
        _attentionMeter = FindObjectOfType<AttentionMeter>();
        GameManager.Instance.OnGameEnd += StopSpawning;
        StartCoroutine("TimedSpawn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TimedSpawn()
    {
        if(_spawnedCitizenCount == 0)
        {
            yield return new WaitForSeconds(5f);
        }
        SpawnCitizen();
        float timeMultiplier = 1f - (_attentionMeter.CurrentAttention / _attentionMeter.MaxAttentionValue) + 0.1f;
        //print(Time.time);
        yield return new WaitForSeconds(10f * timeMultiplier);
        if (_spawnedCitizenCount < _maxSpawnedCitizens)
        {
            StartCoroutine("TimedSpawn");
        }

    }

    private void StopSpawning()
    {
        StopCoroutine("TimedSpawn");
    }

    public void SpawnCitizen()
    {
        int randValue = Random.Range(0, _citizenCharacterModelPrefabs.Count);
        
        Citizen citizenBase = Instantiate(_citizenBasePrefab, GetRandomSpawnPoint().position, Quaternion.identity);
        GameObject citizenCharacterModel = Instantiate(_citizenCharacterModelPrefabs[randValue], citizenBase.transform.position, Quaternion.identity, citizenBase.gameObject.transform);
        citizenBase.SetCitizenVariables(GetCitizenGoalPosition(), _citizenLookAtTarget, false);
        _spawnedCitizens.Add(citizenBase);
        _spawnedCitizenCount += 1;
    }

    public void CitizenCheer()
    {
        foreach(Citizen c in _spawnedCitizens)
        {
            c.TriggerEndEmote();
        }
    }

    //public void SpawnHeroCitizen()
    //{
    //    int randValue = Random.Range(0, _citizenCharacterModelPrefabs.Count);

    //    Citizen citizenBase = Instantiate(_citizenBasePrefab, _spawnPoint_Hero.position, Quaternion.identity);
    //    GameObject citizenCharacterModel = Instantiate(_citizenCharacterModelPrefabs[randValue], citizenBase.transform.position, Quaternion.identity, citizenBase.gameObject.transform);
    //    citizenBase.SetCitizenVariables(_goalPoint_Hero.position, _citizenLookAtTarget, true);
    //}

    private Transform GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }

    private Vector3 GetCitizenGoalPosition()
    {
        Vector3 randOffset = Random.insideUnitSphere * 1f;
        randOffset.y = 0f;
        return _goalPoints[Random.Range(0, _goalPoints.Count)].position + randOffset;
    }
}

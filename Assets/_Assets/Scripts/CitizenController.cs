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

    private AttentionMeter _attentionMeter;

    // Start is called before the first frame update
    void Start()
    {
        _attentionMeter = FindObjectOfType<AttentionMeter>();
        StartCoroutine(TimedSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TimedSpawn()
    {
        SpawnCitizen();
        float a = 1f - (_attentionMeter.CurrentAttention / _attentionMeter.MaxAttentionValue) + 0.2f;
        print(Time.time);
        yield return new WaitForSeconds(15 * a);
        
        StartCoroutine(TimedSpawn());
    }

    public void SpawnCitizen()
    {
        int randValue = Random.Range(0, _citizenCharacterModelPrefabs.Count);
        
        Citizen citizenBase = Instantiate(_citizenBasePrefab, GetRandomSpawnPoint().position, Quaternion.identity);
        GameObject citizenCharacterModel = Instantiate(_citizenCharacterModelPrefabs[randValue], citizenBase.transform.position, Quaternion.identity, citizenBase.gameObject.transform);
        citizenBase.SetCitizenVariables(GetCitizenGoalPosition(), _citizenLookAtTarget);
    }

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

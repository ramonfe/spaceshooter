using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _enemyContainer;
    [SerializeField]
    GameObject[] powerUps;
    bool _stopSpawing = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawing == false)
        {
            Vector3 PrefabPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, PrefabPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }
    IEnumerator SpawnTripleShotRoutine()
    {
        while (_stopSpawing == false)
        {
            Vector3 PrefabPosition = new Vector3(Random.Range(-8, 8), 7, 0);
            int powerUpRandom = Random.Range(0, 3);
            Instantiate(powerUps[powerUpRandom], PrefabPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawing = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    [System.Serializable]
    public class wave
    {
        public string Name;
        public Transform enemy;
        public int Count;
        public float rate;

    }

    public wave[] waves;
    private int nextWave = 0;

    [SerializeField] private Transform[] spawnpoints;

    [SerializeField] private bool IsPlayerTower;

    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountDown;

    [SerializeField] private SpawnState state = SpawnState.COUNTING;

    private float searchCountDown = 1f;
    void Start()
    {
        Time.timeScale = 1f;
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (EnemyIsAlive() == false)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                Debug.Log(waves[nextWave].Name);
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    private bool EnemyIsAlive()
    {

        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return false;
            }
        }

        // constant spawn kleine aanpassing
        return false;
    }
    private IEnumerator SpawnWave(wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.Count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    private void WaveCompleted()
    {

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }

    }
    private void SpawnEnemy(Transform _enemy)
    {
        if (spawnpoints.Length == 0)
        {
            Debug.Log("voeg de tering spawnpoints toe retard");
        }
        //Debug.Log("skskksksskks " + _enemy.name);
        Transform _sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
        if (_enemy.gameObject.GetComponent<Unittag>() != null)
        {
            string tag = _enemy.gameObject.GetComponent<Unittag>().Tag;

            GameObject obj = GenericObjectPool.Instance.SpawnFromPool(tag, _sp.position, _sp.rotation);

            if (IsPlayerTower != true)
            {
                obj.GetComponent<steering.PlayUnit>().bs = (steering.BehaviorStatus)Random.Range(0, 6);
            }
            else
            {
                switch (BehaviorSelector.Instance.CurrentBehavior)
                {
                    case "agro":
                        obj.GetComponent<steering.PlayUnit>().bs = steering.BehaviorStatus.AGGRESSIVE;
                        break;
                    case "def":
                        obj.GetComponent<steering.PlayUnit>().bs = steering.BehaviorStatus.DEFENSIVE;
                        break;
                    case "loyal":
                        obj.GetComponent<steering.PlayUnit>().bs = steering.BehaviorStatus.LOYAL;
                        break;
                    case "GuardA":
                        obj.GetComponent<steering.PlayUnit>().bs = steering.BehaviorStatus.GUARD_A;
                        break;
                    case "GuardB":
                        obj.GetComponent<steering.PlayUnit>().bs = steering.BehaviorStatus.GUARD_B;
                        break;
                    case "wander":
                        obj.GetComponent<steering.PlayUnit>().bs = steering.BehaviorStatus.WANDER;
                        break;
                    default:
                        break;
                }
                
            }
        }
        
    }
}

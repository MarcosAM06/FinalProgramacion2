using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Enemy1,
    Enemy2
}

public class Spawner : MonoBehaviour
{
    public EnemyType enemyToSpawn;
    public bool SpawnAtStart = false;
    public bool rotateTowardsPlayer = true;
    public float SpawnDelay = 10f;

    public GameObject P_Enemy1;
    public GameObject P_Enemy2;

    [SerializeField] int enemiesToSpawn = 1;
    public float Frecuency = 10f;

    int enemiesSpawned = 0;

    private void Start()
    {
        if (SpawnAtStart)
            StartCoroutine(SpawnEnemies());
        else
            StartCoroutine(DelayedSpawn(SpawnDelay));
    }

    public void Spawn()
    {
        GameObject toSpawnPrefab = null;

        switch (enemyToSpawn)
        {
            case EnemyType.Enemy1:
                toSpawnPrefab = P_Enemy1;
                break;
            case EnemyType.Enemy2:
                toSpawnPrefab = P_Enemy2;
                break;
            default:
                break;
        }

        var instanciado = Instantiate(toSpawnPrefab, transform.position, Quaternion.identity);

        Player player = FindObjectOfType<Player>();
        if (rotateTowardsPlayer && player != null)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            dirToPlayer.y = 0;
            instanciado.transform.forward = dirToPlayer;
        }

        enemiesSpawned++;
    }

    IEnumerator DelayedSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < enemiesToSpawn)
        {
            Spawn();
            yield return new WaitForSeconds(Frecuency);
        }
    }
}

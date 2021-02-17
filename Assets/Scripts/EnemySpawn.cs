using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{ // Sheldon McKinley and Miguel Suarez script.
    public float spawnTime = 0.1f;
    public float Distance;
    private GameObject Player;

    // Enemy spawns functions.
    public GameObject enemies;
    public int spawnCap;
    public Transform[] possibleSpawns;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        { 
            InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
            for (int i = 1; i <= spawnCap; i++)
            {
                SpawnEnemy();
            }
            CancelInvoke("SpawnEnemy");
        }
    }
    private void SpawnEnemy()
    {
        Vector3 TargetPos;
        TargetPos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)); // Adjust the Random.Range values to fit the size of the maps.
        TargetPos += transform.position;
        if (Vector3.Distance(TargetPos, Player.transform.position) >= Distance)
        {
            int spawnIndex = Random.Range(0, possibleSpawns.Length);
            Instantiate(enemies, TargetPos,
            possibleSpawns[spawnIndex].rotation);
        }
    }
}

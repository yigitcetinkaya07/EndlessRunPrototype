using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2f;
    public float repeatRate = 3f;
    private PlayerController playerControllerScript;
    private int spawnIndex;
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        if (playerControllerScript.isplayerReady)
        {
            spawnIndex = Random.Range(0, 4);
            if (!playerControllerScript.gameOver)//Oyun bitmediyse
            {
                Instantiate(obstaclePrefab[spawnIndex], spawnPos, obstaclePrefab[spawnIndex].transform.rotation);
            }
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] NPCs;
    private float spawnRate = 10f;
    private int whatToSpawn;
    public float nextSpawn = 0f;
    public int spawnDirection;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            whatToSpawn = Random.Range(1, NPCs.Length + 1);
            Debug.Log("Spawning: " + whatToSpawn);
            GameObject NPC = Instantiate(NPCs[whatToSpawn - 1], transform.position, Quaternion.identity);
            NPC.GetComponent<NPCController>().setDirection(spawnDirection);
            nextSpawn = Time.time + spawnRate;
        }
    }
}

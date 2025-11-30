using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private MonsterA monsterA;
    [SerializeField] private int currentSpawnNumber;
    [SerializeField] private int maxSpawnNumber;

    private void Awake() 
    {
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        int random = Random.Range(0, transforms.Length);
        MonsterA monster = Instantiate(monsterA, transforms[random].position, Quaternion.identity);
        if (currentSpawnNumber < maxSpawnNumber)
        {
            Invoke("SpawnMonster", 2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnData", menuName = "Custom/EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject prefab;
    public int weight;
}

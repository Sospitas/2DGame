using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum E_EnemyType : uint
{
    None,
    Melee,
    Ranged,

    Max
};

[System.Serializable]
public struct EnemyData
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public int Health;
    [SerializeField]
    public int Damage;
}

[System.Serializable]
public class EnemyTypes
{
    public EnemyData[] types;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    private GameObject SpawnedEnemy;

    private void Awake()
    {
        EnemyData spawnData = GetEnemyData("Melee");
        SpawnRandomEnemy();
        SetEnemyData(spawnData);
    }

    EnemyData GetEnemyData(string EnemyName)
    {
        EnemyData enemyData = new EnemyData();

        string assetPath = "EnemyJson";
        TextAsset jsonAsset = Resources.Load(assetPath) as TextAsset;
        string jsonData = jsonAsset.text;

        EnemyTypes enemyTypes = JsonUtility.FromJson<EnemyTypes>(jsonData);

        foreach(EnemyData data in enemyTypes.types)
        {
            if (data.Name.Equals(EnemyName))
                return data;
        }

        Debug.Log("Failed to find Enemy Data with name " + EnemyName);
        return enemyData;
    }

    void SpawnRandomEnemy()
    {
        if(EnemyPrefab == null)
        {
            Debug.LogError("Enemy Spawner: Enemy Prefab is null. Please set up spawner correctly");
            return;
        }

        SpawnedEnemy = GameObject.Instantiate(EnemyPrefab);
        SpawnedEnemy.transform.position = new Vector3(1.28f * 5, 1.28f * 5, 0);
    }

    void SetEnemyData(EnemyData data)
    {
        if (!SpawnedEnemy)
        {
            Debug.Log("SpawnedEnemy is null");
            return;
        }

        Enemy EnemyData = SpawnedEnemy.GetComponent<Enemy>();
        if(EnemyData)
        {
            EnemyData.SetEnemyName(data.Name);
            EnemyData.SetEnemyDamage(data.Damage);
        }
        HealthComp EnemyHealth = SpawnedEnemy.GetComponent<HealthComp>();
        if(EnemyHealth)
        {
            EnemyHealth.SetMaxHealth(data.Health);
        }
    }
}

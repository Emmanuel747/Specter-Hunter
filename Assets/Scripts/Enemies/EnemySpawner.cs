using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop() {
        while (enemyCount < 10) {
            xPos = Random.Range(-30, -20);
            zPos = Random.Range(20, 30);

            Instantiate(enemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(10f);
            enemyCount += 1;
        }
    }
}

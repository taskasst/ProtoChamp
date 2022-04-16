using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject enemy;

    private int enemyCount = 0;
    private int maxEnemy = 100;

    private float minPos = -60;
    private float maxPos = 60;
    private float y = 4;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GenerateEnemy()
    {
        while (true)
        {
            int newEnemyCount = Random.Range(1, 5);
            for (int i = 0; i < newEnemyCount; i++)
            {
                GameObject newEnemy = Instantiate(enemy);
                newEnemy.transform.position = new Vector3(
                    Random.Range(minPos, maxPos),
                    Random.Range(0, 100) < 5 ? y : -0.5f,
                    Random.Range(minPos, maxPos));
            }
            enemyCount += newEnemyCount;
            yield return new WaitForSeconds(3);
        }
    }

    public void EliminateEnemy()
    {
        enemyCount--;
    }
}

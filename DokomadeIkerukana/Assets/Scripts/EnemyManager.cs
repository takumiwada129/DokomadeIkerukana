using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject enemyInstance = null;
    [SerializeField]
    private GameObject[] enemyPrefabs;
    // Hierarchyの親
    [SerializeField]
    GameObject parentPrefab;
    float time = 0;
    int enemyCount = 0;

    public bool isStart = false;
    public bool isFinish = false;

    // isStart is called before the first frame update
    void Start()
    {
        isStart = false;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart && !isFinish)
        {
            generateEnemy();
        }
    }

    void generateEnemy()
    {
        time += Time.deltaTime;
        int spawonPosition=1;
        if(time >= 6)
        {
            time = 0;
            ++enemyCount;
            int enemyNo = Random.Range(0, 2);
            enemyInstance = Instantiate(enemyPrefabs[enemyNo], parentPrefab.transform) as GameObject;
            enemyInstance.name = "enemy_"+ enemyNo + "_" + enemyCount.ToString();
            spawonPosition = Random.Range(0, 2);
            Vector3 scale = transform.localScale;
            if(spawonPosition == 0)
            {
                enemyInstance.transform.position = new Vector3(-10, Random.Range(7f, 3f), 0);
                // enemyInstance.transform.position += new Vector3(1f, 0.2f, 0);
                scale.x = -1;    // 向きを反転させる
                Debug.Log("e_l");
            }
            if(spawonPosition == 1)
            {
                enemyInstance.transform.position = new Vector3(10, Random.Range(7f, 3f), 0);
                // enemyInstance.transform.position += new Vector3(-1f, -0.2f, 0);
                scale.x = 1;
                Debug.Log("e_r");
            }
            enemyInstance.transform.localScale = new Vector3(scale.x, 1, 0);
        }
    }
}

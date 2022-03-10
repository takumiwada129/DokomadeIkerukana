using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameObject itemInstance = null;
    [SerializeField]
    private GameObject[] itemPrefabs;
    // Hierarchyの親
    [SerializeField]
    GameObject parentPrefab;
    float time = 0;
    int itemCount = 0;

    public bool isStart = false;
    public bool isFinish = false;

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
            generateItem();
        }
    }

    void generateItem()
    {
        time += Time.deltaTime;
        int spawonPosition=1;
        if(time >= 9)
        {
            time = 0;
            ++itemCount;
            int itemNo = Random.Range(0, 3);
            itemInstance = Instantiate(itemPrefabs[itemNo], parentPrefab.transform) as GameObject;
            itemInstance.name = "item_"+ itemNo + "_" + itemCount.ToString();
            spawonPosition = Random.Range(0, 2);
            Vector3 scale = transform.localScale;
            if(spawonPosition == 0)
            {
                itemInstance.transform.position = new Vector3(-10, Random.Range(7f, 3f), 0);
                // itemInstance.transform.position += new Vector3(Time.deltaTime * 1f, 0, 0);
                scale.x = -0.7f;    // 向きを反転させる
                scale.y = 0.7f;
                Debug.Log("i_l");
            }
            if(spawonPosition == 1)
            {
                itemInstance.transform.position = new Vector3(10, Random.Range(7f, 3f), 0);
                // itemInstance.transform.position += new Vector3(Time.deltaTime * -1f, 0, 0);
                scale.x = 0.7f;
                scale.y = 0.7f;
                Debug.Log("i_r");
            }
            if(itemNo == 0)
            {
                itemInstance.tag = "ItemBallon";
                if(spawonPosition == 0)scale.x = -1.5f;
                if(spawonPosition == 1)scale.x = 1.5f;
                scale.y = 1.5f;
            }
            itemInstance.transform.localScale = new Vector3(scale.x, scale.y, 0);
        }
    }
}

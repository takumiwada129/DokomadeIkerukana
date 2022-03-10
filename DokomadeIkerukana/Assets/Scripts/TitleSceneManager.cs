using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    void awake()
    {
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveMainScene()
    {
        //SceneChanger.instance.LoadScene("MainScene");
        SceneManager.LoadScene("MainScene");

    }
}

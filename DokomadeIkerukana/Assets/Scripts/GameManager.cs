using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  // 仮

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Timmer;
    public float limmitTime = 10f;
    public float startTextCount = 1f;

    [SerializeField]
    public TextMeshProUGUI distance;
    public float distanceCount = 0f;


    [SerializeField]
    public Image Background_ground;
    [SerializeField]
    public Image Background_sky_1;
    [SerializeField]
    public Image Background_sky_2;

    [SerializeField]
    public Avatar avatar;
    [SerializeField]
    public EnemyManager enemy;
    [SerializeField]
    public ItemManager item;

    float backGroundScrollSpeed = 1f;
    public bool isAccelerateBackGroundScrollSpeed = false;

    public bool isFinish = false;

    public enum GameStatEnum
    {
        ground,
        start,
        sky,
        finish
    }
    public GameStatEnum gameStateEnum = GameStatEnum.ground;

    // Start is called before the first frame update
    void Start()
    {
        gameStateEnum = GameStatEnum.ground;
        Timmer.text = limmitTime.ToString("f0");
    }

    // Update is called once per frame
    void Update()
    {
        ManagementGameState();
    }

    public void ManagementGameState()
    {
        switch(gameStateEnum)
        {
            case GameStatEnum.ground:
                avatar.SwitchGameState("g");
                TimeCount();
            break;
            case GameStatEnum.start:
                StartCount();
            break;
            case GameStatEnum.sky:
                DistanceCount();
                ScrollBackground();
                avatar.SwitchGameState("s");
                enemy.isStart = true;
                item.isStart = true;
                Result();
                // デバッグ用
                if (Input.GetKey(KeyCode.Z))
                    SceneManager.LoadScene("TitleScene");
            break;
            case GameStatEnum.finish:
                enemy.isFinish = true;
                item.isFinish = true;
                // 仮
                Timmer.text = "Finish!";
                if (Input.GetKey(KeyCode.Space))
                    SceneManager.LoadScene("TitleScene");
            break;
        }
    }

    void TimeCount()
    {
        limmitTime -= Time.deltaTime;
        Timmer.text = limmitTime.ToString("f0");
        if(limmitTime <= 0)
        {
            Timmer.text = "0";
            gameStateEnum = GameStatEnum.start;
        }
    }

    void StartCount()
    {
        Timmer.text = "Start!!";
        startTextCount -= Time.deltaTime;
        if(startTextCount <= 0)
        {
            // Timmer.SetActive(false);
            Timmer.text = "";
            gameStateEnum = GameStatEnum.sky;
        }
    }

    void DistanceCount()
    {
        distanceCount += Time.deltaTime;
        distance.text = distanceCount.ToString("f1") + "m";
    }

    float acceralateTime = 1f;
    void ScrollBackground()
    {
        if(isAccelerateBackGroundScrollSpeed)
        {
            isAccelerateBackGroundScrollSpeed = false;
            backGroundScrollSpeed = 3.0f;// あと背景スクロールだけでなく、距離カウントも早くする
            acceralateTime -= Time.deltaTime;
            if(acceralateTime <= 0)
            {
                // 元に戻す
                backGroundScrollSpeed = 1.0f;
                return;
            }
        }

        Background_ground.gameObject.transform.position -= new Vector3(0, Time.deltaTime * backGroundScrollSpeed, 0);
        Background_sky_1.gameObject.transform.position -= new Vector3(0, Time.deltaTime * backGroundScrollSpeed, 0);
        Background_sky_2.gameObject.transform.position -= new Vector3(0, Time.deltaTime * backGroundScrollSpeed, 0);
        // Background_ground.sortingOrder = -10;
        // Background_sky_1.sortingOrder = -10;
        // Background_sky_2.sortingOrder = -10;

        if(Background_ground.gameObject.transform.position.y <= -19.20f)
        {
            Background_ground.gameObject.SetActive(false);
        }
        if(Background_sky_1.gameObject.transform.position.y <= -19.20f)
        {
            Background_sky_1.gameObject.transform.position = new Vector3(0, 19.20f, -10);
        }
        if(Background_sky_2.gameObject.transform.position.y <= -19.20f)
        {
            Background_sky_2.gameObject.transform.position = new Vector3(0, 19.20f, -10);
        }
    }

    void Result()
    {
        if(isFinish)
        {
            gameStateEnum = GameStatEnum.finish;
        }
    }
}

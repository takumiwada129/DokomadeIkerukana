using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    GameObject ballonInstance = null;

    [SerializeField]
    private GameObject[] ballonPrefabs;

    // Hierarchyの親
    [SerializeField]
    GameObject parentPrefab;

    List<GameObject> avatarsBallonList = new List<GameObject>();

    bool createBallonFlag = true;
    bool onMouseFlag = false;
    bool onAvatarFlag = false;

    private Vector3 mouse;
    private Vector3 target;

    // // 補間の強さ（0f～1f） 。0なら追従しない。1なら遅れなしに追従する。
    [SerializeField, Range(0f, 1f)]
    private float followStrength;

    public float speed;

    public RectTransform canvaGameRect;  //座標変換したいキャンバス

    Vector3 pos = new Vector3(0, 0, 0);


    Vector3 previousPos = new Vector3(0, 0, 0);
    Vector3 currentPos = new Vector3(0, 0, 0);
    public float sensitivity = 100f;
    public const float LOAD_WIDTH = 6f;
    public const float LOAD_HEIGHT = 12f;
    public float MOVE_MAX = 5000f;

    int balloonCount = 0;
    public Vector3 defaultScale = Vector3.zero;


    public enum GameStatEnum
    {
        ground,
        sky
    }
    public GameStatEnum gameState = GameStatEnum.ground;

    bool isGround = true; // true: g // false: f


    [SerializeField]
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isGround = true;
        speed = 0.1f;
        defaultScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGround)
        {
            if(Input.GetMouseButton(0))
            {
                CreateBallon();
                CrateBigBallon();
            }

            if(Input.GetMouseButtonUp(0))
            {
                if(!onMouseFlag)
                {
                    // 長押し防止
                    createBallonFlag = true;
                }
            }
        }
        else
        {
            // スワイプによるアバターの移動処理
            if (Input.GetMouseButtonDown(0))
            {
                previousPos = Input.mousePosition;
                previousPos.z = -10;
            }
            // 敗北条件の風船の数を判定
            checkHaveBallonNum();
        }
    }

    public void SwitchGameState(string state)
    {
        switch(state)
        {
            case "g":
                isGround = true;
            break;
            case "s":
                isGround = false;
            break;
        }
    }

    // タップで小さい風船を生成
    void CreateBallon()
    {
        // Debug.Log("createBallonFlag create: " + createBallonFlag);
        if(!createBallonFlag) return;

        createBallonFlag = false;
        ++balloonCount;
        int ballonNo = Random.Range(0, 5);
        ballonInstance = Instantiate(ballonPrefabs[ballonNo], parentPrefab.transform) as GameObject;
        avatarsBallonList.Add(ballonInstance);
        ballonInstance.name = "Ballon_" + balloonCount.ToString();
        ballonInstance.tag = "AvatarsBallon";
        ballonInstance.transform.position = new Vector2(Random.Range(-3.2f, 3.2f),Random.Range(-6f, -4f));
        ballonInstance.transform.localScale = new Vector3(10,10,0);
    }

    int bigcount;
    // 長押しで大きい風船を生成
    void CrateBigBallon()
    {
    //     if(!onAvatarFlag)
    //     {
    //         ++bigcount
    //         if(bigcount >= 2)
    //         {
    //             for (int i = 0; i < avatarsBallonList.Count; i++)
    //             {
                    
    //                 avatarsBallonList[avatarsBallonList.Count - 1];
    //             }
    //         }
    //     }
    }

    public void Move()
    {
        // skyの時だけ移動可能
        if(!isGround)
        {
            // スワイプによる移動距離を取得
            currentPos = Input.mousePosition;
            currentPos.z = -10;
            float diffDistanceX = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;
            float diffDistanceY = (currentPos.y - previousPos.y) / Screen.height * LOAD_HEIGHT;

            diffDistanceX *= sensitivity;
            diffDistanceY *= sensitivity;

            // 次のローカルx座標を設定 ※道の外にでないように
            float newX = Mathf.Clamp(transform.localPosition.x + diffDistanceX, -MOVE_MAX, MOVE_MAX);
            float newY = Mathf.Clamp(transform.localPosition.y + diffDistanceY, -MOVE_MAX, MOVE_MAX);
            transform.localPosition = new Vector3(newX, newY, -10);

            // タップ位置を更新
            previousPos = currentPos;
        }
    }

    //アバターの上に指を載せてるとき
    public void OnMouse()
    {
        onMouseFlag = true;
        createBallonFlag = false;
        Debug.Log("createBallonFlag: " + createBallonFlag);
        onAvatarFlag = true;

    }

    //アバターから指を離したとき
    public void OnExit()
    {
        onMouseFlag = false;
        createBallonFlag = true;
        onAvatarFlag = false;
    }

    void OnMouseDrag()
    {
    }

    void OnMouseUpAsButton()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            // 風船を一つ消す
            for (int i = 0; i < avatarsBallonList.Count; i++)
            {
                //avatarsBallonList.RemoveAt(avatarsBallonList.Count - 1);
                Destroy(avatarsBallonList[avatarsBallonList.Count - 1]);
            }

            Debug.Log("Enemy hit");
            //avatarsBallonList.RemoveAt(0);
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "ItemBallon")
        {
            Debug.Log("itemballon hit");

            // 風船を一つ増やす
            ++balloonCount;
            int ballonNo = Random.Range(0, 5);
            ballonInstance = Instantiate(ballonPrefabs[ballonNo], parentPrefab.transform) as GameObject;
            avatarsBallonList.Add(ballonInstance);
            ballonInstance.name = "Ballon_" + balloonCount.ToString();
            ballonInstance.tag = "AvatarsBallon";
            ballonInstance.transform.position = new Vector2(Random.Range(-3.2f, 3.2f),Random.Range(-6f, -4f));
            ballonInstance.transform.localScale = new Vector3(10,10,0);
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Engine")
        {
            Debug.Log("Engine hit");

            // 背景のスクロールを一瞬加速する
            gameManager.isAccelerateBackGroundScrollSpeed = true;
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Treasurebox")
        {
            Debug.Log("tresurebox hit");

            // アイテムの抽選と獲得。一旦なし
            Destroy(collider.gameObject);
        }
    }

    void checkHaveBallonNum(){
        if(avatarsBallonList.Count == 0)
        {
            gameManager.isFinish = true;
        }
    }
}

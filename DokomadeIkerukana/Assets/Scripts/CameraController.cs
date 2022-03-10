using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenForm
{
    Tate,
    Yoko
}

public class CameraController : MonoBehaviour
{
    private Camera cam;
    [SerializeField] Canvas canvas = default;
    ScreenForm screenForm = ScreenForm.Tate;

    // 理想の表示サイズ
    private const float IdealScreenWidth = 1080.0f;
    private const float IdealScreenHeight = 1920.0f;

    private const float PixelPerUnit = 100.0f;

    private static readonly Rect NormalRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

    private void Awake()
    {
        cam = GetComponent<Camera>();

        // 実際の表示画面のアスペクト比
        float aspect = (float)Screen.height / (float)Screen.width;
        // 理想のアスペクト比
        float idealAspect = IdealScreenHeight / IdealScreenWidth;

        cam.orthographicSize = (IdealScreenHeight / 2.0f / PixelPerUnit);

        if(idealAspect > aspect)
        {
            // 画面が横長の時
            screenForm = ScreenForm.Yoko;

            // Canvasの設定
            var canvasScaler = canvas.transform.GetComponent<CanvasScaler>();
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 1.0f;

            // カメラの設定
            // 理想の拡大率
            float idealScale = IdealScreenHeight / Screen.height;
            // viewport rectの幅
            float camWidth= IdealScreenWidth / (Screen.width * idealScale);

            Rect centerRect = NormalRect;
            centerRect.x = (1.0f - camWidth) / 2.0f;
            centerRect.width = camWidth;
            // viewport Rectの設定
            cam.rect = centerRect;
        }
        else
        {
            // 縦長の時
            screenForm = ScreenForm.Tate;

            // Canvasの設定
            var canvasScaler = canvas.transform.GetComponent<CanvasScaler>();
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.matchWidthOrHeight = 0.0f;

            // カメラの設定
            // 理想のアスペクト比との差を求める
            float idealScale = aspect / idealAspect;

            // カメラのサイズを縦の長さに合わせて修正
            cam.orthographicSize *= idealScale;
        }
    }

    private void Start()
    {
        //LayerController.Instance.CanvasScalerFixe(screenForm);
    }
}

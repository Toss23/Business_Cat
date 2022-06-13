using UnityEngine;
using UnityEngine.UI;

public class Adaptive : MonoBehaviour
{
    private enum ScaleMode
    {
        Height, Width
    }

    [Header("Main")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScaleMode scaleMode = ScaleMode.Height;
    [SerializeField] private bool updateEveryFrame = true;

    private CanvasScaler canvasScaler;
    private RectTransform rect;

    private Vector2 referenceResolution;
    private Vector2 currentResolution;
    private float scale;

    private void Awake()
    {
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        rect = canvas.GetComponent<RectTransform>();
    }

    private void Start()
    {
        Resize();
    }

    private void Update()
    {
        if (updateEveryFrame) Resize();
    }

    private void Resize()
    {
        referenceResolution = canvasScaler.referenceResolution;
        currentResolution = rect.rect.size;

        if (scaleMode == ScaleMode.Height)
            scale = currentResolution.y / referenceResolution.y;
        else
            scale = currentResolution.x / referenceResolution.x;

        transform.localScale = Vector3.one * scale;
    }
}

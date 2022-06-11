using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] private int targetFps = 60;
    [SerializeField] private bool VSync = false;

    private TMP_Text text;

    private void Awake()
    {
        QualitySettings.vSyncCount = VSync ? 1 : 0;
        Application.targetFrameRate = targetFps;
    }

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        frames = 0;
        timer = 1f;
    }

    private float timer;
    private int frames;
    private float fps;

    void Update()
    {
        frames++;

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            fps = frames - 1;
            frames = 0;
            timer = 1f;
        }

        text.text = Mathf.Round(fps).ToString();
    }
}

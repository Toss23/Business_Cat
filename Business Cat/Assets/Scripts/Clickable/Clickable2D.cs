using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Clickable2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool IsAndroid { get { return Application.platform == RuntimePlatform.Android; } }
    public static Vector2 ScreenSize { get { return new Vector2(Screen.width, Screen.height); } }

    public bool Touched { get; private set; }
    public bool Moved { get; private set; }
    public float TouchTime { get; private set; }
    public int TouchID { get; private set; }

    public Vector2 TouchPosition { get; private set; }
    public Vector2 TouchPositionDelta { get; private set; }
    public Vector2 TouchPositionDeltaPerUpdate { get; private set; }

    private Vector2 touchPositionBegin, touchPositionPrevious;

    public Touch Touch { get { return GetTouch(TouchID); } }
    public Vector3 TouchPositionUnfixed 
    { 
        get 
        {
            if (IsAndroid)
                return Touch.position;
            else
                return Input.mousePosition;
        } 
    }

    private void Start()
    {
        TouchID = -1;
    }

    private void Update()
    {
        if (Touched)
        {
            TouchPosition = GetTouchPosition();
            TouchTime += Time.deltaTime;

            if (IsAndroid)
            {
                if (Input.touchCount == 1)
                {
                    switch (Touch.phase)
                    {
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            TouchPositionDelta = touchPositionBegin - TouchPosition;
                            TouchPositionDeltaPerUpdate = touchPositionPrevious - TouchPosition;
                            touchPositionPrevious = TouchPosition;
                            break;
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    TouchPositionDelta = touchPositionBegin - TouchPosition;
                    TouchPositionDeltaPerUpdate = touchPositionPrevious - TouchPosition;
                    touchPositionPrevious = TouchPosition;
                }
            }
        }
        else
        {
            TouchTime = 0;
            TouchPositionDelta = Vector2.zero;
            TouchPositionDeltaPerUpdate = Vector2.zero;
        }

        if (Mathf.Abs(TouchPositionDelta.x / ScreenSize.x) >= 0.1f 
            || Mathf.Abs(TouchPositionDelta.y / ScreenSize.y) >= 0.1f)
            Moved = true;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Touched = false;
            OnTouchUp();
            TouchID = -1;
            TouchTime = 0;
            Moved = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsAndroid)
        {
            foreach (Touch touch in Input.touches)
                if (touch.phase == TouchPhase.Began)
                    TouchID = touch.fingerId;
        }

        Moved = false;
        Touched = true;
        TouchPosition = GetTouchPosition();
        touchPositionBegin = TouchPosition;
        touchPositionPrevious = TouchPosition;
        OnTouchDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsAndroid)
        {
            foreach (Touch touch in Input.touches)
                if (touch.phase == TouchPhase.Ended && touch.fingerId == TouchID)
                {
                    if (Moved == false && TouchTime <= 1) OnSingleClick();
                    Touched = false;
                    OnTouchUp();
                    TouchID = -1;
                    TouchPosition = Vector3.zero;
                    break;
                }
        }
        else
        {
            if (Moved == false) OnSingleClick();
            Touched = false;
            OnTouchUp();
            TouchID = -1;
            TouchPosition = Vector3.zero;
        }
    }

    private Vector3 GetTouchPosition()
    {
        Vector3 input;

        if (IsAndroid)
            input = Touch.position;
        else
            input = Input.mousePosition;

        return TouchPositionOnCanvas(input);
    }

    protected abstract void OnTouchDown();

    protected abstract void OnTouchUp();

    protected abstract void OnSingleClick();

    public static Touch GetTouch(int id)
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == id)
                return touch;
        }

        return new Touch();
    }

    public static Vector2 TouchPositionOnCanvas(Vector2 touchPosition)
    {
        Vector3 UISize = new Vector3(1920, 1080);
        return new Vector3((touchPosition.x / ScreenSize.x - 0.5f) * UISize.x, (touchPosition.y / ScreenSize.y - 0.5f) * UISize.y);
    }
}
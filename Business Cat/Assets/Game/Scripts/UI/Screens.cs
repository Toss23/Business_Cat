using UnityEngine;

public class Screens : MonoBehaviour
{
    private enum Direction
    {
        None, Left, Right
    }

    public static Screens Instance { get; private set; }

    [Header("Main")]
    [SerializeField] private float swipeSensetive;
    [SerializeField] private float swipeSpeed;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Screen[] screens;

    private Vector2 positionBegin;
    private float touchPositionBegin;

    private Screen currentScreen;
    private Screen nextScreen;
    private bool switchingScreen;

    private void Awake()
    {
        Instance = this;

        foreach (Screen screen in screens)
        {
            Vector2 tabPosition = canvas.rect.size * screen.Position;
            screen.Content.transform.localPosition = tabPosition;
            screen.Content.SetActive(true);

            if (screen.Position == new Vector2(0, 0))
                currentScreen = screen;
        }

        positionBegin = transform.localPosition;
    }

    private void Update()
    {
        if (switchingScreen == false)
        {
            // Calculate delta touch
            float deltaTouch = 0;
            Direction touchSwipeDirection;
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            touchPositionBegin = touch.position.x;
                            break;
                        case TouchPhase.Ended:
                            deltaTouch = (touchPositionBegin - touch.position.x) / canvas.rect.width;
                            break;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    touchPositionBegin = Input.mousePosition.x;

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    deltaTouch = (touchPositionBegin - Input.mousePosition.x) / canvas.rect.width;
            }

            // Detect swipe
            if (Mathf.Abs(deltaTouch) > swipeSensetive)
            {
                if (deltaTouch > 0)
                    touchSwipeDirection = Direction.Right;
                else
                    touchSwipeDirection = Direction.Left;
            }
            else
            {
                touchSwipeDirection = Direction.None;
            }

            // Open tab on swipe
            if (touchSwipeDirection == Direction.Left)
            {
                if (currentScreen.Swipe == Screen.SwipeDirection.LeftAndRight || currentScreen.Swipe == Screen.SwipeDirection.Left)
                    OnSwipe(touchSwipeDirection);
            }
            else if (touchSwipeDirection == Direction.Right)
            {
                if (currentScreen.Swipe == Screen.SwipeDirection.LeftAndRight || currentScreen.Swipe == Screen.SwipeDirection.Right)
                    OnSwipe(touchSwipeDirection);
            }
        }

        if (switchingScreen == true)
        {
            if (MoveTo(nextScreen))
            {
                currentScreen = nextScreen;
                switchingScreen = false;
            }
        }
    }

    private void OnSwipe(Direction direction)
    {
        Vector2 nextTabPosition = currentScreen.Position + new Vector2(direction == Direction.Left ? -1 : 1, 0);
        Open(Find(nextTabPosition));
    }

    public Screen Find(string identifier)
    {
        foreach (Screen screen in screens)
        {
            if (screen.Identifier == identifier)
                return screen;
        }
        return null;
    }

    private Screen Find(Vector2 position)
    {
        foreach (Screen screen in screens)
        {
            if (screen.Position == position)
                return screen;
        }
        return null;
    }

    public bool MoveTo(Screen screen)
    {
        transform.localPosition = Vector2.MoveTowards(
            transform.localPosition, 
            positionBegin - screen.Position * canvas.rect.size, 
            swipeSpeed * Time.deltaTime);

        Vector2 position = transform.localPosition;
        return position == positionBegin - screen.Position * canvas.rect.size;
    }

    public void Open(Screen screen)
    {
        nextScreen = screen;
        switchingScreen = true;
    }

    public void Open(string identifier)
    {
        Open(Find(identifier));
    }

    public void InstantlyOpen(string identifier)
    {
        Screen screen = Find(identifier);
        transform.localPosition = positionBegin - screen.Position * canvas.rect.size;
        currentScreen = screen;
    }
}

[System.Serializable]
public class Screen
{
    public enum SwipeDirection
    {
        Disable, LeftAndRight, Left, Right
    }

    [SerializeField] private string identifier;
    [SerializeField] private GameObject content;
    [SerializeField] private Vector2 position;
    [SerializeField] private SwipeDirection swipe;

    public string Identifier { get { return identifier; } }
    public GameObject Content { get { return content; } }
    public Vector2 Position { get { return position; } }
    public SwipeDirection Swipe { get { return swipe; } }
}
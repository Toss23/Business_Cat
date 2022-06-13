using UnityEngine;

public class Tabs : MonoBehaviour
{
    private enum Direction
    {
        None, Left, Right
    }

    public static Tabs Instance;

    [Header("Main")]
    [SerializeField] private RaycastScreen screen;
    [SerializeField] private float swipeSensetive;
    [SerializeField] private float swipeSpeed;
    [SerializeField] private Tab[] tabs;

    private Vector2 positionBegin;

    private Tab currentTab;
    private Tab nextTab;
    private bool nextTabFound = false;

    private Direction touchSwipe;
    private float deltaTouch;

    public bool Switching { get { return nextTabFound; } }

    private void Awake()
    {
        Instance = this;

        positionBegin = transform.localPosition;

        foreach (Tab tab in tabs)
        {
            Vector2 tabPosition = screen.CanvasResolution * tab.Position;
            tab.Content.transform.localPosition = tabPosition;
            tab.Content.SetActive(true);

            if (tab.Position == new Vector2(0, 0))
                currentTab = tab;
        }
    }

    private void Update()
    {
        if (screen.Touched && touchSwipe == Direction.None)
        {
            deltaTouch = screen.TouchPositionDelta.x / screen.CanvasResolution.x;
        }
        else
        {
            // Detect delta touch position on finger up
            if (Mathf.Abs(deltaTouch) > swipeSensetive)
            {
                if (deltaTouch > 0)
                    touchSwipe = Direction.Right;
                else
                    touchSwipe = Direction.Left;
            }
            deltaTouch = 0;

            // Move screen on swipe
            if (touchSwipe == Direction.Left)
            {
                if (currentTab.Swipe == Tab.SwipeDirection.LeftAndRight || currentTab.Swipe == Tab.SwipeDirection.Left)
                {
                    OnSwipe(touchSwipe);
                }
                else
                {
                    touchSwipe = Direction.None;
                }
            }
            else if (touchSwipe == Direction.Right)
            {
                if (currentTab.Swipe == Tab.SwipeDirection.LeftAndRight || currentTab.Swipe == Tab.SwipeDirection.Right)
                {
                    OnSwipe(touchSwipe);
                }
                else
                {
                    touchSwipe = Direction.None;
                }
            }
        }
    }

    private void OnSwipe(Direction direction)
    {
        if (nextTabFound == false)
        {
            Vector2 nextTabPosition = currentTab.Position + new Vector2(direction == Direction.Left ? -1 : 1, 0);
            nextTab = Find(nextTabPosition);
            nextTabFound = true;
        }

        if (nextTabFound == true)
        {
            if (MoveTo(nextTab))
            {
                touchSwipe = Direction.None;
                currentTab = nextTab;
                nextTabFound = false;
            }
        }
    }

    public Tab Find(string identifier)
    {
        foreach (Tab tab in tabs)
        {
            if (tab.Identifier == identifier)
                return tab;
        }
        return null;
    }

    private Tab Find(Vector2 position)
    {
        foreach (Tab tab in tabs)
        {
            if (tab.Position == position)
                return tab;
        }
        return null;
    }

    public bool MoveTo(Tab tab)
    {
        transform.localPosition = Vector2.MoveTowards(
            transform.localPosition, 
            positionBegin - tab.Position * screen.CanvasResolution, 
            swipeSpeed * Time.deltaTime);

        Vector2 position = transform.localPosition;
        return position == positionBegin - tab.Position * screen.CanvasResolution;
    }

    public bool InstantlyOpen(Tab tab)
    {
        transform.localPosition = positionBegin - tab.Position * screen.CanvasResolution;
        currentTab = tab;
        return true;
    }
}

[System.Serializable]
public class Tab
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
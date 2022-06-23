using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour
{
    private enum Direction
    {
        None, Left, Right
    }

    [Header("Main")]
    [SerializeField] private float swipeSensetive;
    [SerializeField] private float swipeSpeed;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Tab[] tabs;

    private Vector2 positionBegin;
    private float touchPositionBegin;

    private Tab currentTab;
    private Tab nextTab;
    private bool switchingTab;

    private void Start()
    {
        foreach (Tab tab in tabs)
        {
            Vector2 tabPosition = canvas.rect.size * tab.Position;
            tab.Content.transform.localPosition = tabPosition;
            tab.Content.SetActive(true);

            if (tab.Position == new Vector2(0, 0))
                currentTab = tab;
        }

        positionBegin = transform.localPosition;
    }

    private void Update()
    {
        if (switchingTab == false)
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
                if (currentTab.Swipe == Tab.SwipeDirection.LeftAndRight || currentTab.Swipe == Tab.SwipeDirection.Left)
                    OnSwipe(touchSwipeDirection);
            }
            else if (touchSwipeDirection == Direction.Right)
            {
                if (currentTab.Swipe == Tab.SwipeDirection.LeftAndRight || currentTab.Swipe == Tab.SwipeDirection.Right)
                    OnSwipe(touchSwipeDirection);
            }
        }

        if (switchingTab == true)
        {
            if (MoveTo(nextTab))
            {
                currentTab = nextTab;
                switchingTab = false;
            }
        }
    }

    private void OnSwipe(Direction direction)
    {
        Vector2 nextTabPosition = currentTab.Position + new Vector2(direction == Direction.Left ? -1 : 1, 0);
        Open(Find(nextTabPosition));
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
            positionBegin - tab.Position * canvas.rect.size, 
            swipeSpeed * Time.deltaTime);

        Vector2 position = transform.localPosition;
        return position == positionBegin - tab.Position * canvas.rect.size;
    }

    public void Open(Tab tab)
    {
        nextTab = tab;
        switchingTab = true;
    }

    public void Open(string identifier)
    {
        Open(Find(identifier));
    }

    public void InstantlyOpen(string identifier)
    {
        Tab tab = Find(identifier);
        transform.localPosition = positionBegin - tab.Position * canvas.rect.size;
        currentTab = tab;
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
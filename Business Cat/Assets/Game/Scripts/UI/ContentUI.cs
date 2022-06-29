using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentUI : MonoBehaviour
{
    public enum State
    {
        Active, Hidden
    }

    private static List<ContentUI> List;

    [SerializeField] private string contentName;
    [SerializeField] private State state;
    [SerializeField] private GameObject content;
    [SerializeField] private bool haveImage = false;

    private Image image;

    public string ContentName { get { return contentName; } }
    public State CurrentState { get { return state; } }

    private void Awake()
    {
        if (List == null) List = new List<ContentUI>();
        if (haveImage) image = GetComponent<Image>();
        List.Add(this);
        Hide();
    }

    public void Show()
    {
        content.SetActive(true);
        if (haveImage) image.enabled = true;
        state = State.Active;
    }

    public void Hide()
    {
        content.SetActive(false);
        if (haveImage) image.enabled = false;
        state = State.Hidden;
    }

    public void ChangeState()
    {
        if (state == State.Active)
            Hide();
        else
            Show();
    }

    public static void HideAll()
    {
        if (List != null)
        {
            foreach (ContentUI contentUI in List)
                contentUI.Hide();
        }
    }

    public static void DeleteList()
    {
        List.Clear();
        List = null;
    }
}
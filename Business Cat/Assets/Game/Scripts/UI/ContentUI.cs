using System.Collections.Generic;
using UnityEngine;

public class ContentUI : MonoBehaviour
{
    public enum State
    {
        Active, Hidden
    }

    private static List<ContentUI> List;

    [SerializeField] private State state;
    [SerializeField] private GameObject content;

    public State CurrentState { get { return state; } }

    private void Awake()
    {
        if (List == null) List = new List<ContentUI>();
        List.Add(this);
        Hide();
    }

    public void Show()
    {
        content.SetActive(true);
        state = State.Active;
    }

    public void Hide()
    {
        content.SetActive(false);
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
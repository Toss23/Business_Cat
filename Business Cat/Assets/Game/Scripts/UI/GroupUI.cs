using UnityEngine;

public class GroupUI : MonoBehaviour
{
    [SerializeField] private int defualt = 0;
    [SerializeField] private ContentUI[] contents;

    private void Start()
    {
        Show(defualt);
    }

    public void HideAll()
    {
        foreach (ContentUI content in contents)
            content.Hide();
    }

    public void Show(int index)
    {
        HideAll();
        contents[index].Show();
    }

    public void Show(string name)
    {
        HideAll();
        foreach (ContentUI content in contents)
        {
            if (content.ContentName == name)
            {
                content.Show();
                break;
            }
        }
    }
}
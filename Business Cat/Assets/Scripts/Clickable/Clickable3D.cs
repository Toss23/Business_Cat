using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Clickable3D : MonoBehaviour
{
    [Header("Show UI content onClick")]
    [SerializeField] private bool haveUI = false;
    [SerializeField] private ContentUI contentUI;

    /// <summary>
    /// Base method requre for open UI content
    /// </summary>
    public virtual void OnClick()
    {
        if (haveUI == true && contentUI != null)
        {
            contentUI.Show();
        }
    }
}
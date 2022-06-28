using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;

    public void SetItem(Item item)
    {
        nameText.text = item.Identifier;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text attributeText;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetItem(Item item)
    {
        nameText.text = item.DisplayName;

        if (item.Attribute == Attribute.None)
            attributeText.text = "---";
        else
            attributeText.text = "+" + item.AttributeValue + " " + item.Attribute;
    }
}
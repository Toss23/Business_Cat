using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text priceText;

    public void SetEvent(Event e)
    {
        nameText.text = e.DisplayName + " (Prestige " + e.Prestige + ")";
        descriptionText.text = e.ShortDescription;
        priceText.text = e.Price + " CF";
    }
}
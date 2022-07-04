using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EventView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text priceText;

    private Event currentEvent;
    private Button button;
    private Screens screens;
    private Currency currency;

    private void Awake()
    {
        button = GetComponent<Button>();
        screens = Screens.Instance;
        currency = Currency.Find(Currency.Money);
    }

    public void SetEvent(Event e)
    {
        nameText.text = e.DisplayName + " (Prestige " + e.Prestige + ")";
        descriptionText.text = e.ShortDescription;
        priceText.text = e.Price > 0 ? e.Price + " " + Currency.Money : "";

        currentEvent = e;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (currency.Remove(currentEvent.Price))
        {
            EventAction.Instance.StartEvent(currentEvent);
            screens.InstantlyOpen("EventAction");
        }
    }
}
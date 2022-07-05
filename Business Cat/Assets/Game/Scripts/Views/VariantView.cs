using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VariantView : MonoBehaviour
{
    [SerializeField] private Attributes attributes;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text resultText;

    private Button button;
    private Variant currentVariant;

    private int percent;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetVariant(Variant variant)
    {
        descriptionText.text = variant.Description;
        percent = attributes.GetVariantChance(variant);
        switch (variant.Result)
        {
            case Variant.ResultVariant.None:
                resultText.text = "";
                break;
            case Variant.ResultVariant.Money:
                resultText.text = "+" + variant.Value + " " + Currency.Money;
                break;
            case Variant.ResultVariant.Attribute:
                resultText.text = "+" + variant.Value + " " + variant.Attribute;
                break;
            case Variant.ResultVariant.Company:
                if (variant.Value >= 0)
                    resultText.text = "The shares of company " + variant.CompanyName + " may increase in price";
                else
                    resultText.text = "The shares of company " + variant.CompanyName + " may decrease in price";
                break;
        }

        if (variant.Result != Variant.ResultVariant.None && percent < 100)
            resultText.text += " (" + percent + "%)";

        currentVariant = variant;
    }

    private void OnClick()
    {
        int random = Random.Range(1, 101);
        Debug.Log("[Variant] Check: " + random + " <= " + percent);
        bool success = random <= percent;
        if (success)
        {
            switch (currentVariant.Result)
            {
                case Variant.ResultVariant.Money:
                    Currency money = Currency.Find(Currency.Money);
                    if (money != null) money.Add(currentVariant.Value);
                    break;
                case Variant.ResultVariant.Attribute:
                    attributes.Add(currentVariant.Attribute, currentVariant.Value);
                    break;
                case Variant.ResultVariant.Company:
                    Company company = Companies.Instance.Find(currentVariant.CompanyName);
                    if (company != null) company.PriceTrend += currentVariant.Value;
                    break;
            }

            EventAction.Instance.VariantSelected(true);
        }
        else
        {
            EventAction.Instance.VariantSelected(false);
        }
    }
}
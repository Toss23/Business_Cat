using TMPro;
using UnityEngine;

public class AttributesView : MonoBehaviour
{
    [SerializeField] private Attributes attributes;
    [SerializeField] private TMP_Text[] texts;
    private int attributesCount;

    private void Start()
    {
        attributesCount = System.Enum.GetNames(typeof(Attribute)).Length;
    }

    private void Update()
    {
        for (int i = 1; i < attributesCount; i++)
        {
            if (texts[i - 1] != null)
                texts[i - 1].text = attributes.Get((Attribute)i) + " " + (Attribute)i;
        }
    }
}
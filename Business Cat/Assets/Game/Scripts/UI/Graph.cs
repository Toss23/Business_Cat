using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [HideInInspector] public int[] Values;

    [Header("Main")]
    [SerializeField] private Vector2 center = Vector2.zero;
    [SerializeField] private Vector2 margins = Vector2.zero;

    [Header("Columns")]
    [SerializeField] private int columnCount = 10;
    [SerializeField] [Range(0.1f, 1)] private float columnWidth = 1;

    [Header("Axes")]
    [SerializeField] [Range(0, 1)] private float axisMargins = 0;
    [SerializeField] [Range(0, 10)] private float axisWidth = 10;

    [Header("Lines")]
    [SerializeField] [Range(0, 1)] private float lineMargins = 0;
    [SerializeField] [Range(0, 10)] private float lineWidth = 10;

    [Header("Texts")]
    [SerializeField] [Range(0.05f, 0.2f)] private float textSize;
    [SerializeField] private TMP_Text minPriceText;
    [SerializeField] private TMP_Text maxPriceText;
    [SerializeField] private TMP_Text lastPriceText;

    [Header("Colors")]
    [SerializeField] private Color lowPriceColor = Color.red;
    [SerializeField] private Color highPriceColor = Color.green;
    [SerializeField] private Color axisColor = Color.gray;
    [SerializeField] private Color lineColor = new Color(1, 0, 0.25f, 1);
    [SerializeField] private Color lastPriceLineColor = Color.yellow;

    private float width, height;

    private RectTransform[] columns;
    private RectTransform lastValueLine;

    private int minValue;
    private int maxValue;
    private int lastValue;
    private int deltaValue;

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        width = rect.rect.width;
        height = rect.rect.height;

        columns = new RectTransform[columnCount];
        Values = new int[columnCount];
        for (int i = 0; i < columnCount; i++)
            Values[i] = 0;
    }

    private void Start()
    {
        Initialize();
        UpdateColumns();
    }

    private void Initialize()
    {
        FindValues();
        CreateAdditionalAxes();
        CreateCoordinateAxes();
        CreateColumns();
        TextOnTop();
    }

    /// <summary>
    /// Set Values and update columns
    /// </summary>
    public void UpdateColumns()
    {
        FindValues();
        UpdateTexts();
        UpdateLastAxes();

        for (int i = 0; i < columnCount; i++)
        {
            UpdateHeightColumn(i, Values[i]);
            UpdateColorColumn(i);
        }
    }

    private void CreateAdditionalAxes()
    {
        Vector2 position;
        Vector2 size = new Vector2(width * (1 - axisMargins) * (1 - lineMargins), lineWidth);
        float columnHeight = (lastValue - minValue + deltaValue * 0.1f) / (deltaValue * 1.1f);

        // Maximum
        position.x = -width * lineMargins / 2;
        position.y = (margins.y / 2 + center.y - 0.5f + (1 - margins.y)) * height - lineWidth / 2;
        CreateAxis("Max Value Line", position, size, lineColor);

        maxPriceText.transform.localPosition = position - new Vector2(0, size.y / 2);
        maxPriceText.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, height * textSize);

        // Minimum
        position.x = -width * lineMargins / 2;
        position.y = (margins.y / 2 + center.y - 0.5f + (1 - margins.y) / 11) * height - lineWidth / 2;
        CreateAxis("Min Value Line", position, size, lineColor);

        minPriceText.transform.localPosition = position - new Vector2(0, size.y / 2);
        minPriceText.text = minValue.ToString();
        minPriceText.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, height * textSize);

        // Last
        position.x = width * lineMargins / 2;
        position.y = (margins.y / 2 + center.y - 0.5f + (1 - margins.y) * columnHeight) * height - lineWidth / 2;
        lastValueLine = CreateAxis("Last Value Line", position, size, lastPriceLineColor);

        lastPriceText.transform.localPosition = position - new Vector2(0, size.y / 2);
        lastPriceText.text = lastValue.ToString();
        lastPriceText.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, height * textSize);
    }

    private void CreateCoordinateAxes()
    {
        Vector2 position;
        Vector2 size;

        position.x = 0;
        position.y = (margins.y / 2 + center.y - 0.5f) * height - axisWidth / 2;
        size.x = width * (1 - axisMargins);
        size.y = axisWidth;
        CreateAxis("Axis X", position, size, axisColor);

        position.x = (margins.x / 2 + center.x - 0.5f - 0.01f) * width - axisWidth / 2;
        position.y = center.y * height;
        size.x = axisWidth;
        size.y = height * (1 - axisMargins);
        CreateAxis("Axis Y", position, size, axisColor);
    }

    private RectTransform CreateAxis(string name, Vector2 position, Vector2 size, Color color)
    {
        GameObject axis = new GameObject(name, typeof(Image));
        axis.transform.SetParent(transform);
        axis.transform.localScale = Vector3.one;
        axis.transform.localPosition = position;
        axis.transform.localRotation = Quaternion.identity;

        RectTransform rect = axis.GetComponent<RectTransform>();
        rect.sizeDelta = size;

        Image image = axis.GetComponent<Image>();
        image.color = color;

        return rect;
    }

    private void CreateColumns()
    {
        for (int i = 0; i < columnCount; i++)
        {
            GameObject column = new GameObject("Column [" + i + "]", typeof(Image));
            column.transform.SetParent(transform);
            column.transform.localScale = Vector3.one;
            column.transform.localPosition = Vector3.zero;
            column.transform.localRotation = Quaternion.identity;
            columns[i] = column.GetComponent<RectTransform>();
        }
    }

    private void TextOnTop()
    {
        minPriceText.transform.SetAsLastSibling();
        maxPriceText.transform.SetAsLastSibling();
        lastPriceText.transform.SetAsLastSibling();
    }

    private void FindValues()
    {
        minValue = Values[0];
        maxValue = Values[0];
        lastValue = Values[columnCount - 1];
        foreach (int value in Values)
        {
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }
        deltaValue = maxValue - minValue;
        if (deltaValue == 0) deltaValue = 1;
    }

    private void UpdateHeightColumn(int index, float value)
    {
        value = (value - minValue + deltaValue * 0.1f) / (deltaValue * 1.1f);

        GameObject column = columns[index].gameObject;

        Vector2 position = Vector3.zero;
        position.x = ((index + 0.5f) / columnCount - 0.5f) * (width * (1 - margins.x));
        position.y = (value - 1) * (height * (1 - margins.y)) / 2;
        column.transform.localPosition = position + center * new Vector2(width, height);
        column.transform.localRotation = Quaternion.identity;

        Vector2 size = Vector2.zero;
        size.x = (columnWidth / columnCount) * (width * (1 - margins.x));
        size.y = value * (height * (1 - margins.y));
        columns[index].sizeDelta = size;
    }

    private void UpdateColorColumn(int index)
    {
        Image image = columns[index].GetComponent<Image>();
        if (index == 0)
        {
            image.color = highPriceColor;
        }
        else
        {
            if (Values[index] >= Values[index - 1])
                image.color = highPriceColor;
            else
                image.color = lowPriceColor;
        }
    }

    private void UpdateTexts()
    {
        maxPriceText.text = maxValue.ToString();
        minPriceText.text = minValue.ToString();
        lastPriceText.text = lastValue.ToString();
    }

    private void UpdateLastAxes()
    {
        Vector2 position;
        float columnHeight = (lastValue - minValue + deltaValue * 0.1f) / (deltaValue * 1.1f);

        position.x = width * lineMargins / 2;
        position.y = (margins.y / 2 + center.y - 0.5f + (1 - margins.y) * columnHeight) * height - lineWidth / 2;

        lastValueLine.localPosition = position;
        lastPriceText.transform.localPosition = position - new Vector2(0, lineWidth / 2);
    }
}
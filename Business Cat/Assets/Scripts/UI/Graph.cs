using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Vector2 center = Vector2.zero;
    [SerializeField] private Vector2 margins = Vector2.zero;

    [Header("Columns")]
    [SerializeField] private int columnCount = 10;
    [SerializeField] [Range(0.1f, 1)] private float columnWidth = 1;

    [Header("Axes")]
    [SerializeField] [Range(0, 1)] private float axisMargins = 0;
    [SerializeField] [Range(0, 10)] private float axisWidth = 10;

    [Header("Colors")]
    [SerializeField] private Color lowPriceColor = Color.red;
    [SerializeField] private Color highPriceColor = Color.green;
    [SerializeField] private Color axisColor = Color.gray;
    [SerializeField] private Color lineColor = new Color(1, 0, 0.25f, 1);

    private RectTransform graphRect;
    private RectTransform[] columns;
    private RectTransform axisX, axisY;
    private RectTransform minValueLine, maxValueLine, lastValueLine;

    [HideInInspector]
    public float[] Heights;
    private float minValue;
    private float maxValue;
    private float lastValue;

    private void Awake()
    {
        graphRect = GetComponent<RectTransform>();

        Heights = new float[] { 0, 5, 3, 7, 8, 2, 0, 1 };
    }

    private void Start()
    {
        UpdateGraph();
    }

    public void UpdateGraph()
    {
        DeleteColumns();
        DeleteAxis();
        FindValues();
        CreateLines();
        CreateColumns();
        CreateAxes();
    }

    private void CreateLines()
    {

    }

    private void CreateAxes()
    {
        Vector2 position;
        Vector2 size;

        position.x = 0;
        position.y = (margins.y / 2 - 0.5f) * graphRect.rect.height - axisWidth / 2 + center.y * graphRect.rect.height;
        size.x = graphRect.rect.width * (1 - axisMargins);
        size.y = axisWidth;
        axisX = AddAxis("Axis X", position, size, axisColor);

        position.x = (margins.x / 2 - 0.5f) * graphRect.rect.width - axisWidth / 2 + center.x * graphRect.rect.width;
        position.y = center.y * graphRect.rect.height;
        size.x = axisWidth;
        size.y = graphRect.rect.height * (1 - axisMargins);
        axisY = AddAxis("Axis Y", position, size, axisColor);
    }

    private RectTransform AddAxis(string name, Vector2 position, Vector2 size, Color color)
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

    private void DeleteAxis()
    {
        if (axisX != null) Destroy(axisX.gameObject);
        if (axisY != null) Destroy(axisY.gameObject);
        if (minValueLine != null) Destroy(minValueLine.gameObject);
        if (maxValueLine != null) Destroy(maxValueLine.gameObject);
        if (lastValueLine != null) Destroy(lastValueLine.gameObject);
    }

    private void FindValues()
    {
        minValue = Heights[0];
        maxValue = Heights[0];
        lastValue = Heights[columnCount - 1];
        foreach (float value in Heights)
        {
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }
    }

    private void CreateColumns()
    {
        float deltaValue = maxValue - minValue;
        for (int i = 0; i < columnCount; i++)
        {
            float height = (Heights[i] - minValue + deltaValue * 0.1f) / (deltaValue * 1.1f);
            AddColumn(i, height);
        }
        RecolorColumns();
    }

    private void RecolorColumns()
    {
        for (int i = 0; i < columnCount; i++)
        {
            Image image = columns[i].GetComponent<Image>();
            if (i == 0)
            {
                image.color = highPriceColor;
            }
            else
            {
                if (columns[i].rect.height >= columns[i - 1].rect.height)
                    image.color = highPriceColor;
                else
                    image.color = lowPriceColor ;
            }
        }
    }

    private void DeleteColumns()
    {
        if (columns != null)
        {
            foreach (RectTransform column in columns)
            {
                Destroy(column.gameObject);
            }
        }
        columns = new RectTransform[columnCount];
    }

    private void AddColumn(int index, float height)
    {
        GameObject column = new GameObject("Column [" + index + "]", typeof(Image));
        column.transform.SetParent(transform);
        column.transform.localScale = Vector3.one;

        Vector2 position = Vector3.zero;
        position.x = ((index + 0.5f) / columnCount - 0.5f) * (graphRect.rect.width * (1 - margins.x));
        position.y = (height * (graphRect.rect.height * (1 - margins.y))) / 2 - (graphRect.rect.height * (1 - margins.y)) / 2;
        column.transform.localPosition = position + center * new Vector2(graphRect.rect.width, graphRect.rect.height);
        column.transform.localRotation = Quaternion.identity;

        Vector2 size = Vector2.zero;
        size.x = (columnWidth / columnCount) * (graphRect.rect.width * (1 - margins.x));
        size.y = height * (graphRect.rect.height * (1 - margins.y));
        columns[index] = column.GetComponent<RectTransform>();
        columns[index].sizeDelta = size;
    }
}
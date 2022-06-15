using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private int columnCount = 10;
    [SerializeField] private Vector2 margins = Vector2.zero;
    [SerializeField][Range(0.1f, 1f)] private float columnWidth  = 1;

    [Header("Colors")]
    [SerializeField] private Color lowPriceColor = Color.red;
    [SerializeField] private Color highPriceColor = Color.green;

    private RectTransform graphRect;
    private RectTransform[] columns;

    private void Awake()
    {
        graphRect = GetComponent<RectTransform>();
        columns = new RectTransform[columnCount];
    }

    private void Start()
    {
        UpdateGraph();
    }

    public void UpdateGraph()
    {
        DeleteColumns();

        for (int i = 0; i < columnCount; i++)
            AddColumn(i, 200 * Mathf.Sin((i + 1) / 3f));

        RecolorColumns();
    }

    public void RecolorColumns()
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

    public void DeleteColumns()
    {
        foreach (RectTransform column in columns)
        {

        }
        columns = new RectTransform[columnCount];
    }

    public void AddColumn(int index, float height)
    {
        GameObject column = new GameObject("Column [" + index + "]");
        column.AddComponent<Image>();
        column.transform.SetParent(transform);
        column.transform.localScale = Vector3.one;

        Vector3 position = Vector3.zero;
        position.x = ((index + 0.5f) / columnCount - 0.5f) * (graphRect.rect.width - margins.x);
        position.y = height / 2 - (graphRect.rect.height - margins.y) / 2;
        column.transform.localPosition = position;

        Vector2 size = Vector2.zero;
        size.x = (columnWidth / columnCount) * (graphRect.rect.width - margins.x);
        size.y = height;
        columns[index] = column.GetComponent<RectTransform>();
        columns[index].sizeDelta = size;
    }
}
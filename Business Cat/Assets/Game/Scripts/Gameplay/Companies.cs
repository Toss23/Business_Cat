using UnityEngine;

public class Companies : MonoBehaviour
{
    [SerializeField] private Graph graph;
    [SerializeField] private Company[] companies;
    private Company company;

    private void Awake()
    {
        foreach (Company company in companies)
            company.OnClickListener = this;
    }

    private void Start()
    {
        company = companies[0];
    }

    public void Select(Company company)
    {
        this.company.OnUpdateListener = null;
        this.company = company;
        this.company.OnUpdateListener = this;
        UpdateGraph();
    }

    public void UpdateGraph()
    {
        graph.Values = company.PriceHistory;
        graph.UpdateColumns();
    }
}

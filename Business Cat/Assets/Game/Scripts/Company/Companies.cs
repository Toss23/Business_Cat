using UnityEngine;

public class Companies : MonoBehaviour
{
    [SerializeField] private Currency currency;
    [SerializeField] private Graph graph;
    [SerializeField] private Company[] companies;
    private Company company;

    private void Awake()
    {
        foreach (Company company in companies)
            company.OnClickListener = this;
    }

    public void Select(Company company)
    {
        if (this.company != null) 
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

    public void OnClickBuy(int count)
    {
        if (company != null)
        {
            if (currency.Remove(count * company.Price))
            {
                if (company.Add(count) == false)
                    currency.Add(count * company.Price);
            }
        }
    }

    public void OnClickSell(int count)
    {
        if (company != null)
        {
            if (company.Remove(count))
            {
                if (currency.Add(count * company.Price) == false)
                    company.Add(count);
            }
        }
    }
}

using UnityEngine;

public class Companies : MonoBehaviour
{
    public static Companies Instance { get; private set; }

    [SerializeField] private Currency currency;
    [SerializeField] private Graph graph;
    [SerializeField] private Company[] companies;
    private Company company;

    private void Awake()
    {
        Instance = this;

        foreach (Company company in companies)
            company.OnClickListener = this;
    }

    public void Select(Company company)
    {
        if (this.company != null) 
            this.company.OnUpdatePrice = null;
        
        this.company = company;
        this.company.OnUpdatePrice = this;
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

    public Company Find(string identifier)
    {
        foreach (Company company in companies)
        {
            if (company.Identidier == identifier)
                return company;
        }
        return null;
    }
}

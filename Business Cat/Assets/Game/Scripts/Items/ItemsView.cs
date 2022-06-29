using System.Collections.Generic;
using UnityEngine;

public class ItemsView : MonoBehaviour
{
    [SerializeField] private Items items;
    [SerializeField] private ItemType type;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject itemPrefab;

    private List<GameObject> rows;

    private void Awake()
    {
        rows = new List<GameObject>();
        items.AddRunnableOnLoad(new Runnable(CreateView));
    }

    public void CreateView()
    {
        Item[] selectedItems = items.FindWithType(type);

        int itemsDisplayed = 0;
        int itemsCreated = 0;
        GameObject row = Row();
        GameObject item;

        foreach (Item currentItem in selectedItems)
        {
            if (items.HaveItem(currentItem))
            {
                if (itemsCreated == 2)
                {
                    row = Row();
                    itemsCreated = 0;
                }
                item = Item(currentItem, row);
                itemsCreated++;
                itemsDisplayed++;
            }
        }

        Debug.Log("[Items View] [" + name + "] Items displayed: " + itemsDisplayed + "/" + selectedItems.Length);
    }

    public void ClearView()
    {
        if (rows != null)
        {
            foreach (GameObject row in rows)
                Destroy(row);

            rows.Clear();
        }

        Debug.Log("[Items View] [" + name + "] View cleared");
    }

    private GameObject Row()
    {
        GameObject gameObject = Instantiate(rowPrefab, parent);
        gameObject.name = "Row";
        rows.Add(gameObject);
        return gameObject;
    }

    private GameObject Item(Item item, GameObject row)
    {
        GameObject gameObject = Instantiate(itemPrefab, row.transform);
        gameObject.name = "Item (" + item.Identifier + ")";
        gameObject.GetComponent<ItemView>().SetItem(item);
        return gameObject;
    }
}
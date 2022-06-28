using UnityEngine;

public class ItemsView : MonoBehaviour
{
    [SerializeField] private Items items;
    [SerializeField] private ItemType type;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        items.AddRunnableOnLoad(new Runnable(CreateView));
    }

    private void CreateView()
    {
        Item[] selectedItems = items.FindWithType(type);
        Debug.Log("[Items View] [" + name + "] Items displayed: " + selectedItems.Length);

        int itemsCreated = 0;
        GameObject row = Row();
        GameObject item;

        foreach (Item currentItem in selectedItems)
        {
            if (itemsCreated == 2)
            {
                row = Row();
                itemsCreated = 0;
            }
            item = Item(currentItem, row);
            itemsCreated++;
        }
    }

    private GameObject Row()
    {
        GameObject gameObject = Instantiate(rowPrefab, parent);
        gameObject.name = "Row";
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
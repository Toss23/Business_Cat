using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Items items;
    private Dictionary<ItemType, Item> equipment;

    public Dictionary<ItemType, Item> Equipment { get { return equipment; } }

    private void Awake()
    {
        equipment = new Dictionary<ItemType, Item>();
        items.OnLoad.Add(new Runnable(LoadEquipment));
    }

    private void LoadEquipment()
    {
        string[] itemsName = LocalData.EquipedItems();

        for (int i = 0; i < itemsName.Length; i++)
        {
            equipment.Add((ItemType)i, null);

            if (itemsName[i] != DataKey.Null)
            {
                Item item = items.Find(itemsName[i]);
                if (item != null)
                    equipment[(ItemType)i] = item;
            }
        }
    }
}
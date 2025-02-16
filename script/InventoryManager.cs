using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Transform backpackContent;  // Scroll View 的 Content（存放物品）
    public GameObject itemPrefab;  // DraggableItem 预制体
    private List<GameObject> itemsInBackpack = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemName)
    {
        GameObject newItem = Instantiate(itemPrefab, backpackContent);
        newItem.GetComponent<DraggableItem>().SetItem(itemName);
        itemsInBackpack.Add(newItem);
    }
    public void RemoveItem(GameObject item)
    {
        itemsInBackpack.Remove(item);
        Destroy(item);
    }
}
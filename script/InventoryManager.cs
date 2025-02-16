using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Transform backpackContent;  // Scroll View �� Content�������Ʒ��
    public GameObject itemPrefab;  // DraggableItem Ԥ����
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
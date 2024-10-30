using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ItemBox : NetworkBehaviour
{
    // Item types
    public enum ItemType
    {
        Banana, Shell, Mushroom, Coin
    }

    // Item list
    List<ItemType> items = new List<ItemType> { ItemType.Banana, ItemType.Shell, ItemType.Mushroom, ItemType.Coin }; /*move to director script*/

    // Set item when item box is hit
    private void OnTriggerEnter(Collider other)
    {
        ItemManager manager = other.GetComponent<ItemManager>();
        if (manager != null)
        {
            ItemType item = GetItem();
            Debug.Log(item + " acquired!");

            manager.SetItem(item);

            Destroy(gameObject);
        }
    }

    // Get item from box
    private ItemType GetItem()
    {
        int random = Random.Range(0, items.Count);
        return items[random];
    }

}

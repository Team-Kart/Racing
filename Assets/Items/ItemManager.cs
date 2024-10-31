using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : NetworkBehaviour
{
    // Item prefabs
    [SerializeField] GameObject banana;
    [SerializeField] GameObject shell;
    [SerializeField] GameObject mushroom;
    [SerializeField] GameObject coin;


    // Reference to held Item
    ItemBox.ItemType itemHeld;
    bool hasItem = false;


    // Set item acquired
    public void SetItem(ItemBox.ItemType item)
    {
        itemHeld = item;
        hasItem = true;
        Debug.Log("Item set: " + item);
    }

    // Use item currently held
    public void UseHeldItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && IsOwner && hasItem)
        {
            hasItem = false;
            //SpawnItem(itemHeld);
            ItemEffect(itemHeld);
            Debug.Log("Used item" + itemHeld);

        }
        else
        {
            Debug.Log("No item to use!");
        }

    }

    // Item effects
    void ItemEffect(ItemBox.ItemType itemHeld)
    {
        GameObject itemPrefab = null;

        switch (itemHeld)
        {
            case ItemBox.ItemType.Banana:
                itemPrefab = banana;
                break;
            case ItemBox.ItemType.Shell:
                itemPrefab = shell;
                break;
            case ItemBox.ItemType.Coin:
                itemPrefab = coin;
                break;
            case ItemBox.ItemType.Mushroom:
                itemPrefab = mushroom;
                break;
        }
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Debug.Log("Spawned Item: " + itemHeld);
        }
    }

}

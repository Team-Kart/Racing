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
    List<ItemType> items = new List<ItemType> { ItemType.Banana, ItemType.Shell, ItemType.Mushroom, ItemType.Coin };


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Award item when item box is hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kart"))
        {
            // Get random item
            ItemType randomItem = GetItem();

            Debug.Log(randomItem + " acquired!");

            Destroy(gameObject);
        }
    }

    private ItemType GetItem()
    {
        int random = Random.Range(0, items.Count);
        return items[random];
    }

}

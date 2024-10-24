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

    // Item list //i suggest putting this in the director
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
        if (other.GetComponent<ItemManager>() != null)
        {
            // Get random item

            //make a call to the Director Script to get assigned item for that player

            //other.GetComponent<ItemManager>().AddHeldItem(director given item);

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

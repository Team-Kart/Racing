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
        //if (!IsServer) return;
        KartData data = other.GetComponent<KartData>();
        if (data == null)
        {
            //Debug.Log("Data is null");
        }
        ItemManager manager = other.GetComponent<ItemManager>();
        if (manager != null)
        {
            //Debug.Log("Position: " + data.racePosition.Value);
            ItemType item = GetItem(data.racePosition.Value);


            //Debug.Log(item + " acquired!");

            manager.SetItem(item);

            Destroy(gameObject);
        }
    }

    // Get item from box
    private ItemType GetItem(int position)
    {
        //return items[random];*/
        switch (position)
        {
            case 1:
                return ItemType.Coin;
            case 2:
                return ItemType.Banana;
            case 3:
                return ItemType.Shell;
            case 4:
                return ItemType.Mushroom;
            default:
                return items[Random.Range(0, items.Count)];


        }
    }

}



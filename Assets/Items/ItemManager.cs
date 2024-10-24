using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : NetworkBehaviour
{
    //Reference to held Item

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AddHeldItem(Item item); 

    public void UseHeldItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && IsOwner)
        {
            Debug.Log("use item");
        }
        
    }
}

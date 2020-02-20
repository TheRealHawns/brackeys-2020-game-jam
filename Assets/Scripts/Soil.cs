using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{

    PlayerMovement player;
    PlayerInventory playerInventory;
    public SOPlant plant;
    bool hasCrop;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject == player.gameObject && !hasCrop)
        {
            player.HandleInteraction += HandleCrop;
          
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.HandleInteraction -= HandleCrop;
        }
    }

   

    public void HandleCrop()
    {
        
        if (!hasCrop) 
        {
            plant = playerInventory.ReturnPlantType(out bool canPlant);
            if (canPlant)
            {
                playerInventory.ManageSeeds(plant, -1);
                Debug.Log("Planting" + plant.name);
                var crop = Instantiate(plant.CropPrefab, transform);
                crop.transform.parent = transform;
                crop.GetComponent<Crop>().SetSoil(this);
                hasCrop = true;
            }
        } 
    }

    internal void Reset()
    {
        hasCrop = false;
        player.HandleInteraction += HandleCrop;
    }
}

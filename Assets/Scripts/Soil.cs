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
    bool hasHole;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject == player.gameObject && !hasCrop || collision.gameObject == player.gameObject && hasHole)
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
        else if (hasHole)
        {
            Debug.Log("Checking to see if can remove hole");
            if (playerInventory.DirtCount > 0)
            {
                removeHole();
            }
        }
    }

    private void removeHole()
    {
        playerInventory.DirtCount--;
        hasCrop = false;
        hasHole = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    internal void CreateHole()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        if (hasCrop)
        {
            Crop crop = GetComponentInChildren<Crop>();
            if (crop.IsWatered)
            {
                Timer.instance.PlantsWaterTime.Remove(crop.localTime - plant.growTime);
            }
            Destroy(transform.GetChild(0).gameObject);
        }
        hasCrop = true;
        hasHole = true;
    }

    internal void Reset()
    {
        hasCrop = false;
        player.HandleInteraction += HandleCrop;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Soil : MonoBehaviour
{

    PlayerMovement player;
    PlayerInventory playerInventory;
    public Image HolePopUp;
    public SOPlant plant;
    bool hasCrop;
    bool hasHole;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        playerInventory = player.GetComponent<PlayerInventory>();
        HolePopUp.gameObject.SetActive(false);
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
            HolePopUp.gameObject.SetActive(false);
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
                var crop = Instantiate(plant.CropPrefab, transform);
                crop.transform.parent = transform;
                crop.GetComponent<Crop>().SetSoil(this);
                hasCrop = true;
                SoundManager.PlaySound(SoundManager.Sound.Plant);
            }
        } 
        else if (hasHole)
        {
            if (playerInventory.DirtCount > 0)
            {
                removeHole();
            }
            else
            {
            HolePopUp.gameObject.SetActive(true);
                SoundManager.PlaySound(SoundManager.Sound.FailToBuy);
            }
        }
    }

    private void removeHole()
    {
        playerInventory.DirtCount--;
        var dirt = GameObject.Find("DirtCount").GetComponent<TextMeshProUGUI>().text = playerInventory.DirtCount.ToString();
        hasCrop = false;
        hasHole = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        SoundManager.PlaySound(SoundManager.Sound.FixHole);
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

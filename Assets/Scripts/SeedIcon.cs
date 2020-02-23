using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SeedIcon : MonoBehaviour
{
    Image spr;

    public Sprite[] Sprites;
    public TextMeshProUGUI text;
    PlayerInventory inv;
    PlantToPlant currentPlant = PlantToPlant.Wheat;

    private void Awake()
    {
        spr = GetComponentInChildren<Image>();
        inv = FindObjectOfType<PlayerInventory>();
        RefreshSeedCount();
    }

    public void SwitchIcon(PlantToPlant plant)
    {
        currentPlant = plant;
        switch (plant)
        {
            case PlantToPlant.Wheat:
                spr.sprite = Sprites[0];
                text.text = 99.ToString();
                break;
            case PlantToPlant.Potato:
                spr.sprite = Sprites[1];
                text.text = inv.PotatoCount.ToString();
                break;
            case PlantToPlant.Carrot:
                spr.sprite = Sprites[2];
                text.text = inv.CarrotCount.ToString();
                break;
            case PlantToPlant.Tomato:
                spr.sprite = Sprites[3];
                text.text = inv.TomatoCount.ToString();
                break;
            case PlantToPlant.Grape:
                spr.sprite = Sprites[4];
                text.text = inv.GrapeCount.ToString();
                break;
        }
    }

    internal void RefreshSeedCount()
    {
        SwitchIcon(currentPlant);
    }
}

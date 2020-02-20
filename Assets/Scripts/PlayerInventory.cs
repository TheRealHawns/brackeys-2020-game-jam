using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInventory : MonoBehaviour
{

    public int WeightLimit;
    public int Earnings;
    public int CurrentWeight;
    public int Score;

    public int WheatCount = 9999;
    public int PotatoCount = 0;
    public int CarrotCount = 0;

    public SOPlant[] plantList;
    public PlantToPlant plant = PlantToPlant.Potato;

    TextMeshProUGUI EarningsText;
    TextMeshProUGUI WeightText;

    private void Start()
    {
        EarningsText = GameObject.Find("EarningText").GetComponent<TextMeshProUGUI>();
        WeightText = GameObject.Find("WeightText").GetComponent<TextMeshProUGUI>();
    }

    public SOPlant ReturnPlantType(out bool CanPlant)
    {
        switch (plant)
        {
            default:
                CanPlant = true;
                return plantList[0];
            case PlantToPlant.Wheat:
                CanPlant = true;
                return plantList[0];
            case PlantToPlant.Potato:
                CanPlant = PotatoCount > 0;
                return plantList[1];
            case PlantToPlant.Carrot:
                CanPlant = CarrotCount > 0;
                return plantList[2];
        }
    }

    internal void SubtractSeed(SOPlant plant)
    {
        if (plant == plantList[0]) return;

        if (plant == plantList[1])
        {
            PotatoCount--;
            return;
        }

        if (plant == plantList[2])
        {
            CarrotCount--;
            return;
        }

    }

    

    internal void SetEarningText()
    {
        EarningsText.text = "Current Earnings- $" + Earnings.ToString();
        WeightText.text = "Weight Limit- " + CurrentWeight.ToString() + " / 20";
    }
}

public enum PlantToPlant
{
    Wheat,
    Potato,
    Carrot
}
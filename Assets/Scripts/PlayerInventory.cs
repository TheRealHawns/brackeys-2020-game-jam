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

    public SOPlant returnPlantType()
    {
        switch (plant)
        {
            default: 
                return plantList[0];
            case PlantToPlant.Wheat:
                return plantList[0];
            case PlantToPlant.Potato:
                return plantList[1];
            case PlantToPlant.Carrot:
                return plantList[2];
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
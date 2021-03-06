﻿using System;
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
    public int Money;
    public int Score;

    public int WheatCount = 9999;
    public int PotatoCount = 0;
    public int CarrotCount = 0;
    public int TomatoCount = 0;
    public int GrapeCount = 0;
    public int DirtCount = 0;

    public SOPlant[] plantList;
    public PlantToPlant plant = PlantToPlant.Potato;

    TextMeshProUGUI EarningsText;
    TextMeshProUGUI WeightText;
    TextMeshProUGUI MoneyText;
    TextMeshProUGUI ScoreText;


    private void Start()
    {
        EarningsText = GameObject.Find("EarningText").GetComponent<TextMeshProUGUI>();
        WeightText = GameObject.Find("WeightText").GetComponent<TextMeshProUGUI>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
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
            case PlantToPlant.Tomato:
                CanPlant = TomatoCount > 0;
                return plantList[3];
            case PlantToPlant.Grape:
                CanPlant = GrapeCount > 0;
                return plantList[4];
        }
    }

    internal void ManageSeeds(SOPlant plant, int addOrSubtract)
    {
        var seed = GameObject.Find("SeedToPlantBG").GetComponent<SeedIcon>();
        seed.RefreshSeedCount();
        if (plant == plantList[0]) return;

        if (plant == plantList[1])
        {
            PotatoCount += addOrSubtract;
            seed.RefreshSeedCount();
            return;
        }

        if (plant == plantList[2])
        {
            CarrotCount += addOrSubtract ;
            seed.RefreshSeedCount();
            return;
        }
        if (plant == plantList[3])
        {
            TomatoCount += addOrSubtract;
            seed.RefreshSeedCount();
            return;
        }
        if (plant == plantList[4])
        {
            GrapeCount += addOrSubtract;
            seed.RefreshSeedCount();
            return;
        }

        DirtCount++;
        var dirt = GameObject.Find("DirtCount").GetComponent<TextMeshProUGUI>().text = DirtCount.ToString();

        
    }

    

    internal void SetUIText()
    {
        EarningsText.text = "Current Earnings- $" + Earnings.ToString();
        WeightText.text = "Weight Limit- " + CurrentWeight.ToString() + " / 20";
        MoneyText.text = "Money- $" + Money.ToString();
        ScoreText.text = "Score- " + Score.ToString();
    }
}

public enum PlantToPlant
{
    Wheat,
    Potato,
    Carrot,
    Tomato,
    Grape,
    MAX_LENGTH
}
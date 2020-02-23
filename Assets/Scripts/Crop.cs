using TMPro;
using UnityEngine;

public class Crop : MonoBehaviour
{


    [SerializeField] PlayerMovement player;
    PlayerInventory inv;
    int waterRespawntime;
    SpriteRenderer spr;

    public bool IsWatered { get; private set; }

    private int waterLevel;
    private bool harvestable;
    private Soil soil;
    private Color soilColor;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }


    #region Event Stuff
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (player == null)
            {
                player = collision.transform.GetComponent<PlayerMovement>();
                inv = player.GetComponent<PlayerInventory>();
                player.HandleInteraction += InteractWithPlayer;
            }
            else
                player.HandleInteraction += InteractWithPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            player.HandleInteraction -= InteractWithPlayer;
        }

    }

    internal void SetSoil(Soil soil)
    {
        this.soil = soil;
    }

    private void OnDestroy()
    {
        if (player != null)
        player.HandleInteraction -= InteractWithPlayer;
    }
    #endregion

    void InteractWithPlayer()
    {
        if (!IsWatered)
        {
            Water();
            IsWatered = true;
        }
    }

    public void ResetWaterTimer()
    {
        IsWatered = false;
        var soilspr = soil.GetComponent<SpriteRenderer>();
        soilspr.color = soilColor;
        if (waterLevel >= soil.plant.overworldSprites.Length)
        {
            waterLevel = soil.plant.overworldSprites.Length -1;
        }
        if (spr != null) spr.sprite = soil.plant.overworldSprites[waterLevel];
        SoundManager.PlaySound(SoundManager.Sound.ReWater);
    }
    public int localTime;

    private void WaterRespawnAlarm()
    {
        localTime = Timer.instance.RequestTime();
        Timer.instance.Alarm(localTime - soil.plant.growTime, this);
    }

    private void Water()
    {
        player.Water();
        if (harvestable && inv.CurrentWeight + soil.plant.weight <= inv.WeightLimit)
        {
            Harvest();
            return;
        }
        waterLevel++;
        if (soil != null)
        {
            WaterRespawnAlarm();
            SoundManager.PlaySound(SoundManager.Sound.Water);
            var soilspr = soil.GetComponent<SpriteRenderer>();
            soilColor = soilspr.color;
            soilspr.color = Color.gray;
        }

        if (waterLevel == soil.plant.wateringCount) harvestable = true;

    }

    private void Harvest() //WIP
    {
        SoundManager.PlaySound(SoundManager.Sound.Harvest);
        inv.CurrentWeight += soil.plant.weight;
        inv.Earnings += soil.plant.value;
        inv.SetUIText();
        Destroy(gameObject);
        if (soil!=null) soil.Reset();
    }
}

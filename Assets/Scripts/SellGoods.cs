using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SellGoods : MonoBehaviour
{
    public PlayerMovement Player;
    public PlayerInventory PlayerInv;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>();
        PlayerInv = Player.GetComponent<PlayerInventory>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.gameObject)
        {
            Player.HandleInteraction += SellMerchandise;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.gameObject)
        {
            Player.HandleInteraction -= SellMerchandise;
        }
    }
    private void OnDestroy()
    {
        Player.HandleInteraction -= SellMerchandise;
    }

    void SellMerchandise()
    {
        if (PlayerInv.Earnings == 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.FailToBuy);
        } else
        {
            SoundManager.PlaySound(SoundManager.Sound.Sell);
        }
        PlayerInv.CurrentWeight = 0;
        PlayerInv.Money += PlayerInv.Earnings;
        PlayerInv.Score += PlayerInv.Earnings;
        PlayerInv.Earnings = 0;
        PlayerInv.SetUIText();
    }
}

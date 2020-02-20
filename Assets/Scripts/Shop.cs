using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Shop : MonoBehaviour
{

    public SOPlant PlantToSell;
    public int Price;

    public PlayerMovement Player;
    public PlayerInventory PlayerInv;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>();
        PlayerInv = Player.GetComponent<PlayerInventory>();
    }

    public abstract void GiveMerchandise();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.gameObject)
        {
            Player.HandleInteraction += GiveMerchandise;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject == Player.gameObject)
        {
            Player.HandleInteraction -= GiveMerchandise;
        }
    }

    private void OnDestroy()
    {
        Player.HandleInteraction -= GiveMerchandise;
    }


}

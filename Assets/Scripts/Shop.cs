using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shop : MonoBehaviour
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

    public void GiveMerchandise()
    {
        if (PlayerInv.Score >= Price)
        {
            PlayerInv.Score -= Price;
            PlayerInv.ManageSeeds(PlantToSell, 1);

        }
        PlayerInv.SetUIText();
    }

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

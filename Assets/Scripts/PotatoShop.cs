using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoShop : Shop
{
    
    public override void GiveMerchandise()
    {
        if (PlayerInv.Score >= Price)
        {
            PlayerInv.Score -= Price;
            PlayerInv.PotatoCount += 1;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/Plants", order = 1)]
public class SOPlant : ScriptableObject
{
    public Sprite[] overworldSprites;
    public int weight;
    public int value;
    public int growTime;
    public int wateringCount;
    public GameObject CropPrefab;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Kajia Setting",menuName = "Back/KajiaSet")]
public class K_Scr : ScriptableObject
{
    public float minSpawnTime, distance;
    public int activateAtScore,eventMax;
}

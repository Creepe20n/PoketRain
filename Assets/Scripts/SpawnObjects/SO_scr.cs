using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnObject",menuName = "Game/SpawnObject")]
public class SO_scr : ScriptableObject
{
    public ParticleSystem preParticle;
    public int spawnChance, changeLife;
    public float changePlayerSpeed,moveSpeed=4;
    public int changeScorePointsPlayer,changeScorePointsGround;
    public GameObject spawnObject;
    [TextAreaAttribute]
    public string description;
    public Sprite cardSprite;
}

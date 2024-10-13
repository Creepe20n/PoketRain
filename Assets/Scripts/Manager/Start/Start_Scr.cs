using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Start Scr",menuName = "Back/StartUp")]
public class Start_Scr : ScriptableObject
{
    public List<Vector2> spawnPoints = new();
    public List<Vector2> eventSpawnPoints = new();
    public float addXToSP = 0.2f,addXToESP = 0.4f;
    public long highScore, score;
    public int level;
}

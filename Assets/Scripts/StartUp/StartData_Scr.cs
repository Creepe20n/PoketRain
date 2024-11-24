using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new StartUpData",menuName = "Back/StartUpData")]
public class StartData_Scr : ScriptableObject
{
    public List<Transform> SpawnPoints = new();
}

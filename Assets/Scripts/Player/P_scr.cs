using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player", menuName = "Back/Player/Player")]
public class P_scr : ScriptableObject
{
    public float speed;
    public LayerMask mask;
    public int life;
    public GameObject graveStone;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "P_Modifiy",menuName = "Back/Player/Modifiy")]
public class P_Modifiy_scr : ScriptableObject
{
    public float speed;
    public int K_startItemChance,K_startPlusItemChance,
        life,maxHeart = 5;
    public GameObject graveStone;

    public void TReset() {
        speed = 0;
        K_startItemChance = 0;
        K_startPlusItemChance = 0;
        life = 0;
        maxHeart = 5;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface P_Hit {
    public void hitPlayer(int addScorePoints = 0,float changeSpeed = 0,int changeLife = 0,GameManager gM = null);
}

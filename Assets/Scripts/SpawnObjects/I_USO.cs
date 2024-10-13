using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_USO
{
    public void SetPlayer(GameObject plyr);
    //collPos = CollisionPositon
    public void ResetObj(Vector2 collPos);
    public void AddScore(int points);
}

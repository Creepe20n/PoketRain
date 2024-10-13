using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastBase;
using FastThings.FastCamera;
public class StartUpScript : MonoBehaviour
{
    [SerializeField] Start_Scr settings;
    [SerializeField] Transform wallLeft, wallRight;
    private void Awake() {
        settings.spawnPoints.Clear();
        settings.eventSpawnPoints.Clear();

        float xDist = Fast.X_Distance(new Vector2(wallLeft.position.x+0.5f,0),new Vector2(wallRight.position.x-0.5f,0))-0.2f;
        print(xDist);

        float x = wallLeft.position.x + 0.7f;
        float y = Camera.main.transform.position.y + F_Camera.GetCameraHight(Camera.main) / 2 ;

        SetSpawnPointPos(settings.addXToSP,settings.spawnPoints,x,y);
        SetSpawnPointPos(settings.addXToESP,settings.eventSpawnPoints,x,y);
    }

    void SetSpawnPointPos(float plusX,List<Vector2> addToList,float xStart,float y) {
        while(xStart <= wallRight.position.x - 0.5f) {
            addToList.Add(new Vector2(xStart,y));
            xStart += plusX;
        }
    }
    private void OnDrawGizmos() {
        DrawSphere(settings.spawnPoints,Color.red);
        DrawSphere(settings.eventSpawnPoints,Color.green);
    }

    void DrawSphere(List<Vector2> list,Color color) {
        Gizmos.color = color;
        foreach(Vector2 v in list) {
            Gizmos.DrawSphere(v,0.1f);
        }
    }
}
 
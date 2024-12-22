using UnityEngine;
using PoketAPI.Camera;
using PoketAPI.Convert;
using System.Linq;
public class StartUpScript : MonoBehaviour
{
    [SerializeField] private StartData_Scr startData_Scr;
    [SerializeField] private int spawnPoints = 10;

    private void Awake() {
        startData_Scr.SpawnPoints.Clear();
        startData_Scr.SpawnPoints = CalcPoints(spawnPoints).ToList();
    }

    Vector2[] CalcPoints(int points) {
        Vector2[] vecPoints = new Vector2[points];
        float startX = Camera.main.transform.position.x - (CameraData.GetWidth() /2);
        float endX = Camera.main.transform.position.x + (CameraData.GetWidth() /2);

        float hight = Camera.main.transform.position.y + (CameraData.GetHight()/2);

        float plusX = ConvertPosition.X_Distance(new Vector2(startX,0),new Vector2(endX-0.2f,0)) / points;

        float activeX = startX+0.2f;

        for(int i = 0;i < vecPoints.Length;i++) {
            vecPoints[i] = new Vector2(activeX,hight);
            activeX += plusX;

        }

        return vecPoints;
    }

    private void OnDrawGizmos() {
        for(int i = 0; i < startData_Scr.SpawnPoints.Count;i++) {
            Gizmos.DrawSphere(startData_Scr.SpawnPoints[i],0.1f);
        }
    }
}

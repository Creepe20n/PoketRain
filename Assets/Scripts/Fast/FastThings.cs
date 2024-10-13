using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FastThings
{
    namespace FastBase {
        public class Fast{
            public static bool LookInDir(Vector2 Origin,Vector2 Dir,float RayLength,LayerMask Mask)
            {
                RaycastHit2D hit = Physics2D.Raycast(Origin, Dir, RayLength,Mask);
                return hit;
            }
            public static float X_Distance(Vector2 Dist1,Vector2 Dist2) 
            { 
                Dist1 = new Vector2(Dist1.x,0);Dist2 = new Vector2(Dist2.x,0);
                return Vector2.Distance(Dist1,Dist2);
            }
            public static float Y_Distance(Vector2 Dist1,Vector2 Dist2) 
            { 
                Dist1 = new Vector2(0,Dist1.y);Dist2 = new Vector2(0,Dist2.y);
                return Vector2.Distance(Dist1,Dist2);
            }
            //"Gives you a Percent number. For example 30 => 70"
            public static float GetPercents( float PercentNum)
            {
                return PercentNum = 100-PercentNum;
            }
            public static GameObject[] RemoveElementFromArray(GameObject[] Array,GameObject ElementToRemove)
            {
                List<GameObject> ArrayList = new List<GameObject>();
                for(int i = 0; i < Array.Length; i++)
                {
                    if(Array[i] == ElementToRemove)
                        continue;
                
                    ArrayList.Add(Array[i]);
                }
                GameObject[] TempArray = ArrayList.ToArray();
                return TempArray;
            }
            public static bool stopTime(float time) {
                float countTime = 0;
                while(true) {
                    countTime += Time.deltaTime;
                    if(countTime >= time)
                        break;
                }
                return true;
            }
        }
    }
    namespace FastCamera {
        public class F_Camera { 
            public static float GetCameraWidht(Camera cam)
            {
                return cam.aspect * (cam.orthographicSize * 2);
            }
            public static Vector2 ToWorldPoint(Vector2 ConvertPos)
            {
                return Camera.main.ScreenToWorldPoint(ConvertPos);
            }
            public static float GetCameraHight(Camera cam)
            {
                return cam.orthographicSize * 2;
            }
        }
    }
    namespace FastGUI {
        public class F_UI {
            public static float GetCanvasHight(Canvas canvas) {
                return canvas.GetComponent<RectTransform>().rect.height;
            }
            public static float GetCanvasWidth(Canvas canvas) {
                return canvas.GetComponent<RectTransform>().rect.width;
            }
            public static Vector2 ConvertCanvasPosToPercent(Canvas canvas,RectTransform rect) {
                float C_oneHightPercent = F_UI.GetCanvasHight(canvas)/1000;
                float C_oneWidthPercent = F_UI.GetCanvasWidth(canvas)/1000;

                float Y_percent = rect.rect.y / C_oneHightPercent;
                float X_percent = rect.rect.x / C_oneWidthPercent;
                return new Vector2(X_percent,Y_percent);
            }
            public static Vector2 ConvertPercentToCanvasPos(Canvas canvas,Vector2 percent) {
                float C_oneHightPercent = F_UI.GetCanvasHight(canvas) / 1000;
                float C_oneWidthPercent = F_UI.GetCanvasWidth(canvas) / 1000;

                float Y_percent = percent.y * C_oneHightPercent;
                float X_percent = percent.x * C_oneWidthPercent;
                return new Vector2(X_percent,Y_percent);
            }
        }
    }
    namespace FastConverter {
        public class F_Converter
        {
            public static Vector2 PosToCamPercent(Camera cam,Vector2 ObjPos)
            {
                Vector2 vec1, vec2;
                float DividedPos=0,Distance;

                //Y Berechnen
                vec1 = new Vector2(0, ObjPos.y);vec2 = new Vector2(0, cam.transform.position.y - cam.orthographicSize);
                DividedPos = FastCamera.F_Camera.GetCameraHight(cam)/100;
                Distance = Vector2.Distance(vec1, vec2);
                if (ObjPos.y < vec2.y)
                    DividedPos = -DividedPos;
                ObjPos.y = Distance / DividedPos;

                // .X Berechnen 
                vec1 = new Vector2(ObjPos.x, 0);vec2 = new Vector2(cam.transform.position.x,0);
                DividedPos = FastCamera.F_Camera.GetCameraWidht(cam) / 100;
                Distance = Vector2.Distance(vec1, vec2);
                if(ObjPos.x < vec2.x)
                    DividedPos = -DividedPos;
                ObjPos.x = Distance / DividedPos;

                return ObjPos;
            }
            public static Vector2 PercentToWorldPos(Camera cam,Vector2 Percent)
            {
                float DividedPos = FastCamera.F_Camera.GetCameraHight(cam) / 100;
                Percent.y = (cam.transform.position.y - cam.orthographicSize) + (Percent.y * DividedPos);

                DividedPos = FastCamera.F_Camera.GetCameraWidht(cam)/100;
                Percent.x = cam.transform.position.x + (Percent.x * DividedPos);

                return Percent;
            }
        }
    }
    namespace FastGameObject {
        public class F_GameObject{
            public static GameObject GetObjectFromPoolTag(GameObject[] Array,GameObject NeededObject)
            {
                for(int i = 0;i< Array.Length;i++)
                {
                    if(Array[i].tag == NeededObject.tag&&!Array[i].activeInHierarchy)
                        return Array[i];
                }
                return null;
            }
            public static bool CheckArrayForObj(GameObject[] Array,GameObject NeededObject) {
                for(int i = 0;i < Array.Length;i++) {
                    if(Array[i] == NeededObject)
                        return true;
                }
                return false;
            }
        }
    }
    namespace FastTouch {
        public class F_Touch{
            public static bool IsTouching() {
                if(Input.touchCount <= 0)
                    return false;
                else
                    return true;
            }
            public static bool DoubleTab()
            {
                if(Input.touchCount <= 0)
                    return false;

                Touch t =Input.GetTouch(0);

                if(t.tapCount <= 1)
                    return false;

                return true;
            }
            public static Vector2 TouchPos(bool RawTouchPos = false)
            {
                if(Input.touchCount <= 0)
                    return Vector2.zero;
            
                Touch t = Input.GetTouch(0);

                if(RawTouchPos)
                    return t.position;
                return FastCamera.F_Camera.ToWorldPoint(t.position);
            }
        }
    }
}

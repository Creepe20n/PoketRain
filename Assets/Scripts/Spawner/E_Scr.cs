using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Event",menuName = "Game/Event")]
public class E_Scr : ScriptableObject
{
    [TextArea]
    public string eventFrase;
    public GameObject eventObject;
}

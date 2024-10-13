using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Perk",menuName = "")]
public class Pk_Scr : ScriptableObject
{
    public string prkName;
    public Sprite icon;
    public float changePlyrSpeed;
    public int changeActiveHeart,changeMaxHeart;
    [TextAreaAttribute]
    public string description;
}

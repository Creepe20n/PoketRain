using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemy : MonoBehaviour
{
    [SerializeField] int life = 3;
    [SerializeField] float movmentSpeed;
    [HideInInspector] public float goToY;
    [HideInInspector] public SpaceShip_event spaceShip_Event;
}

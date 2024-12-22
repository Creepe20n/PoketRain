using UnityEngine;

public class Event_ChangePlayerData : EventBase
{
    [Header("When object hits Ground")]
    [SerializeField] private int g_changePlayerLife = 0;
    [SerializeField] private float g_changePlayerSpeed = 0;
    [Header("When object hits Entity")]
    [SerializeField] private int e_changeEntityLife = 0;
    [SerializeField] private float e_changeEntitySpeed = 0;

    public override void OnEventStart(GameObject hitObject, GameManager gameManager) {

        switch(hitObject.tag) {
            case "Entity":
                EntityBase entityBase = hitObject.GetComponent<EntityBase>();

                entityBase.health += e_changeEntityLife;
                entityBase.moveSpeed += e_changeEntitySpeed;

                break;
            case "Ground":
                EntityBase entityBase1 = gameManager.player.GetComponent<EntityBase>();

                entityBase1.health += g_changePlayerLife;
                entityBase1.moveSpeed += g_changePlayerSpeed;

                break;

        }

        OnEventEnd();
    }
}

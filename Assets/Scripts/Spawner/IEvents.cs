using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvents
{
    public void StartEvent(Start_Scr _startSettings,List<GameObject> _activeObjectPool ,KajiaSys _kajiaSys,float _activeSpawnTime = 0,Spawner _spawner = null,
        P_Base _playerBase = null);
    public void EndEvent();
}

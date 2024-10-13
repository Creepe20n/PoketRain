using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Be_SlowTime:MonoBehaviour, I_USO {

    [SerializeField] float slowLeangth,newSoTime;


    private void OnEnable() {
        StartCoroutine(Timer());
    }

    public IEnumerator Timer() {
        SO_Base.SoTime = newSoTime;
        yield return new WaitForSeconds(slowLeangth);
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        SO_Base.SoTime = 1;
    }



    #region unused
    public void AddScore(int points) {

    }

    public void ResetObj(Vector2 collPos) {

    }

    public void SetPlayer(GameObject plyr) {

    }
    #endregion

}

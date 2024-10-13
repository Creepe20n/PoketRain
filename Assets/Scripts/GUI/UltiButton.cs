using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UltiButton : MonoBehaviour
{
    public string LoadSceneName;
    public List<GameObject> setActiveObj = new(),setDisableObj = new();
    public void OnPress() {
        if(LoadSceneName != "")
            SceneManager.LoadScene(LoadSceneName);

        if(setActiveObj.Count > 0 )
            for(int i = 0; i < setActiveObj.Count; i++)
                setActiveObj[i].SetActive(true);

        if(setDisableObj.Count > 0 )
            for (int i = 0;i < setDisableObj.Count;i++)
                setDisableObj[i].SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Animator aniTopSpike,aniDownSpike;
    [SerializeField] float timeToLoad = 1;

    private void Start() {
        aniTopSpike.Play("ReSpikeDown");
        aniDownSpike.Play("ReSpikeUp");
    }
    public void LoadScene(string _sceneName) {
        if(_sceneName != "")
            sceneName = _sceneName;

        aniTopSpike.Play("BlackSpikeDown");
        aniDownSpike.Play("BlackSpkieUp");
        Invoke(nameof(goToScene),timeToLoad);
    }
    void goToScene() {
        SceneManager.LoadScene(sceneName);
    }


}

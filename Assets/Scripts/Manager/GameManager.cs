using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FastThings.FastTouch;
public class GameManager : MonoBehaviour
{
    public P_Base playerBase;
    public Vector2 taskbarUpperEnd;
    [SerializeField] KajiaSys kajia;
    [SerializeField] UnityEvent startEvent,endEvent;
    [SerializeField] P_Modifiy_scr P_modifiy;
    public Pk_Scr[] allPerks,selectedPerks,selecteblePrks;
    public GameObject[] allItems,selectedItens;
    [SerializeField] UIManager uiManager;
    public bool blockStart = false;
    public int scoreMultiply, score;
    [SerializeField]Animator[] perkFlyin;
    [SerializeField] GameObject perkTitle;
    [SerializeField] Animator perkWindow;

    private void Start() {
        Application.targetFrameRate = 60;
        P_modifiy.TReset();
    }
    public void AddScorePoints(int points) {
        if(playerBase.life > 0)
            score += (scoreMultiply+1) * points;
    }
    private void Update() {
        float y = F_Touch.TouchPos().y;

        if(y == 0)
            return;

        if(y > taskbarUpperEnd.y && !blockStart && F_Touch.DoubleTab()) {
            blockStart = true;
            StartCoroutine(GameStart());
        }

        if(!blockStart)
            return;
        //Game Ends
        if(playerBase.life <= 0 || !playerBase.isActiveAndEnabled)
            EndGame();
    }
    public IEnumerator GameStart() {
        //selectet Cards
        //prk
        uiManager.HideSideBar();
        for(int j = 0;j < selectedPerks.Length;j++) {
            if(selectedPerks[j] == null)
                continue;

            P_modifiy.maxHeart += selectedPerks[j].changeMaxHeart;
            P_modifiy.life += selectedPerks[j].changeActiveHeart;
            P_modifiy.speed += selectedPerks[j].changePlyrSpeed;
        }

        perkTitle.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        foreach(Animator x in perkFlyin) {
            x.Play("FlyIn");
            yield return new WaitForSeconds(0.3f);
        }
        
        //Start PErks
        for(int i = 0;i < 3;i++) {
            Pk_Scr autoPerk = allPerks[Random.Range(0,allPerks.Length)];

            P_modifiy.maxHeart += autoPerk.changeMaxHeart;
            P_modifiy.life += autoPerk.changeActiveHeart;
            P_modifiy.speed += autoPerk.changePlyrSpeed;

            perkFlyin[i].Play("PopUp");
            uiManager.ShowAutoPerks(autoPerk,i);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(3);
        perkWindow.Play("FadeOut");
        yield return new WaitForSeconds(0.4f);

        foreach(Animator x in perkFlyin) {
            x.Play("Base");
        }
        perkTitle.SetActive(false);
        uiManager.ResetPerkView();

        startEvent.Invoke();
        playerBase.GameStart(P_modifiy);
    }
    void EndGame() {

        if(PlayerPrefs.GetInt("highScore",0) < score)
            PlayerPrefs.SetInt("highScore",score);

        score = 0;

        endEvent.Invoke();
        P_modifiy.TReset();
        playerBase.gameObject.SetActive(true);
        playerBase.EndGame();
        Invoke(nameof(EnableRestart),0.1f);
    }
    void EnableRestart() {
        blockStart = false;
    }

}

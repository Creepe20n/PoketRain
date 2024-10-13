using FastThings.FastCamera;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FastDebug;
public class UIManager : MonoBehaviour
{
    [SerializeField] Sprite heartFull,heartEmpty;
    [SerializeField] RectTransform taskBarReact;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject doubleTab;
    public RectTransform gameCanvasRect;
    public GameManager gameManager;
    [SerializeField] GameObject preImage;
    P_Base playerBase;
    List<GameObject> heartArray = new();
    [SerializeField]Image[] showPerkArray;
    [SerializeField] TextMeshProUGUI score_txt, highScore_txt;
    [SerializeField] Canvas devViewCanvas;
    [SerializeField] Image devViewButtonIm;
    [SerializeField] Sprite devViewActive, devViewInActive, basePerkSpr;
    [SerializeField] Animator leftSideBar;
    [SerializeField] TextMeshProUGUI[] perkTexts;

    bool gameStarted,activeDevView=false;
    private void Start() {
        gameManager.taskbarUpperEnd = F_Camera.ToWorldPoint(new Vector2(0,taskBarReact.transform.position.y * 2));
        highScore_txt.text = PlayerPrefs.GetInt("highScore",0).ToString();
        leftSideBar.Play("FlyIn");
    }
    public void BlockGameStart() {
        gameManager.blockStart = true;
    }
    public void AllowGameStart() {
        gameManager.blockStart = false;
    }
    public void GameStart(P_Modifiy_scr modifiy) {
        print("UI Start");
        BuildHeartUI(modifiy);

        if(playerBase == null)
            playerBase = gameManager.playerBase;

        gameStarted = true;
    }
    public void HideSideBar() {
        leftSideBar.Play("FlyOut");
        doubleTab.SetActive(false);
    }
    public void ShowAutoPerks(Pk_Scr autoPerk,int num) {
        taskBarReact.gameObject.SetActive(false);//

        showPerkArray[num].sprite = autoPerk.icon;
        perkTexts[num].text = autoPerk.prkName;
    }

    public void ResetPerkView() {
        for(int i =0;i < 3;i++) {
            showPerkArray[i].sprite = basePerkSpr;
            perkTexts[i].text = "";
        }
    }

    public void EndGame() {
        doubleTab.SetActive(true);
        StopAllCoroutines();

        highScore_txt.text = PlayerPrefs.GetInt("highScore",0).ToString();
        score_txt.text = "0";

        gameStarted = false;
        foreach(GameObject x in heartArray) {
            x.SetActive(false);
        }
        taskBarReact.gameObject.SetActive(true);
        leftSideBar.Play("FlyIn");
        print("UI End");
    }

    private void Update() {

        if(activeDevView)
            DevView();

        if(!gameStarted)
            return;

        score_txt.text = gameManager.score.ToString();
        UpdateHeartUI();
    }
    #region DevView
    D_text versionNumber;
    D_text fps_txt;
    bool buildDevView = false;


    public void ActivateDevView() {
        if(activeDevView) {
            devViewButtonIm.sprite = devViewInActive;
            activeDevView = false;

            versionNumber.SetActive(false);
            fps_txt.SetActive(false);
            CancelInvoke(nameof(UpdateFPS));
            return;
        }

        devViewButtonIm.sprite = devViewActive;

        if(!buildDevView) {
            buildDevView = true;
            BuildDevView();
        }

        activeDevView = true;

        versionNumber.SetActive(true);
        fps_txt.SetActive(true);

        UpdateFPS();
    }

    void BuildDevView() {
        versionNumber = new(1,1,devViewCanvas,fontSize:40,allowedRows: 20);
        fps_txt = new(2,1,devViewCanvas,fontSize:70,allowedRows:20);
    }

    private void DevView() {
        versionNumber.SetText(Application.version,Color.green);
    }
    private void UpdateFPS() {
        fps_txt.SetText("fps: "+ ((int)(1f / Time.unscaledDeltaTime)).ToString(),Color.green);
        Invoke(nameof(UpdateFPS),1);
    }

    #endregion
    void UpdateHeartUI() {
        int lifes = gameManager.playerBase.life;

        for(int i = 0; i < heartArray.Count; i++) {
            if(lifes >= i + 1)
                heartArray[i].GetComponent<Image>().sprite = heartFull;
            else
                heartArray[i].GetComponent<Image>().sprite = heartEmpty;
        }
    }
    void BuildHeartUI(P_Modifiy_scr modifiy) {
        int maxHearts = modifiy.maxHeart;
        int lifes = 5 + modifiy.life;

        float usebleCanvasLeangth = gameCanvasRect.rect.width/2;
        float leangthBetweanHeart = usebleCanvasLeangth / 3;
        float startCanvasY = (gameCanvasRect.rect.height/2)-100;
        float startCanvasX = leangthBetweanHeart - usebleCanvasLeangth;
        int rest = maxHearts % 3;

        for(int i =0; i < maxHearts;i++) {
            if(startCanvasX > 0) {
                startCanvasX = leangthBetweanHeart - usebleCanvasLeangth;
                startCanvasY -= 100;
            }

            Vector2 spawnHeartAt = new(startCanvasX,startCanvasY);

            if(heartArray.Count-1 < i)
                heartArray.Add(Instantiate(preImage));
            else { heartArray[i].SetActive(true); }

            heartArray[i].transform.SetParent(gameCanvas.transform);
            heartArray[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(spawnHeartAt.x,spawnHeartAt.y,0);

            if(rest > 1 && maxHearts - i == rest) {
                startCanvasX += leangthBetweanHeart / 2;
                heartArray[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(startCanvasX,spawnHeartAt.y,0);
            }

            if(rest > 1 && maxHearts - i == 1) {
                heartArray[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(spawnHeartAt.x,spawnHeartAt.y,0);
            }

            if(rest == 1 && maxHearts - i == 1)
                heartArray[i].GetComponent<RectTransform>().transform.localPosition = new Vector3(spawnHeartAt.x+leangthBetweanHeart,spawnHeartAt.y,0);

            if(lifes >= i+1)
                heartArray[i].GetComponent<Image>().sprite = heartFull;
            else
                heartArray[i].GetComponent<Image>().sprite = heartEmpty;

            startCanvasX += leangthBetweanHeart;
        }

    }
}

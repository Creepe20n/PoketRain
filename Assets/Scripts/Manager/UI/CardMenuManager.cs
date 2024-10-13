using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FastThings.FastGameObject;
public class CardMenuManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] KajiaSys kajiaSys;
    [SerializeField] RectTransform cardsBackground;
    [SerializeField] GameObject preCardButton,perkParent,itemParent,cardInfoPanel,selectionMenu;
    GameObject oldActiveCard;
    [SerializeField] Sprite lookedStateSp;
    List<GameObject> perkCards = new(), itemCards = new();
    [SerializeField] TextMeshProUGUI titleInfoCard_txt,descriptionInfoCard_txt;
    [SerializeField] Image infoCardPFP,selectionFakeCard;

    [SerializeField] CardButton[] mainCards;

    Pk_Scr[] selectetPerks = new Pk_Scr[3];
    GameObject[] selectetItems = new GameObject[3];

    bool perkViewActive = true, waitForSelection = false;
    int lastCardCall = -1,activeWaitForSelectionCard = 0;

    private void Update() {
        if(!waitForSelection)
            return;
        if(lastCardCall < 0)
            return;

        if(perkViewActive) {
            CardButton tempCard = perkCards[activeWaitForSelectionCard].GetComponent<CardButton>();
            selectetPerks[lastCardCall] = tempCard.activePerk;
        }
        else {
            CardButton temp = itemCards[activeWaitForSelectionCard].GetComponent< CardButton>();
            selectetItems[lastCardCall] = temp.activeItem;
        }
        UpdateSelectedCards();
        ResetCards();
    }

    public void SwitchToItemView() {
        if(itemCards.Count == 0)
            BuildBaseCards(itemParent,itemList: gameManager.allItems.ToList());
        perkViewActive = false;
        perkParent.SetActive(false);
        itemParent.SetActive(true);
        ManageCardsDownMenu(null);
        ResetCards();
        UpdateSelectedCards();
    }
    public void SwitchToPerkView() {
        if(perkCards.Count == 0)
            BuildBaseCards(perkParent,perkList: gameManager.selecteblePrks.ToList());
        itemParent.SetActive(false);
        perkParent.SetActive(true);
        ManageCardsDownMenu(null);
        perkViewActive = true;
        ResetCards();
        UpdateSelectedCards();
    }
    void UpdateSelectedCards() {
        for(int i = 0; i < mainCards.Length;i++) { 
            if(perkViewActive) { 
                mainCards[i].UpdateCard(selectetPerks[i]);
                continue;
            }
            mainCards[i].UpdateCard(newActiveItem:selectetItems[i]);
        }
    }
    //Sets open window to deffult
    public void ResetCards(){
        lastCardCall = -1;
        waitForSelection = false;
        selectionMenu.SetActive(false);
        ManageCardsDownMenu(null);
        SelectCard(true);
    }
    public void CardFinalSelect(int cardNum) {
        for(int i = 0;i < 2;i++) {
            if(perkViewActive) {
                if(selectetPerks[i] == perkCards[cardNum].GetComponent<CardButton>().activePerk) {
                    ResetCards();
                    return;
                }
                    
                continue;
            }
            if(selectetItems[i] == itemCards[cardNum].GetComponent<CardButton>().activeItem) {
                ResetCards();
                return;
            }
        }
        activeWaitForSelectionCard = cardNum;
        waitForSelection = true;

        selectionFakeCard.sprite = perkViewActive ? 
            perkCards[cardNum].GetComponent<Image>().sprite 
            : itemCards[cardNum].GetComponent<Image>().sprite;

        SelectCard();

        selectionMenu.SetActive(true);
    }
    //Gets triggerd by the three active Cards
    public void SelectedCardsCall(int cardNum) {
        if(waitForSelection)
            lastCardCall = cardNum;
    }
    public void ManageCardsInfoPanel(Pk_Scr pearkCard = null,GameObject itemCard = null) {
        if(cardInfoPanel.activeInHierarchy) {
            cardInfoPanel.SetActive(false);
            return;
        }

        if(pearkCard == null && itemCard == null)
            return;

        string cardTitle = pearkCard != null ? pearkCard.name:itemCard.name;
        string description = pearkCard != null ? pearkCard.description : itemCard.GetComponent<SO_Base>().settings.description;
        Sprite pfp = pearkCard != null ? pearkCard.icon : itemCard.GetComponent<SpriteRenderer>().sprite;

        titleInfoCard_txt.text = cardTitle;
        descriptionInfoCard_txt.text = description;
        infoCardPFP.sprite = pfp;

        infoCardPFP.SetNativeSize();
        cardInfoPanel.SetActive(true);
    }
    public void RemoveFromActive(int cardNum) {
        if(perkViewActive) {
            selectetPerks[cardNum] = null;
        }else
            selectetItems[cardNum] = null;
        UpdateSelectedCards();
        ManageCardsDownMenu(null);
    }
    public void ManageCardsDownMenu(GameObject newActiveCard) {
        if(oldActiveCard != null)
            oldActiveCard.SetActive(false);
        if(newActiveCard != null) {
            oldActiveCard = newActiveCard;
            oldActiveCard.SetActive(true);
        }
    }
    void BuildBaseCards(GameObject parent,List<GameObject> itemList = null,List<Pk_Scr> perkList = null) {

        int count;
        bool itm = false;

        if(itemList != null) {
            count = itemList.Count;
            itm = true;
        }
        else {
            count = perkList.Count;
        }

        float width = cardsBackground.rect.width;
        float hieght = cardsBackground.rect.height;

        float xDistance = width/3;
        float yDistance = hieght/2.5f;

        float startX = -((width+90) / 2)+xDistance/1.5f;
        float startY = yDistance/1.8f;
        float activeX = startX;
        float activeY = startY;

        for(int i = 0; i < count; i++) {
            GameObject tempCard = Instantiate(preCardButton);
            tempCard.transform.SetParent(parent.transform);
            CardButton tempButton = tempCard.GetComponent<CardButton>();
            tempButton.manager = this;
            tempButton.cardNum = i;

            Vector2 tempVec = new(activeX,activeY);
            tempCard.transform.localPosition = tempVec;
            tempCard.transform.localScale = Vector2.one;
            
            if(itm) {
                tempButton.activeItem = itemList[i];
                tempButton.GetComponent<Image>().sprite = itemList[i].GetComponent<SO_Base>().settings.cardSprite;
                itemCards.Add(tempCard);
            }
            else {
                tempButton.activePerk = perkList[i];
                tempButton.GetComponent<Image>().sprite = perkList[i].icon;
                perkCards.Add(tempCard);
            }

            activeX += xDistance;

            if(activeX > xDistance) {
                activeX = startX;
                activeY -= yDistance;

            }
        }
    }
    //For Buttons cuz they cant use the method
    public void CloseInfoPanel() {
        ManageCardsInfoPanel();
    }
    public void CloseCardMenu() {
        SaveSelection();

        ManageCardsInfoPanel();
        ResetCards();
    }

    public void SaveSelection() {
        gameManager.selectedPerks = selectetPerks;

        for(int i = 0;i < selectetItems.Length;i++) {
            if(selectetItems[i] != null && !F_GameObject.CheckArrayForObj(kajiaSys.itemList.ToArray(),selectetItems[i])) {
                kajiaSys.itemList.Add(selectetItems[i]);
            }
        }
    }
    public void SelectCard(bool setTo = false) {
        GameObject[] objectArray = perkViewActive ? perkCards.ToArray() : itemCards.ToArray();

        for(int i = 0; objectArray.Length > i; i++) {
            objectArray[i].SetActive(setTo);
        }
    }
}

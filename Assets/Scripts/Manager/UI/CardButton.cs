using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardButton : MonoBehaviour
{
    public int cardNum;
    public bool isLooked = false;
    public Pk_Scr activePerk;
    public GameObject activeItem;
    public CardMenuManager manager;
    public Sprite lookedSprite;
    [SerializeField] Sprite baseCardSpr;
    [SerializeField] GameObject downMenu;
    [SerializeField] TextMeshProUGUI cardName;
    public void ToggleDownMenu() {
        if(isLooked)
            return;
        manager.ManageCardsDownMenu(downMenu);
    }
    public void RemoveCard() {
        manager.RemoveFromActive(cardNum);
    }
    public void ToggleCardInfoMenu() {
        manager.ManageCardsInfoPanel(activePerk,activeItem);
    }
    public void TriggerSelect() {
        downMenu.SetActive(false);
        manager.CardFinalSelect(cardNum);
    }
    public void UpdateCard(Pk_Scr newActivePerk = null,GameObject newActiveItem = null) {
        if(newActivePerk == null && newActiveItem == null) {
            GetComponent<Image>().sprite = baseCardSpr;
            cardName.text = "";
            isLooked = true;
            return;
        }
        isLooked = false;
        if(newActivePerk != null) {
            GetComponent<Image>().sprite = newActivePerk.icon;
            activePerk = newActivePerk;
            cardName.text = activePerk.name;
            return;
        }
        GetComponent<Image>().sprite = newActiveItem.GetComponent<SpriteRenderer>().sprite;
        activeItem = newActiveItem;
        cardName.text = newActiveItem.name;
    }
}

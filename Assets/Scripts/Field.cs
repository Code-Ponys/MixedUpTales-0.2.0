using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public FieldProperties FP;
    public GameManager GameManager;
    public MousePos MP;

    public List<GameObject> cardsOnField = new List<GameObject>();

    // Use this for initialization
    void Start() {
        for (int x = (((FP._size - 1) / 2) * -1); x < ((FP._size - 1) / 2); x++) {
            for (int y = (((FP._size - 1) / 2) * -1); y < ((FP._size - 1) / 2); y++) {
                GameManager.GenerateFieldCard(CardID.FieldIndicator, x, y);
                GameManager.GenerateFieldCard(CardID.CardIndicator, x, y);
            }
        }
        GameManager.GenerateFieldCard(CardID.Startpoint, 0, 0);
        GameManager.currentChoosedCard = CardID.none;
    }

    // Update is called once per frame
    void Update() {
        //if (lastPlayer != RD.currentPlayer) {
        //    lastPlayer = RD.currentPlayer;
        //    CM.CenterCamera();
        //    print("Kamera korrigiert");
        //}
        if (GameManager.ChangePlayer.enabled == true
            || GameObject.Find("Field").GetComponent<GameManager>().WinScreen.enabled == true
            || GameObject.Find("Field").GetComponent<GameManager>().cardlocked == true) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (GameObject.Find("SideMenu Blue").GetComponent<SideBarMove>().panelactive == false
                || GameObject.Find("SideMenu Red").GetComponent<SideBarMove>().panelactive == false) {
                ToggleMouseField();
                if (GameManager.IsFieldOccupied(MP.x, MP.y) == false) {
                    GameManager.GenerateFieldCard(CardID.ChoosedCard, MP.x, MP.y);
                }
            }
        }
    }

    private Card GetCardData(int x, int y) {
        //foreach (Card card in cardsOnField) {
        //    if (card.x == x && card.y == y) {
        //        return card;
        //    }
        //}
        //cardsOnField.Find(index => x == 0 && y == 0);
        return null;
    }

    public void ToggleMouseField() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int indexX = RoundIt(mouseWorldPos.x);
        int indexY = RoundIt(mouseWorldPos.y);
        MP.x = indexX;
        MP.y = indexY;
    }

    public int RoundIt(float roundme) {
        float floatresult = 0;
        float remains = roundme % 1;
        if (roundme > 0) {
            if (remains < 0.5) {
                floatresult = roundme - remains;
            } else {
                floatresult = roundme + (1 - remains);
            }
        } else {
            if (remains > -0.5) {
                floatresult = roundme + (remains * -1);
            } else {
                floatresult = roundme - (1 + remains);
            }
        }
        int result = (int)floatresult;
        return result;
    }


}

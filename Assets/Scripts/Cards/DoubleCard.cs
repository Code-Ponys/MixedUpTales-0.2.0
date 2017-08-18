using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class DoubleCard : Card {
        GameObject OwnGO;
        private bool cardprocessdone;
        GameObject F;
        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            F.GetComponent<GameManager>().cardlocked = true;
            F.GetComponent<GameManager>().GenerateFieldCard(CardID.Blankcard, x, y);

        }

        // Update is called once per frame
        void Update() {
            if (cardprocessdone) return;
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (F.GetComponent<GameManager>().cardlocked == true) {
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int indexX = F.GetComponent<Field>().RoundIt(mouseWorldPos.x);
                    int indexY = F.GetComponent<Field>().RoundIt(mouseWorldPos.y);
                    GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, indexX, indexY));
                    GameObject FieldIndicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, indexX, indexY));
                    GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, indexX, indexY));
                    if (indexX == x && indexY == y) return;
                    if (Card != null) return;

                    if (FieldIndicator.GetComponent<Indicator>().currentcolor == IndicatorColor.green) {
                        print("Pointcard created");
                        F.GetComponent<GameManager>().GetPointCardNumber(team);
                        F.GetComponent<GameManager>().GenerateFieldCard(CardID.Pointcard, indexX, indexY);
                        cardprocessdone = true;
                        F.GetComponent<GameManager>().RemoveCard(OwnGO);
                    }
                }
            }
        }
    }
}

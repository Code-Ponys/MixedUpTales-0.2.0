using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class BurnCard : Card {

        GameObject OwnGO;
        GameObject F;
        GameObject CardIndicatorLeft;
        GameObject CardIndicatorRight;
        GameObject CardIndicatorUp;
        GameObject CardIndicatorDown;
        GameObject CardLeft;
        GameObject CardRight;
        GameObject CardUp;
        GameObject CardDown;
        GameObject FieldIndicatorLeft;
        GameObject FieldIndicatorRight;
        GameObject FieldIndicatorUp;
        GameObject FieldIndicatorDown;
        private bool cardprocessdone = false;

        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            F.GetComponent<GameManager>().cardlocked = true;
            CardIndicatorLeft = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x - 1, y));
            CardIndicatorRight = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x + 1, y));
            CardIndicatorUp = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y + 1));
            CardIndicatorDown = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y - 1));
            CardLeft = GameObject.Find(Slave.GetCardName(CardID.Card, x - 1, y));
            CardRight = GameObject.Find(Slave.GetCardName(CardID.Card, x + 1, y));
            CardUp = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 1));
            CardDown = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 1));
            FieldIndicatorLeft = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x - 1, y));
            FieldIndicatorRight = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x + 1, y));
            FieldIndicatorUp = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y + 1));
            FieldIndicatorDown = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y - 1));

            int cardcounter = 0;
            if (CardLeft == null) {
                cardcounter++;
            }
            if (CardRight == null) {
                cardcounter++;
            }
            if (CardDown == null) {
                cardcounter++;
            }
            if (CardUp == null) {
                cardcounter++;
            }
            if (cardcounter == 3) {
                if (CardLeft != null) {

                    F.GetComponent<GameManager>().CollectRemoveCard(CardLeft, CardAction.CardDeleted);
                }
                if (CardRight != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardRight.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardRight.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    F.GetComponent<GameManager>().CollectRemoveCard(CardRight, CardAction.CardDeleted);
                }
                if (CardDown != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardDown.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardDown.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    F.GetComponent<GameManager>().CollectRemoveCard(CardDown, CardAction.CardDeleted);
                }
                if (CardUp != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardUp.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardUp.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    F.GetComponent<GameManager>().CollectRemoveCard(CardUp, CardAction.CardDeleted);
                }

                F.GetComponent<GameManager>().animationDone = true;
                DestroyImmediate(OwnGO);
                return;
            } else {
                if (CardLeft != null) {
                    CardIndicatorLeft.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
                }
                if (CardRight != null) {
                    CardIndicatorRight.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
                }
                if (CardUp != null) {
                    CardIndicatorUp.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
                }
                if (CardDown != null) {
                    CardIndicatorDown.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
                }
            }

        }

        // Update is called once per frame
        void Update() {
            if (cardprocessdone) return;
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                print("Klick");
                if (F.GetComponent<GameManager>().cardlocked == true) {
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int indexX = F.GetComponent<Field>().RoundIt(mouseWorldPos.x);
                    int indexY = F.GetComponent<Field>().RoundIt(mouseWorldPos.y);
                    GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, indexX, indexY));
                    GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, indexX, indexY));

                    if (Card != null
                        && Card.GetComponent<Card>().team != Team.system
                        && CardIndicator.GetComponent<Indicator>().indicatorColor == IndicatorColor.yellowcovered) {
                        F.GetComponent<GameManager>().animationDone = true;
                        F.GetComponent<GameManager>().cardlocked = false;
                        cardprocessdone = true;
                        CardIndicatorLeft.GetComponent<Indicator>().setColor(IndicatorColor.transparent);
                        CardIndicatorRight.GetComponent<Indicator>().setColor(IndicatorColor.transparent);
                        CardIndicatorUp.GetComponent<Indicator>().setColor(IndicatorColor.transparent);
                        CardIndicatorDown.GetComponent<Indicator>().setColor(IndicatorColor.transparent);

                        F.GetComponent<GameManager>().CollectRemoveCard(Card, CardAction.CardDeleted);
                        DestroyImmediate(OwnGO);
                    }
                }
            }
        }
    }
}

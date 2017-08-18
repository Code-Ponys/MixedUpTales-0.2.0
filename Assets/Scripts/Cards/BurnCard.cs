using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class BurnCard : Card {

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
                print("CardLeft not found!");
                cardcounter++;
            }
            if (CardRight == null) {
                print("CardRight not found!");
                cardcounter++;
            }
            if (CardDown == null) {
                print("CardDown not found!");
                cardcounter++;
            }
            if (CardUp == null) {
                print("CardUp not found!");
                cardcounter++;
            }
            print("Cardcounter: " + cardcounter);
            if (cardcounter == 3) {
                if (CardLeft != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardLeft.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardLeft.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    DestroyImmediate(CardLeft);
                }
                if (CardRight != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardRight.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardRight.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    DestroyImmediate(CardRight);
                }
                if (CardDown != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardDown.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardDown.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    DestroyImmediate(CardDown);
                }
                if (CardUp != null) {
                    for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                        if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == CardUp.GetComponent<Card>().x
                            && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == CardUp.GetComponent<Card>().y) {
                            F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                            break;
                        }
                    }
                    DestroyImmediate(CardUp);
                }
                DestroyImmediate(GameObject.Find(Slave.GetCardName(CardID.Burncard, x, y)));

                F.GetComponent<GameManager>().animationDone = true;
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

                        for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                            if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == indexX
                                && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == indexY) {
                                F.GetComponent<Field>().cardsOnField.RemoveAt(i);
                                break;
                            }
                        }
                        if (Card.GetComponent<Card>().cardid == CardID.Blockcard) {
                            Block blockdirection = GameObject.Find(Slave.GetCardName(CardID.Card, x, y)).GetComponent<BlockCard>().blockDirection;
                            switch (blockdirection) {
                                case Block.right:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x + 1, y)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x + 1, y)).GetComponent<Indicator>().team = Team.system;
                                    break;
                                case Block.left:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x - 1, y)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x - 1, y)).GetComponent<Indicator>().team = Team.system;
                                    break;
                                case Block.up:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y + 1)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y + 1)).GetComponent<Indicator>().team = Team.system;
                                    break;
                                case Block.down:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y - 1)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y - 1)).GetComponent<Indicator>().team = Team.system;
                                    break;
                            }
                        }
                        DestroyImmediate(Card);
                        DestroyImmediate(GameObject.Find(Slave.GetCardName(CardID.Burncard, x, y)));
                    }
                }
            }
        }
    }
}

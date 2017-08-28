using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class ShuffleCard : Card {
        GameObject OwnGO;
        GameObject CardLeft;
        GameObject CardRight;
        GameObject CardDown;
        GameObject CardUp;
        GameObject CardIndicatorLeft;
        GameObject CardIndicatorRight;
        GameObject CardIndicatorDown;
        GameObject CardIndicatorUp;

        GameObject An_Shuffle;

        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            if (reconstructed) return;
            F.GetComponent<GameManager>().cardlocked = true;
            F.GetComponent<GameManager>().currentChoosedCard = CardID.none;
            CardLeft = GameObject.Find(Slave.GetCardName(CardID.Card, x - 1, y));
            CardRight = GameObject.Find(Slave.GetCardName(CardID.Card, x + 1, y));
            CardDown = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 1));
            CardUp = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 1));
            CardIndicatorLeft = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x - 1, y));
            CardIndicatorRight = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x + 1, y));
            CardIndicatorDown = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y - 1));
            CardIndicatorUp = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y + 1));

        }

        // Update is called once per frame
        void Update() {
            if (reconstructed) {
                if (animationActive)
                    IsSetAnimationEnd();
                return;
            }

            if (CardLeft != null && CardIndicatorLeft.GetComponent<Indicator>().indicatorColor != IndicatorColor.yellowcovered) {
                if (CardLeft.GetComponent<Card>().cardid == CardID.Pointcard || CardLeft.GetComponent<Card>().cardid == CardID.Blankcard) {
                    if (CardLeft.GetComponent<Card>().team == F.GetComponent<GameManager>().currentPlayer) {
                        CardIndicatorLeft.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                    }

                }
            }
            if (CardRight != null && CardIndicatorRight.GetComponent<Indicator>().indicatorColor != IndicatorColor.yellowcovered) {
                if (CardRight.GetComponent<Card>().cardid == CardID.Pointcard || CardRight.GetComponent<Card>().cardid == CardID.Blankcard) {
                    if (CardLeft.GetComponent<Card>().team == F.GetComponent<GameManager>().currentPlayer) {
                        CardIndicatorRight.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                    }
                }
            }
            if (CardDown != null && CardIndicatorDown.GetComponent<Indicator>().indicatorColor != IndicatorColor.yellowcovered) {
                if (CardDown.GetComponent<Card>().cardid == CardID.Pointcard || CardDown.GetComponent<Card>().cardid == CardID.Blankcard) {
                    if (CardLeft.GetComponent<Card>().team == F.GetComponent<GameManager>().currentPlayer) {
                        CardIndicatorDown.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                    }
                }
            }
            if (CardUp != null && CardIndicatorUp.GetComponent<Indicator>().indicatorColor != IndicatorColor.yellowcovered) {
                if (CardUp.GetComponent<Card>().cardid == CardID.Pointcard || CardUp.GetComponent<Card>().cardid == CardID.Blankcard) {
                    if (CardLeft.GetComponent<Card>().team == F.GetComponent<GameManager>().currentPlayer) {
                        CardIndicatorUp.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                    }
                }
            }

            if (cardprocessdone) return;
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (F.GetComponent<GameManager>().cardlocked == true) {
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int indexX = F.GetComponent<Field>().RoundIt(mouseWorldPos.x);
                    int indexY = F.GetComponent<Field>().RoundIt(mouseWorldPos.y);
                    GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, indexX, indexY));
                    GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, indexX, indexY));
                    if (Card == null) return;

                    if (CardIndicator.GetComponent<Indicator>().currentcolor == IndicatorColor.yellowcovered) {
                        GameObject FirstCard = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
                        GameObject SecondCard = Card;
                        int x1 = FirstCard.GetComponent<Card>().x;
                        int y1 = FirstCard.GetComponent<Card>().y;
                        int x2 = SecondCard.GetComponent<Card>().x;
                        int y2 = SecondCard.GetComponent<Card>().y;

                        FirstCard.transform.position = new Vector3(x2, y2, -2);
                        SecondCard.transform.position = new Vector3(x1, y1, -2);

                        FirstCard.name = Slave.GetCardName(CardID.Card, x2, y2);
                        SecondCard.name = Slave.GetCardName(CardID.Card, x1, y1);


                        FirstCard.GetComponent<Card>().x = x2;
                        FirstCard.GetComponent<Card>().y = y2;
                        SecondCard.GetComponent<Card>().x = x1;
                        SecondCard.GetComponent<Card>().y = y1;
                        //An_Shuffle = (GameObject)Instantiate(Resources.Load("Animations/AN_Shuffle"));
                        GameObject.Find("CardSwitch").GetComponent<AudioSource>().Play();

                        cardprocessdone = true;
                        F.GetComponent<GameManager>().currentChoosedCard = CardID.Shufflecard;
                        F.GetComponent<GameManager>().animationDone = true;
                        F.GetComponent<GameManager>().AddToCardsAffectedLastRound(FirstCard, CardAction.CardShuffled);
                        F.GetComponent<GameManager>().AddToCardsAffectedLastRound(SecondCard, CardAction.CardShuffled);
                        DestroyImmediate(OwnGO);
                    }
                }
            }
        }
    }
}
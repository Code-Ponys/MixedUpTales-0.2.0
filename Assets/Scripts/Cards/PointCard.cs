using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cards {

    public class PointCard : Card {
        GameObject OwnGO;
        GameObject Card;
        SpriteRenderer SpriteRenderer;

        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            GameObject F = GameObject.Find("Field");
            Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            SpriteRenderer = Card.GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(team, F.GetComponent<GameManager>().currentChoosedCardGO.GetComponent<Handcards>().PointCardCounter));
            if (WinCondition() == true) {
                F.GetComponent<GameManager>().WinScreen.enabled = true;
                string playerName;
                if (team == Team.blue) {
                    playerName = "Player 1" + "!";
                } else {
                    playerName = "Player 2" + "!";
                }
                GameObject.Find("PlayerNameWin").GetComponent<Text>().text = playerName;
            } else {
                if (F.GetComponent<GameManager>().currentChoosedCard != CardID.Doublecard) {
                    F.GetComponent<GameManager>().animationDone = true;
                }
            }

        }
        protected bool WinCondition() {
            //horizontal
            if (GameObject.Find("Card " + (x - 1) + "," + y) != null
                && GameObject.Find("Card " + (x - 1) + "," + y).GetComponent<Card>().team == team
                && GameObject.Find("Card " + (x - 1) + "," + y).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + (x - 2) + "," + y) != null
                    && GameObject.Find("Card " + (x - 2) + "," + y).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x - 2) + "," + y).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                } else {
                    if (GameObject.Find("Card " + (x + 1) + "," + y) != null
                        && GameObject.Find("Card " + (x + 1) + "," + y).GetComponent<Card>().team == team
                        && GameObject.Find("Card " + (x + 1) + "," + y).GetComponent<Card>().cardid == CardID.Pointcard) {
                        print("WIN °_°");
                        return true;
                    }
                }
            } else if (GameObject.Find("Card " + (x + 1) + "," + y) != null
                    && GameObject.Find("Card " + (x + 1) + "," + y).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x + 1) + "," + y).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + (x + 2) + "," + y) != null
                    && GameObject.Find("Card " + (x + 2) + "," + y).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x + 2) + "," + y).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                }
            }

            //vertikal
            if (GameObject.Find("Card " + x + "," + (y + 1)) != null
                && GameObject.Find("Card " + x + "," + (y + 1)).GetComponent<Card>().team == team
                && GameObject.Find("Card " + x + "," + (y + 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + x + "," + (y + 2)) != null
                    && GameObject.Find("Card " + x + "," + (y + 2)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + x + "," + (y + 2)).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                } else {
                    if (GameObject.Find("Card " + x + "," + (y - 1)) != null
                        && GameObject.Find("Card " + x + "," + (y - 1)).GetComponent<Card>().team == team
                        && GameObject.Find("Card " + x + "," + (y - 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                        print("WIN °_°");
                        return true;
                    }
                }
            } else if (GameObject.Find("Card " + x + "," + (y - 1)) != null
                    && GameObject.Find("Card " + x + "," + (y - 1)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + x + "," + (y - 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + x + "," + (y - 2)) != null
                    && GameObject.Find("Card " + x + "," + (y - 2)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + x + "," + (y - 2)).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                }
            }

            //diagonal links oben -> rechts unten
            if (GameObject.Find("Card " + (x - 1) + "," + (y + 1)) != null
                && GameObject.Find("Card " + (x - 1) + "," + (y + 1)).GetComponent<Card>().team == team
                && GameObject.Find("Card " + (x - 1) + "," + (y + 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + (x - 2) + "," + (y + 2)) != null
                    && GameObject.Find("Card " + (x - 2) + "," + (y + 2)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x - 2) + "," + (y + 2)).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                } else {
                    if (GameObject.Find("Card " + (x + 1) + "," + (y - 1)) != null
                        && GameObject.Find("Card " + (x + 1) + "," + (y - 1)).GetComponent<Card>().team == team
                        && GameObject.Find("Card " + (x + 1) + "," + (y - 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                        print("WIN °_°");
                        return true;
                    }
                }
            } else if (GameObject.Find("Card " + (x + 1) + "," + (y - 1)) != null
                    && GameObject.Find("Card " + (x + 1) + "," + (y - 1)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x + 1) + "," + (y - 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + (x + 2) + "," + (y - 2)) != null
                    && GameObject.Find("Card " + (x + 2) + "," + (y - 2)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x + 2) + "," + (y - 2)).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                }
            }

            //diagonal links unten -> rechts oben
            if (GameObject.Find("Card " + (x - 1) + "," + (y - 1)) != null
                && GameObject.Find("Card " + (x - 1) + "," + (y - 1)).GetComponent<Card>().team == team
                && GameObject.Find("Card " + (x - 1) + "," + (y - 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + (x - 2) + "," + (y - 2)) != null
                    && GameObject.Find("Card " + (x - 2) + "," + (y - 2)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x - 2) + "," + (y - 2)).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                } else {
                    if (GameObject.Find("Card " + (x + 1) + "," + (y + 1)) != null
                        && GameObject.Find("Card " + (x + 1) + "," + (y + 1)).GetComponent<Card>().team == team
                        && GameObject.Find("Card " + (x + 1) + "," + (y + 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                        print("WIN °_°");
                        return true;
                    }
                }
            } else if (GameObject.Find("Card " + (x + 1) + "," + (y + 1)) != null
                    && GameObject.Find("Card " + (x + 1) + "," + (y + 1)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x + 1) + "," + (y + 1)).GetComponent<Card>().cardid == CardID.Pointcard) {
                if (GameObject.Find("Card " + (x + 2) + "," + (y + 2)) != null
                    && GameObject.Find("Card " + (x + 2) + "," + (y + 2)).GetComponent<Card>().team == team
                    && GameObject.Find("Card " + (x + 2) + "," + (y + 2)).GetComponent<Card>().cardid == CardID.Pointcard) {
                    print("WIN °_°");
                    return true;
                }
            }
            return false;
        }
    }
}



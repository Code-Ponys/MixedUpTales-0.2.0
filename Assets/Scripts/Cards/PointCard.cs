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
        AudioSource Music;



        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            if (reconstructed) {
                PointCardCounter = F.GetComponent<GameManager>().PointCardCounterCurrent;
                Card = GameObject.Find(Slave.GetCardName(CardID.Reconstruct, x, y));
                SpriteRenderer = Card.GetComponent<SpriteRenderer>();
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(team, PointCardCounter));
                return;
            }
            DeactivateSlider();
            PointCardCounter = F.GetComponent<GameManager>().currentChoosedCardGO.GetComponent<Handcards>().PointCardCounter;
            Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            SpriteRenderer = Card.GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(team, PointCardCounter));

            SetAnimationStart();





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

        void Update() {
            if (reconstructed) {
                if (animationActive)
                    IsSetAnimationEnd();
                return;
            }
            if (cardprocessdone) return;

            if (IsSetAnimationEnd()) {
                if (WinCondition() == true) {
                    string playerName;
                    if (team == Team.blue) {
                        playerName = "Player 1" + "!";
                    } else {
                        playerName = "Player 2" + "!";
                    }
                    Debug.Log(playerName);
                    GameObject.Find("PlayerNameWin").GetComponent<Text>().text = playerName;
                    F.GetComponent<GameManager>().WinScreen.enabled = true;
                    Music = GameObject.Find("Sound_Win").GetComponent<AudioSource>();
                    Music.Play();
                }

                cardprocessdone = true;
                if (F.GetComponent<GameManager>().currentChoosedCard != CardID.Doublecard) {
                    AnimationDone();
                }

                //Shine.GetComponent<SpriteRenderer>().enabled = false;

            }
        }
    }
}
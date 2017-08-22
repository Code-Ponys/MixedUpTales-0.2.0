using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

namespace Cards {

    public class PointCard : Card {
        GameObject OwnGO;
        GameObject Card;
        SpriteRenderer SpriteRenderer;
        GameObject Shine;
        Animator An;
        bool cardprocessdone;


        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            GameObject F = GameObject.Find("Field");
            Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            SpriteRenderer = Card.GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(team, F.GetComponent<GameManager>().currentChoosedCardGO.GetComponent<Handcards>().PointCardCounter));

            Shine = (GameObject)Instantiate(Resources.Load("Animations/AN_Shine"));
            An = Shine.GetComponent<Animator>();

            Shine.transform.position = new Vector3(x, y, -3);
            


            if (WinCondition() == true) {
                F.GetComponent<GameManager>().WinScreen.enabled = true;
                string playerName;
                if (team == Team.blue) {
                    playerName = "Player 1" + "!";
                } else {
                    playerName = "Player 2" + "!";
                }
                GameObject.Find("PlayerNameWin").GetComponent<Text>().text = playerName;
               

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

        void Update() {
            if (cardprocessdone) return;

            GameObject F = GameObject.Find("Field");
            if (F.GetComponent<GameManager>().currentChoosedCard != CardID.Doublecard) {

                if (An.GetCurrentAnimatorStateInfo(0).IsName("end")) {
                   
                    F.GetComponent<GameManager>().animationDone = true;
                    cardprocessdone = true;
                    Destroy(Shine);
                }
            }
        }
    }
}






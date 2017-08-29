using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Cards {
    public class InfernoCard : Card {

        GameObject OwnGO;
        GameObject CardLeft1;
        GameObject CardLeft2;
        GameObject CardLeft3;
        GameObject CardRight1;
        GameObject CardRight2;
        GameObject CardRight3;
        GameObject CardDown1;
        GameObject CardDown2;
        GameObject CardDown3;
        GameObject CardUp1;
        GameObject CardUp2;
        GameObject CardUp3;
        GameObject CardIndicatorLeft1;
        GameObject CardIndicatorLeft2;
        GameObject CardIndicatorLeft3;
        GameObject CardIndicatorRight1;
        GameObject CardIndicatorRight2;
        GameObject CardIndicatorRight3;
        GameObject CardIndicatorDown1;
        GameObject CardIndicatorDown2;
        GameObject CardIndicatorDown3;
        GameObject CardIndicatorUp1;
        GameObject CardIndicatorUp2;
        GameObject CardIndicatorUp3;

        GameObject An_Inferno;

        SkeletonAnimation skeletonAnimation;

        Spine.AnimationState AS;

        int XCord;
        int YCord;

        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            if (reconstructed) return;
            DeactivateSlider();
            F.GetComponent<GameManager>().cardlocked = true;
            CardLeft1 = GameObject.Find(Slave.GetCardName(CardID.Card, x - 1, y));
            CardLeft2 = GameObject.Find(Slave.GetCardName(CardID.Card, x - 2, y));
            CardLeft3 = GameObject.Find(Slave.GetCardName(CardID.Card, x - 3, y));
            CardRight1 = GameObject.Find(Slave.GetCardName(CardID.Card, x + 1, y));
            CardRight2 = GameObject.Find(Slave.GetCardName(CardID.Card, x + 2, y));
            CardRight3 = GameObject.Find(Slave.GetCardName(CardID.Card, x + 3, y));
            CardDown1 = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 1));
            CardDown2 = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 2));
            CardDown3 = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 3));
            CardUp1 = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 1));
            CardUp2 = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 2));
            CardUp3 = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 3));
            CardIndicatorLeft1 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x - 1, y));
            CardIndicatorLeft2 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x - 2, y));
            CardIndicatorLeft3 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x - 3, y));
            CardIndicatorRight1 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x + 1, y));
            CardIndicatorRight2 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x + 2, y));
            CardIndicatorRight3 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x + 3, y));
            CardIndicatorDown1 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y - 1));
            CardIndicatorDown2 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y - 2));
            CardIndicatorDown3 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y - 3));
            CardIndicatorUp1 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y + 1));
            CardIndicatorUp2 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y + 2));
            CardIndicatorUp3 = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y + 3));

            XCord = OwnGO.GetComponent<Card>().x;
            YCord = OwnGO.GetComponent<Card>().y;

            if (CardLeft1 != null || CardLeft2 != null || CardLeft3 != null) {
                CardIndicatorLeft1.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorLeft2.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorLeft3.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
            }
            if (CardRight1 != null || CardRight2 != null || CardRight3 != null) {
                CardIndicatorRight1.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorRight2.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorRight3.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
            }
            if (CardUp1 != null || CardUp2 != null || CardUp3 != null) {
                CardIndicatorUp1.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorUp2.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorUp3.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
            }
            if (CardDown1 != null || CardDown2 != null || CardDown3 != null) {
                CardIndicatorDown1.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorDown2.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
                CardIndicatorDown3.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
            }
        }

        // Update is called once per frame
        void Update() {
            if (reconstructed) {
                if (animationActive)
                    IsSetAnimationEnd();
                return;
            }
            if (cardprocessdone) return;
            if (Input.GetKeyDown(KeyCode.Mouse0)) {


                if (F.GetComponent<GameManager>().cardlocked == true) {
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int indexX = F.GetComponent<Field>().RoundIt(mouseWorldPos.x);
                    int indexY = F.GetComponent<Field>().RoundIt(mouseWorldPos.y);
                    GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, indexX, indexY));
                    GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, indexX, indexY));

                    if (CardIndicator.GetComponent<Indicator>().indicatorColor != IndicatorColor.yellowcovered) return;
                    An_Inferno = (GameObject)Instantiate(Resources.Load("Animations/AN_Inferno"));

                    skeletonAnimation = An_Inferno.GetComponent<SkeletonAnimation>();
                    AS = skeletonAnimation.state;

                    if (CardIndicator.name == CardIndicatorLeft1.name
                        || CardIndicator.name == CardIndicatorLeft2.name
                        || CardIndicator.name == CardIndicatorLeft3.name) {

                        //An_Inferno.transform.Rotate(0, 0, -45);
                        //An_Inferno.transform.position = new Vector3((XCord - 3.5f), (YCord - 0.5f), -3);
                        An_Inferno.transform.Rotate(0, 0, 0);
                        An_Inferno.transform.position = new Vector3((XCord - 3f), (YCord - 1.8f), -3);

                        F.GetComponent<GameManager>().CollectRemoveCard(CardLeft1, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardLeft2, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardLeft3, CardAction.CardDeleted);
                        cardprocessdone = true;
                        //F.GetComponent<GameManager>().animationDone = true;
                        //HideCardIndicator();
                        //DestroyImmediate(OwnGO);
                    } else if (CardIndicator.name == CardIndicatorRight1.name
                          || CardIndicator.name == CardIndicatorRight2.name
                          || CardIndicator.name == CardIndicatorRight3.name) {

                        //An_Inferno.transform.Rotate(0, 0, 135);
                        //An_Inferno.transform.position = new Vector3((XCord + 3.5f), (YCord + 0.5f), -3);
                        An_Inferno.transform.Rotate(0, 0, 180);
                        An_Inferno.transform.position = new Vector3((XCord + 3), (YCord + 1.8f), -3);

                        F.GetComponent<GameManager>().CollectRemoveCard(CardRight1, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardRight2, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardRight3, CardAction.CardDeleted);
                        cardprocessdone = true;
                        //F.GetComponent<GameManager>().animationDone = true;
                        //HideCardIndicator();
                        //DestroyImmediate(OwnGO);

                    } else if (CardIndicator.name == CardIndicatorUp1.name
                          || CardIndicator.name == CardIndicatorUp2.name
                          || CardIndicator.name == CardIndicatorUp3.name) {

                        //An_Inferno.transform.Rotate(0, 0, -135);
                        An_Inferno.transform.Rotate(0, 0, -90);
                        An_Inferno.transform.position = new Vector3((XCord - 1.8f), (YCord + 3), -3);

                        F.GetComponent<GameManager>().CollectRemoveCard(CardUp1, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardUp2, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardUp3, CardAction.CardDeleted);
                        cardprocessdone = true;
                        //F.GetComponent<GameManager>().animationDone = true;
                        //HideCardIndicator();
                        //DestroyImmediate(OwnGO);
                    } else if (CardIndicator.name == CardIndicatorDown1.name
                          || CardIndicator.name == CardIndicatorDown2.name
                          || CardIndicator.name == CardIndicatorDown3.name) {

                        //An_Inferno.transform.Rotate(0, 0, 45);
                        An_Inferno.transform.Rotate(0, 0, 90);
                        An_Inferno.transform.position = new Vector3((XCord + 01.8f), (YCord - 3f), -3);

                        F.GetComponent<GameManager>().CollectRemoveCard(CardDown1, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardDown2, CardAction.CardDeleted);
                        F.GetComponent<GameManager>().CollectRemoveCard(CardDown3, CardAction.CardDeleted);
                        cardprocessdone = true;
                        //F.GetComponent<GameManager>().animationDone = true;
                        //HideCardIndicator();
                        //DestroyImmediate(OwnGO);
                    }

                    AS.Complete += delegate {

                        AnimationDone();
                        Destroy(An_Inferno);
                        HideCardIndicator();
                        DestroyImmediate(OwnGO);

                    };
                }
            }
        }

        void HideCardIndicator() {
            CardIndicatorLeft1.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorLeft2.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorLeft3.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorRight1.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorRight2.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorRight3.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorUp1.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorUp2.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorUp3.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorDown1.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorDown2.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            CardIndicatorDown3.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;

        }
    }
}



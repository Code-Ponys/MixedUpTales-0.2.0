using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Cards {

    public class BlockCard : Card {

        GameObject OwnGO;

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

        GameObject An_Block;

        AudioSource Sound;
        SkeletonAnimation skeletonAnimation;

        Spine.AnimationState AS;

        public Block blockDirection;

        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");

            SetAnimationStart();

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
            if (CardLeft == null && FieldIndicatorLeft != null
                && FieldIndicatorLeft.GetComponent<Indicator>().team == Team.system
                && FieldIndicatorLeft.GetComponent<Indicator>().currentcolor != IndicatorColor.red) {
                CardIndicatorLeft.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
            }
            if (CardRight == null && FieldIndicatorLeft != null
                && FieldIndicatorRight.GetComponent<Indicator>().team == Team.system
                && FieldIndicatorRight.GetComponent<Indicator>().currentcolor != IndicatorColor.red) {
                CardIndicatorRight.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
            }
            if (CardUp == null && FieldIndicatorLeft != null
                && FieldIndicatorUp.GetComponent<Indicator>().team == Team.system
                && FieldIndicatorUp.GetComponent<Indicator>().currentcolor != IndicatorColor.red) {
                CardIndicatorUp.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
            }
            if (CardDown == null && FieldIndicatorLeft != null
                && FieldIndicatorDown.GetComponent<Indicator>().team == Team.system
                && FieldIndicatorDown.GetComponent<Indicator>().currentcolor != IndicatorColor.red) {
                CardIndicatorDown.GetComponent<Indicator>().setColor(IndicatorColor.yellowcovered);
            }
        }

        void Update() {
            if (cardprocessdone) return;

            if (An.GetCurrentAnimatorStateInfo(0).IsName("end")) {
                Shine.GetComponent<SpriteRenderer>().enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (F.GetComponent<GameManager>().cardlocked == true) {

                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int indexX = F.GetComponent<Field>().RoundIt(mouseWorldPos.x);
                    int indexY = F.GetComponent<Field>().RoundIt(mouseWorldPos.y);
                    GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, indexX, indexY));
                    GameObject FieldIndicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, indexX, indexY));
                    GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, indexX, indexY));

                    An_Block = (GameObject)Instantiate(Resources.Load("Animations/AN_Block"));

                    Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
                    skeletonAnimation = An_Block.GetComponent<SkeletonAnimation>();
                    AS = skeletonAnimation.state;

                    if (CardIndicator.GetComponent<Indicator>().indicatorColor == IndicatorColor.yellowcovered && Card == null) {

                        An_Block.transform.position = new Vector3((indexX), (indexY), -3);

                        skeletonAnimation.AnimationState.SetAnimation(0, "From Up Falling", false);
                        Sound.Play();

                        cardprocessdone = true;
                        CardIndicatorLeft.GetComponent<Indicator>().setColor(IndicatorColor.transparent);
                        CardIndicatorRight.GetComponent<Indicator>().setColor(IndicatorColor.transparent);
                        CardIndicatorUp.GetComponent<Indicator>().setColor(IndicatorColor.transparent);
                        CardIndicatorDown.GetComponent<Indicator>().setColor(IndicatorColor.transparent);

                        if (CardIndicator.name == CardIndicatorDown.name) {
                            blockDirection = Block.down;
                        }
                        if (CardIndicator.name == CardIndicatorUp.name) {
                            blockDirection = Block.up;
                        }
                        if (CardIndicator.name == CardIndicatorLeft.name) {
                            blockDirection = Block.left;
                        }
                        if (CardIndicator.name == CardIndicatorRight.name) {
                            blockDirection = Block.right;
                        }

                        FieldIndicator.GetComponent<Indicator>().indicatorState = IndicatorState.blocked;
                        FieldIndicator.GetComponent<Indicator>().team = F.GetComponent<GameManager>().currentPlayer;

                        Destroy(Shine);

                        AS.Complete += delegate {
                            print("animation end");

                            F.GetComponent<GameManager>().animationDone = true;
                            Destroy(An_Block);
                            AnimationDone();

                        };

                        F.GetComponent<GameManager>().cardlocked = false;
                    }

                }
            }
        }
    }
}



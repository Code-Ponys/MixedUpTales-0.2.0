using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class Indicator : MonoBehaviour {
        int x;
        int y;

        public IndicatorState indicatorState = IndicatorState.unreachable;

        public bool isFieldDeleteable = false;

        public Team team = Team.system;

        public IndicatorType indicatorType;

        public bool reachableMarked = false;
        public bool anchorvisible = false;
        public bool placeallow = false;

        public IndicatorColor indicatorColor;
        public IndicatorColor currentcolor = IndicatorColor.transparent;

        GameObject Card;
        SpriteRenderer SpriteRenderer;

        // Use this for initialization
        void Start() {
            if (indicatorType == IndicatorType.card) {
                Card = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y));

            } else {
                Card = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y));
            }
            SpriteRenderer = Card.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update() {
            if (indicatorType == IndicatorType.field) {
                FieldStateUpdate();
            }
            if (indicatorType == IndicatorType.card) {
                CardStateUpdate();
            }

        }


        public void setData(int xcord, int ycord, Team cardteam, IndicatorType type, IndicatorColor color) {
            x = xcord;
            y = ycord;
            team = cardteam;
            indicatorType = type;
            currentcolor = color;
        }
        private void CardStateUpdate() {
            if (currentcolor != indicatorColor) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(indicatorColor));
                print("Changed currentcolor " + currentcolor + " of Card " + Card.name + " to " + indicatorColor);
                currentcolor = indicatorColor;
            }
        }


        void FieldStateUpdate() {
            if (indicatorState == IndicatorState.blocked
                && GameObject.Find("Field").GetComponent<GameManager>().currentPlayer == team
                && currentcolor != IndicatorColor.green) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.green));
                currentcolor = IndicatorColor.green;
            } else if (indicatorState == IndicatorState.blocked
                  && GameObject.Find("Field").GetComponent<GameManager>().currentPlayer != team
                  && currentcolor != IndicatorColor.red) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.red));
                currentcolor = IndicatorColor.red;
            } else if (indicatorState == IndicatorState.anchorfield
                  && GameObject.Find("Field").GetComponent<GameManager>().currentChoosedCard == CardID.Anchorcard
                  && anchorvisible == false) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.green));
                anchorvisible = true;
            } else if (anchorvisible == true
                  && indicatorState == IndicatorState.anchorfield
                  && GameObject.Find("Field").GetComponent<GameManager>().currentChoosedCard != CardID.Anchorcard) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.transparent));
                anchorvisible = false;
            } else if (indicatorState == IndicatorState.reachable
                  && currentcolor != IndicatorColor.green) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.green));
                currentcolor = IndicatorColor.green;
            } else if (indicatorState == IndicatorState.unreachable
                  && currentcolor != IndicatorColor.transparent) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.transparent));
                currentcolor = IndicatorColor.transparent;
            } else if (indicatorState == IndicatorState.anchorfield
                  && currentcolor == IndicatorColor.green) {
                SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(IndicatorColor.transparent));
                currentcolor = IndicatorColor.transparent;
            }
        }

        public void setColor(IndicatorColor color) {
            indicatorColor = color;
        }
    }
}

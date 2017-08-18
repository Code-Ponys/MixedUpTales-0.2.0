using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class DeleteCard : Card {


        // Use this for initialization
        void Start() {
        GameObject F = GameObject.Find("Field");
        GameObject Card = GameObject.Find("Card " + x + "," + y);
            if (Card != null) {
                for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                    if (F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == x
                        && F.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == y) {
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
                Destroy(GameObject.Find(Slave.GetCardName(CardID.Card, x, y)));
            }
            Destroy(GameObject.Find(Slave.GetCardName(CardID.Deletecard, x, y)));
            F.GetComponent<GameManager>().animationDone = true;
        }


        // Update is called once per frame
        void Update() {

        }
    }
}

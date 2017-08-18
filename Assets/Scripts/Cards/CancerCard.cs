using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class CancerCard : Card {

        GameObject F;
        GameObject Card;
        GameObject Cardchange;
        SpriteRenderer SpriteRenderer;


        // Use this for initialization
        void Start() {
            F = GameObject.Find("Field");
            Card = GameObject.Find(Slave.GetCardName(CardID.Changecard, x, y));

            for (int i = 0; i < F.GetComponent<Field>().cardsOnField.Count; i++) {
                Cardchange = F.GetComponent<Field>().cardsOnField[i];
                if (Cardchange.GetComponent<Card>().x == x || Cardchange.GetComponent<Card>().y == y) {
                    if (Cardchange.GetComponent<Card>().cardid == CardID.Startpoint || Cardchange.GetComponent<Card>().cardid == CardID.Blankcard) {
                        continue;
                    }
                    int ycord = Cardchange.GetComponent<Card>().y;
                    Team cardteam = Cardchange.GetComponent<Card>().team;
                    int xcord = Cardchange.GetComponent<Card>().x;
                    switch (Cardchange.GetComponent<Card>().cardid) {
                        case CardID.Blockcard:
                            Block blockdirection = Cardchange.GetComponent<BlockCard>().blockDirection;
                            switch (blockdirection) {
                                case Block.right:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x + 1, Cardchange.GetComponent<Card>().y)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x + 1, Cardchange.GetComponent<Card>().y)).GetComponent<Indicator>().team = Team.system;
                                    break;
                                case Block.left:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x - 1, Cardchange.GetComponent<Card>().y)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x - 1, Cardchange.GetComponent<Card>().y)).GetComponent<Indicator>().team = Team.system;
                                    break;
                                case Block.up:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x, Cardchange.GetComponent<Card>().y + 1)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x, Card.GetComponent<Card>().y + 1)).GetComponent<Indicator>().team = Team.system;
                                    break;
                                case Block.down:
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x, Cardchange.GetComponent<Card>().y - 1)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, Cardchange.GetComponent<Card>().x, Cardchange.GetComponent<Card>().y - 1)).GetComponent<Indicator>().team = Team.system;
                                    break;
                            }

                            DestroyImmediate(Cardchange.GetComponent<BlockCard>());
                            break;
                        case CardID.Anchorcard:
                            DestroyImmediate(Cardchange.GetComponent<AnchorCard>());
                            break;
                        case CardID.Pointcard:
                            DestroyImmediate(Cardchange.GetComponent<PointCard>());
                            break;
                    }
                    Cardchange.AddComponent<BlankCard>();
                    Cardchange.GetComponent<Card>().y = ycord;
                    Cardchange.GetComponent<Card>().x = xcord;
                    Cardchange.GetComponent<Card>().team = cardteam;
                    Cardchange.GetComponent<Card>().cardid = CardID.Blankcard;
                    SpriteRenderer = Cardchange.GetComponent<SpriteRenderer>();
                    SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(CardID.Blankcard, cardteam));
                }
            }
            F.GetComponent<GameManager>().lastSetCard = CardID.Cancercard;
            F.GetComponent<GameManager>().animationDone = true;
            F.GetComponent<GameManager>().RemoveCard(GameObject.Find(Slave.GetCardName(CardID.Cancercard, x, y)));
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

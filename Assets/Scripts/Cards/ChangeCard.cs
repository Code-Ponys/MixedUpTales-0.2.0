using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards {
    public class ChangeCard : Card {

        GameObject F;
        GameObject Card;
        GameObject Cardbelow;
        SpriteRenderer SpriteRenderer;
        private bool cardprocessdone;

        // Use this for initialization
        void Start() {
            F = GameObject.Find("Field");
            Card = GameObject.Find(Slave.GetCardName(CardID.Changecard, x, y));

            Cardbelow = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            int ycord = Cardbelow.GetComponent<Card>().y;
            Team cardteam = Cardbelow.GetComponent<Card>().team;
            int xcord = Cardbelow.GetComponent<Card>().x;
            switch (Card.GetComponent<Card>().cardid) {
                case CardID.Blockcard:
                    Destroy(Cardbelow.GetComponent<BlockCard>());
                    break;
                case CardID.Anchorcard:
                    Destroy(Cardbelow.GetComponent<AnchorCard>());
                    break;
                case CardID.Pointcard:
                    Destroy(Cardbelow.GetComponent<PointCard>());
                    break;
            }
            Cardbelow.AddComponent<BlankCard>();
            Cardbelow.GetComponent<Card>().y = ycord;
            Cardbelow.GetComponent<Card>().x = xcord;
            Cardbelow.GetComponent<Card>().team = cardteam;
            Cardbelow.GetComponent<Card>().cardid = CardID.Blankcard;
            SpriteRenderer = Cardbelow.GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(CardID.Blankcard, cardteam));
            F.GetComponent<GameManager>().animationDone = true;

            F.GetComponent<GameManager>().RemoveCard(Card);
        }

        // Update is called once per frame
        void Update() {
        }
    }
}

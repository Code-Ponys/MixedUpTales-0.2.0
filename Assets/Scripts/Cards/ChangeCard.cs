using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards {
    public class ChangeCard : Card {

        GameObject OwnGO;
        GameObject Cardbelow;
        SpriteRenderer SpriteRenderer;

        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            Cardbelow = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            int ycord = Cardbelow.GetComponent<Card>().y;
            int xcord = Cardbelow.GetComponent<Card>().x;
            Team cardteam = Cardbelow.GetComponent<Card>().team;
            switch (Cardbelow.GetComponent<Card>().cardid) {
                case CardID.Blockcard:
                    DestroyImmediate(Cardbelow.GetComponent<BlockCard>());
                    break;
                case CardID.Anchorcard:
                    DestroyImmediate(Cardbelow.GetComponent<AnchorCard>());
                    break;
                case CardID.Pointcard:
                    DestroyImmediate(Cardbelow.GetComponent<PointCard>());
                    break;
            }
            Cardbelow.AddComponent<BlankCard>();
            Cardbelow.GetComponent<Card>().y = ycord;
            Cardbelow.GetComponent<Card>().x = xcord;
            Cardbelow.GetComponent<Card>().team = cardteam;
            Cardbelow.GetComponent<Card>().cardid = CardID.Blankcard;
            Cardbelow.GetComponent<Card>().cardprocessdone = true;
            F.GetComponent<GameManager>().AddToCardsAffectedLastRound(Cardbelow, CardAction.CardChanged);
            SpriteRenderer = Cardbelow.GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Resources.Load<Sprite>(Slave.GetImagePath(CardID.Blankcard, cardteam));
            F.GetComponent<GameManager>().animationDone = true;

            DestroyImmediate(OwnGO);
        }

        // Update is called once per frame
        void Update() {
        }
    }
}

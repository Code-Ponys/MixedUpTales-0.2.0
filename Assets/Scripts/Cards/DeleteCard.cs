using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class DeleteCard : Card {
        GameObject OwnGO;


        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            GameObject F = GameObject.Find("Field");
            GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            F.GetComponent<GameManager>().RemoveCard(Card);
            F.GetComponent<GameManager>().animationDone = true;
            F.GetComponent<GameManager>().RemoveCard(OwnGO);
        }


        // Update is called once per frame
        void Update() {

        }
    }
}

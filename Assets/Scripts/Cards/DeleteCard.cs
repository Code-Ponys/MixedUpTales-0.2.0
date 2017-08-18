using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class DeleteCard : Card {


        // Use this for initialization
        void Start() {
            GameObject F = GameObject.Find("Field");
            GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
            F.GetComponent<GameManager>().RemoveCard(Card);
            F.GetComponent<GameManager>().RemoveCard(GameObject.Find(Slave.GetCardName(CardID.Deletecard, x, y)));
            F.GetComponent<GameManager>().animationDone = true;
        }


        // Update is called once per frame
        void Update() {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards {
    public class NukeCard : Card {

        GameObject OwnGO;

        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            GameObject F = GameObject.Find("Field");
            while (F.GetComponent<Field>().cardsOnField.Count != 0) {
                GameObject RemoveCard = F.GetComponent<Field>().cardsOnField[0];
                F.GetComponent<GameManager>().RemoveCard(RemoveCard);
            }
            F.GetComponent<GameManager>().animationDone = true;
            F.GetComponent<GameManager>().RemoveCard(OwnGO);
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

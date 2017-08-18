using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class BlankCard : Card {
        GameObject OwnGO;

        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            GameObject F = GameObject.Find("Field");
            if (F.GetComponent<GameManager>().currentChoosedCard != CardID.Doublecard) {
            F.GetComponent<GameManager>().animationDone = true;
            }
        }
    }
}

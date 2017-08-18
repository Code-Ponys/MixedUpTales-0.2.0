using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class BlankCard : Card {

        private void Start() {
            GameObject F = GameObject.Find("Field");
            if (F.GetComponent<GameManager>().currentChoosedCard != CardID.Doublecard) {
            F.GetComponent<GameManager>().animationDone = true;
            }
        }
    }
}

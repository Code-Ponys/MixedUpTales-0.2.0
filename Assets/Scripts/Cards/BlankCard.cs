using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class BlankCard : Card {
        GameObject OwnGO;

        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");

            if (F.GetComponent<GameManager>().lastSetCard != CardID.Changecard
                && F.GetComponent<GameManager>().lastSetCard != CardID.Cancercard) {
                SetAnimationStart();
            }
        }

        void Update() {
            if (cardprocessdone) return;

            if (IsSetAnimationEnd()) {
                cardprocessdone = true;
                if (F.GetComponent<GameManager>().lastSetCard != CardID.Changecard
                    && F.GetComponent<GameManager>().lastSetCard != CardID.Cancercard
                    && F.GetComponent<GameManager>().lastSetCard != CardID.Doublecard) {
                    AnimationDone();
                }

            }
        }
    }
}

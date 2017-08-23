using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class BlankCard : Card {
        GameObject OwnGO;
        bool cardprocessdone;

        private void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");

            SetAnimationStart();
        }

        void Update() {
            if (cardprocessdone) return;

            if (IsSetAnimationEnd()) {
                cardprocessdone = true;
                if (F.GetComponent<GameManager>().currentChoosedCard != CardID.Doublecard) {
                    AnimationDone();
                }

            }
        }
    }
}

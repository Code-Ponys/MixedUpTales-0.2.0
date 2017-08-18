using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards {
    public class Startpoint : Card {
        GameObject OwnGO;

        public void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
        }
    }
}
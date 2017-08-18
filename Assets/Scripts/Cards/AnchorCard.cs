using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards {
    public class AnchorCard : Card {
        GameObject OwnGO;

        // Use this for initialization
        void Start() {
            GameObject F = GameObject.Find("Field");
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F.GetComponent<GameManager>().animationDone = true;

        }

        // Update is called once per frame
        void Update() {

        }
        
    }
}

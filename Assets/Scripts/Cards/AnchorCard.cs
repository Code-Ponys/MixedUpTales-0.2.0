using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards {
    public class AnchorCard : Card {

        // Use this for initialization
        void Start() {
            GameObject F = GameObject.Find("Field");
            F.GetComponent<GameManager>().animationDone = true;

        }

        // Update is called once per frame
        void Update() {

        }
        
    }
}

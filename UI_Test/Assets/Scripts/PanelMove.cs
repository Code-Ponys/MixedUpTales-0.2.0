using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMove : MonoBehaviour {

    private void Start() {
        transform.position = new Vector3(11, 0, 0);
    }

    public void OnMouseEnter() {
        Vector3 point = new Vector3(11, 0, 0);
        Vector3 goal = new Vector3(6, 0, 0);
        Physics.queriesHitTriggers = true;
        point = this.transform.position;
        if (point != goal) {
            transform.Translate(-5, 0, 0);
        }
    }

    public void OnMouseExit() {
        Physics.queriesHitTriggers = true;
        transform.Translate(2.5f, 0, 0);
    }
   
}

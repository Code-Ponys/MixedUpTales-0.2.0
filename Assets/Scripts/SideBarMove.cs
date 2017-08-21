using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBarMove : MonoBehaviour {

    public bool panelactive;
    public Image CardPreview;
    public Text CardPreviewText;

    private void Start() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(240, 0, 0);
    }

    public void MovePanelOut() {
        Vector3 goal = new Vector3(240, 0, 0);
        Vector3 point = new Vector3(-300, 0, 0);
        Physics.queriesHitTriggers = true;
        if (point != goal) {
            RectTransform var = GetComponent<RectTransform>();
            var.anchoredPosition = goal;
        }
        panelactive = false;
        CardPreview.enabled = true;
        CardPreviewText.enabled = true;

    }

    public void MovePanelIn() {
        Vector3 point = new Vector3(240, 0, 0);
        Vector3 goal = new Vector3(-300, 0, 0);
        Physics.queriesHitTriggers = true;
        if (point != goal) {
            RectTransform var = GetComponent<RectTransform>();
            var.anchoredPosition = goal;
        }
        panelactive = true;
        CardPreview.enabled = false;
        CardPreviewText.enabled = false;
    }
}

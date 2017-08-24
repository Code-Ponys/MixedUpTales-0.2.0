using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class SideBarMove : MonoBehaviour {

    public bool panelactive;
    public Image CardPreview;
    public Text CardPreviewText;
    bool AnimationPlayed;

    GameObject An_Hot;

    AudioSource Sound;
    SkeletonAnimation skeletonAnimation;

    Spine.AnimationState AS;

    private void Start() {

        AnimationPlayed = false;
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

        if (GameObject.Find("Field").GetComponent<GameManager>().lastSetCard == CardID.HotPotatoe && AnimationPlayed == false) {
            An_Hot = (GameObject)Instantiate(Resources.Load("Animations/AN_Hot"));

            //Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
            skeletonAnimation = An_Hot.GetComponent<SkeletonAnimation>();
            AS = skeletonAnimation.state;

            AS.Complete += delegate {
                
                GameObject.Find("Field").GetComponent<GameManager>().animationDone = true;
                Destroy(An_Hot);
                AnimationPlayed = true;
                
            };


        }
    }
}

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

    public AudioSource PanelMoveSound;

    GameObject An_Hot;
    SkeletonAnimation skeletonAnimation;

    Spine.AnimationState AS;

    private void Start() {

        AnimationPlayed = false;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(240, 0, 0);

        PanelMoveSound = GameObject.Find("SidebarPanelSound").GetComponent<AudioSource>();
        print(PanelMoveSound);
    }

    public void MovePanelOut() {
        Vector3 goal = new Vector3(240, 0, 0);
        Vector3 point = new Vector3(-300, 0, 0);
        Physics.queriesHitTriggers = true;
        if (point != goal) {
            RectTransform var = GetComponent<RectTransform>();
            var.anchoredPosition = goal;
        }
        PanelMoveSound.Play();
        panelactive = false;
        CardPreview.enabled = true;
        CardPreviewText.enabled = true;

    }

    public void MovePanelIn() {
        if (GameObject.Find("Field").GetComponent<GameManager>().reconstructState != RecontrustState.standby || GameObject.Find("Field").GetComponent<GameManager>().deactivateSlider == true) return;
        Vector3 point = new Vector3(240, 0, 0);
        Vector3 goal = new Vector3(-300, 0, 0);
        Physics.queriesHitTriggers = true;
        if (point != goal) {
            RectTransform var = GetComponent<RectTransform>();
            var.anchoredPosition = goal;
        }

        PanelMoveSound.Play();
        panelactive = true;
        CardPreview.enabled = false;
        CardPreviewText.enabled = false;

        if (GameObject.Find("Field").GetComponent<GameManager>().lastSetCard == CardID.HotPotatoe && AnimationPlayed == false) {
            An_Hot = (GameObject)Instantiate(Resources.Load("Animations/AN_Hot"));
            
            skeletonAnimation = An_Hot.GetComponent<SkeletonAnimation>();
            AS = skeletonAnimation.state;

            AS.Complete += delegate {
                                
                Destroy(An_Hot);
                AnimationPlayed = true;
                                
            };
        }
    }
}

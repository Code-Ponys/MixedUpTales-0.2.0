using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Card : MonoBehaviour {

    public CardID cardid;
    public Team team;
    public int x;
    public int y;
    public bool visited;
    public int PointCardCounter;
    public CardAction cardAction;
    public GameObject Shine;
    public Animator An;
    public GameObject F;
    public bool cardprocessdone;
    public bool reconstructed;
    public bool animationActive;

    public void SetAnimationStart() {
        Shine = (GameObject)Instantiate(Resources.Load("Animations/AN_Shine"));
        An = Shine.GetComponent<Animator>();
        Shine.transform.position = new Vector3(x, y, -3);
        Shine.GetComponent<SpriteRenderer>().enabled = true;
    }


    protected void AnimationDone() {
        F.GetComponent<GameManager>().animationDone = true;
    }

    public bool IsSetAnimationEnd() {
        if (An.GetCurrentAnimatorStateInfo(0).IsName("end")) {
            Destroy(Shine);
            animationActive = false;
            return true;
        }
        return false;
    }

    public bool isSetAnimationInDeleteFrame() {
        if (An.GetCurrentAnimatorStateInfo(0).IsName("22")) {
            return true;
        }
        return false;
    }

    public void HighlightAnimationStart() {
        SetAnimationStart();
        animationActive = true;
    }
}
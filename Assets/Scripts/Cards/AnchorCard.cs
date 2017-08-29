using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


namespace Cards {
    public class AnchorCard : Card {
        GameObject OwnGO;
        GameObject An_Anchor;

        //AudioSource Sound;
        SkeletonAnimation skeletonAnimation;
       
        Spine.AnimationState AS;

        // Use this for initialization
        void Start() {
            GameObject F = GameObject.Find("Field");
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));

            if (reconstructed) return;

            DeactivateSlider();
            An_Anchor = (GameObject)Instantiate(Resources.Load("Animations/AN_Anchor"));

            //Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
            skeletonAnimation = An_Anchor.GetComponent<SkeletonAnimation>();
           
            AS = skeletonAnimation.state;


            An_Anchor.transform.position = new Vector3(x, (y - 0.5f), -3);
           
            //skeletonAnimation.AnimationState.SetAnimation(0, "animation", false);
            //Sound.Play();

            AS.Complete += delegate {
                OwnGO.GetComponent<SpriteRenderer>().enabled = true;
                F.GetComponent<GameManager>().animationDone = true;
                Destroy(An_Anchor);
            };

        }

        // Update is called once per frame
        void Update() {
            if (reconstructed) {
                if (animationActive)
                    IsSetAnimationEnd();
                return;
            }
        }
        
    }
}

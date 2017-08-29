using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Cards {
    public class DeleteCard : Card {
        GameObject OwnGO;


        GameObject An_Delete;

        //AudioSource Sound;
        SkeletonAnimation skeletonAnimation;
        Spine.AnimationState AS;

        // Use this for initialization
        void Start() {
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            GameObject F = GameObject.Find("Field");
            if (reconstructed) return;

            DeactivateSlider();
            GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));

            An_Delete = (GameObject)Instantiate(Resources.Load("Animations/AN_Delete"));

            //Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
            skeletonAnimation = An_Delete.GetComponent<SkeletonAnimation>();

            AS = skeletonAnimation.state;


            An_Delete.transform.position = new Vector3(x, (y - 1.5f), -3);
            //Sound.Play();

            AS.Complete += delegate {
                print("animation end");

                F.GetComponent<GameManager>().CollectRemoveCard(Card, CardAction.CardDeleted);
                F.GetComponent<GameManager>().animationDone = true;
                Destroy(An_Delete);
                DestroyImmediate(OwnGO);
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

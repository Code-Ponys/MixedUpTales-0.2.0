using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


namespace Cards {
    public class NukeCard : Card {

        GameObject An_Nuke;
        GameObject OwnGO;

        AudioSource Sound;
        SkeletonAnimation skeletonAnimation;
        
        Spine.AnimationState AS;

        // Use this for initialization
        void Start() {
            F = GameObject.Find("Field");
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            if (reconstructed) return;

            An_Nuke = (GameObject)Instantiate(Resources.Load("Animations/AN_Nuke"));


            Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
            skeletonAnimation = An_Nuke.GetComponent<SkeletonAnimation>();
            
            AS = skeletonAnimation.state;            

            An_Nuke.transform.position = (Camera.main.GetComponent<CameraManager>().GetCenter());
            
            skeletonAnimation.AnimationState.SetAnimation(0, "neuer versuch", false);
            Sound.Play();
          
            while (F.GetComponent<Field>().cardsOnField.Count != 0) {
                GameObject RemoveCard = F.GetComponent<Field>().cardsOnField[0];
                F.GetComponent<GameManager>().CollectRemoveCard(RemoveCard, CardAction.CardDeleted);
            }

            F.GetComponent<GameManager>().RenewIndicators();
            
            AS.Complete += delegate {
                print("animation end");
               
                F.GetComponent<GameManager>().animationDone = true;
                Destroy(An_Nuke);
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


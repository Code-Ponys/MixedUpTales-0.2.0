using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


namespace Cards {
    public class NukeCard : Card {

        GameObject An_Nuke;
        GameObject F;
        GameObject OwnGO;

        AudioSource Sound;
        SkeletonAnimation skeletonAnimation;
        MeshRenderer MR;

        // Use this for initialization
        void Start() {
            print("start");

            An_Nuke = (GameObject)Instantiate(Resources.Load("Animations/AN_Nuke"));

            F = GameObject.Find("Field");
            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));

            Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
            skeletonAnimation = An_Nuke.GetComponent<SkeletonAnimation>();
            MR = skeletonAnimation.GetComponent<MeshRenderer>();

            MR.enabled = true;
            print("Animation sichtbar");
            skeletonAnimation.AnimationState.SetAnimation(0, "neuer versuch", false);
            print("animation set");
            Sound.Play();
            print("sound");


            while (F.GetComponent<Field>().cardsOnField.Count != 0) {
                GameObject RemoveCard = F.GetComponent<Field>().cardsOnField[0];
                F.GetComponent<GameManager>().RemoveCard(RemoveCard);
            }
<<<<<<< HEAD
            F.GetComponent<GameManager>().GenerateFieldCard(CardID.Startpoint, 0, 0);
            F.GetComponent<GameManager>().RenewIndicators();

=======
            F.GetComponent<GameManager>().animationDone = true;
            F.GetComponent<GameManager>().RemoveCard(OwnGO);
>>>>>>> 6e587489c1408db539ef8e6280b83f25347bb9f7
        }


        // Update is called once per frame
        void Update() {

            Spine.AnimationState AS = skeletonAnimation.state;


            AS.Complete += delegate {
                print("animation end");
                MR.enabled = false;
                F.GetComponent<GameManager>().animationDone = true;
                DestroyImmediate(An_Nuke);
                F.GetComponent<GameManager>().RemoveCard(OwnGO);
            };
        }



    }

}


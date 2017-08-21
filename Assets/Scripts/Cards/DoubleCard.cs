using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Cards {
    public class DoubleCard : Card {
        GameObject OwnGO;
        GameObject An_Double;


        AudioSource Sound;
        SkeletonAnimation skeletonAnimation;
        MeshRenderer MR;
        Spine.AnimationState AS;

        private bool cardprocessdone;
        GameObject F;
        // Use this for initialization
        void Start() {

            An_Double = (GameObject)Instantiate(Resources.Load("Animations/AN_Double"));

            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");

            Sound = GameObject.Find("ErrorSound (1)").GetComponent<AudioSource>();
            skeletonAnimation = An_Double.GetComponent<SkeletonAnimation>();
            MR = skeletonAnimation.GetComponent<MeshRenderer>();
            AS = skeletonAnimation.state;


            F.GetComponent<GameManager>().cardlocked = true;

            An_Double.transform.position = new Vector3(x, y, -3);
            MR.enabled = true;
            skeletonAnimation.AnimationState.SetAnimation(0, "animation", false);
            Sound.Play();

            F.GetComponent<GameManager>().GenerateFieldCard(CardID.Blankcard, x, y);

            AS.Complete += delegate {
                MR.enabled = false;
            };

        }

        // Update is called once per frame
        void Update() {
            if (cardprocessdone) return;
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (F.GetComponent<GameManager>().cardlocked == true) {
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int indexX = F.GetComponent<Field>().RoundIt(mouseWorldPos.x);
                    int indexY = F.GetComponent<Field>().RoundIt(mouseWorldPos.y);
                    GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, indexX, indexY));
                    GameObject FieldIndicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, indexX, indexY));
                    GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, indexX, indexY));
                    if (indexX == x && indexY == y) return;
                    if (Card != null) return;

                    if (FieldIndicator.GetComponent<Indicator>().currentcolor == IndicatorColor.green) {
                        print("Pointcard created");
                        
                        An_Double.transform.position = new Vector3(indexX, indexY, -3);
                        MR.enabled = true;
                        skeletonAnimation.AnimationState.SetAnimation(0, "animation", false);
                        Sound.Play();

                        F.GetComponent<GameManager>().GetPointCardNumber(team);
                        F.GetComponent<GameManager>().GenerateFieldCard(CardID.Pointcard, indexX, indexY);

                        AS.Complete += delegate {
                            MR.enabled = false;
                            F.GetComponent<GameManager>().animationDone = true;
                            DestroyImmediate(An_Double);
                            cardprocessdone = true;
                            F.GetComponent<GameManager>().RemoveCard(OwnGO);
                        };

                    }
                }
            }
        }
    }
}

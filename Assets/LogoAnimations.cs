using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class LogoAnimations : MonoBehaviour {

    public int AnimCounter;


    GameObject CodeFoxLogo;
    GameObject LPPLogo;
    GameObject CutePowerLogo;
    GameObject JayLogo;
    GameObject YousseffLogo;

    GameObject TeamLogo;
    SkeletonAnimation skeletonAnimation;
    Spine.AnimationState AS;

    public GameObject SceneChanger;

    Animator An;

    // Use this for initialization
    void Start() {
        AnimCounter = 0;

        //TeamLogo
        TeamLogo = (GameObject)Instantiate(Resources.Load("Animations/TeamLogoAnim"));
        skeletonAnimation = TeamLogo.GetComponent<SkeletonAnimation>();
        AS = skeletonAnimation.state;

        AS.Complete += delegate {

            DestroyImmediate(TeamLogo);
            AnimCounter++;
            
        };

    }

    // Update is called once per frame
    void Update() {


        if ( AnimCounter == 1) {

            //Somni
            JayLogo = (GameObject)Instantiate(Resources.Load("Animations/SomnialaLogo/SomniAnim"));
            An = JayLogo.GetComponent<Animator>();
            AnimCounter++;

        }

        if (An.GetCurrentAnimatorStateInfo(0).IsName("end") && AnimCounter == 2) {

            //NWG
            YousseffLogo = (GameObject)Instantiate(Resources.Load("Animations/NoWingGames/NWGAnim"));
            An = YousseffLogo.GetComponent<Animator>();
            AnimCounter++;

        }

        if (An.GetCurrentAnimatorStateInfo(0).IsName("end") && AnimCounter == 3) {

            //CodeFoxLogo
            CodeFoxLogo = (GameObject)Instantiate(Resources.Load("Animations/CodeFoxLogo/CodeFoxAnim"));
            An = CodeFoxLogo.GetComponent<Animator>();
            AnimCounter++;

        }

        if (An.GetCurrentAnimatorStateInfo(0).IsName("end") && AnimCounter == 4) {

            //LPP
            LPPLogo = (GameObject)Instantiate(Resources.Load("Animations/LPPLogo/LPPAnim"));
            An = LPPLogo.GetComponent<Animator>();
            AnimCounter++;

        }

        if (An.GetCurrentAnimatorStateInfo(0).IsName("end") && AnimCounter == 5) {

            //CPLogo
            CutePowerLogo = (GameObject)Instantiate(Resources.Load("Animations/CutePowerLogo/CPAnim"));
            An = CutePowerLogo.GetComponent<Animator>();
            AnimCounter++;           
            
        }

        if (An.GetCurrentAnimatorStateInfo(0).IsName("end") && AnimCounter == 6) {

            AnimCounter++;
        }

        if (AnimCounter == 7) {
            SceneManager.LoadScene("UI_MainMenu");
        }


    }


}

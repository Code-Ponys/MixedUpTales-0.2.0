using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAnimations : MonoBehaviour {

    public int AnimCounter;


    GameObject CodeFoxLogo;
    GameObject LPPLogo;
    GameObject CutePowerLogo;
    GameObject JayLogo;
    GameObject YousseffLogo;

    public GameObject SceneChanger;

    Animator An;

    // Use this for initialization
    void Start() {
        AnimCounter = 0;

        //CodeFoxLogo
        CodeFoxLogo = (GameObject)Instantiate(Resources.Load("Animations/CodeFoxLogo/CodeFoxAnim"));
        An = CodeFoxLogo.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {

      



        if (An.GetCurrentAnimatorStateInfo(0).IsName("end")) {

            //LPP
            LPPLogo = (GameObject)Instantiate(Resources.Load("Animations/LPPLogo/LPPAnim"));
            An = LPPLogo.GetComponent<Animator>();
            AnimCounter++;

        }

        if (An.GetCurrentAnimatorStateInfo(0).IsName("end")) {

            AnimCounter++;
        }

        if (AnimCounter == 2) {

            SceneManager.LoadScene("UI_MainMenu");
        }

    }

   
}

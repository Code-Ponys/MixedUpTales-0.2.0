using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cards {
    public class VortexCard : Card {

        GameObject OwnGO;
        List<CardID> newDeckPlayer1 = new List<CardID>();
        List<CardID> newDeckPlayer2 = new List<CardID>();

        // Use this for initialization
        void Start() {

            OwnGO = GameObject.Find(Slave.GetCardName(cardid, x, y));
            F = GameObject.Find("Field");
            F.GetComponent<GameManager>().currentChoosedCardGO.GetComponent<Handcards>().cardid = CardID.none;

            GameObject Player1 = GameObject.Find("PlayerBlue");
            GameObject Player2 = GameObject.Find("PlayerRed");
            while (Player1.GetComponent<Player>().Deck.Count != 0) {
                newDeckPlayer2.Add(Player1.GetComponent<Player>().Deck[0]);
                Player1.GetComponent<Player>().Deck.RemoveAt(0);
            }
            while (Player2.GetComponent<Player>().Deck.Count != 0) {
                newDeckPlayer1.Add(Player2.GetComponent<Player>().Deck[0]);
                Player2.GetComponent<Player>().Deck.RemoveAt(0);
            }
            while (newDeckPlayer1.Count != 0) {
                Player1.GetComponent<Player>().Deck.Add(newDeckPlayer1[0]);
                newDeckPlayer1.RemoveAt(0);
            }
            while (newDeckPlayer2.Count != 0) {
                Player2.GetComponent<Player>().Deck.Add(newDeckPlayer2[0]);
                newDeckPlayer2.RemoveAt(0);
            }

            CardID Handcard1Blue = GameObject.Find("HandCard1" + Team.blue).GetComponent<Handcards>().cardid;
            CardID Handcard2Blue = GameObject.Find("HandCard2" + Team.blue).GetComponent<Handcards>().cardid;
            CardID Handcard3Blue = GameObject.Find("HandCard3" + Team.blue).GetComponent<Handcards>().cardid;

            CardID Handcard1Red = GameObject.Find("HandCard1" + Team.red).GetComponent<Handcards>().cardid;
            CardID Handcard2Red = GameObject.Find("HandCard2" + Team.red).GetComponent<Handcards>().cardid;
            CardID Handcard3Red = GameObject.Find("HandCard3" + Team.red).GetComponent<Handcards>().cardid;

            Team team = Team.red;
            GameObject Handcard1 = GameObject.Find("HandCard1" + team);
            Image image1 = Handcard1.GetComponent<Image>();
            image1.sprite = Resources.Load<Sprite>(Slave.GetImagePath(Handcard1Blue, team));
            Handcard1.GetComponent<Handcards>().cardid = Handcard1Blue;
            Player2.GetComponent<Handcards>().GetCardTexts(Handcard1Blue);
            GameObject Handcard2 = GameObject.Find("HandCard2" + team);
            Image image2 = Handcard2.GetComponent<Image>();
            image2.sprite = Resources.Load<Sprite>(Slave.GetImagePath(Handcard2Blue, team));
            Handcard2.GetComponent<Handcards>().cardid = Handcard2Blue;
            Player2.GetComponent<Handcards>().GetCardTexts(Handcard2Blue);
            GameObject Handcard3 = GameObject.Find("HandCard3" + team);
            Image image3 = Handcard3.GetComponent<Image>();
            image3.sprite = Resources.Load<Sprite>(Slave.GetImagePath(Handcard3Blue, team));
            Handcard3.GetComponent<Handcards>().cardid = Handcard3Blue;
            Player2.GetComponent<Handcards>().GetCardTexts(Handcard3Blue);

            team = Team.blue;
            GameObject Handcard4 = GameObject.Find("HandCard1" + team);
            Image image4 = Handcard4.GetComponent<Image>();
            image4.sprite = Resources.Load<Sprite>(Slave.GetImagePath(Handcard1Red, team));
            Handcard4.GetComponent<Handcards>().cardid = Handcard1Red;
            Player2.GetComponent<Handcards>().GetCardTexts(Handcard1Red);
            GameObject Handcard5 = GameObject.Find("HandCard2" + team);
            Image image5 = Handcard5.GetComponent<Image>();
            image5.sprite = Resources.Load<Sprite>(Slave.GetImagePath(Handcard2Red, team));
            Handcard5.GetComponent<Handcards>().cardid = Handcard2Red;
            Player2.GetComponent<Handcards>().GetCardTexts(Handcard2Red);
            GameObject Handcard6 = GameObject.Find("HandCard3" + team);
            Image image6 = Handcard6.GetComponent<Image>();
            image6.sprite = Resources.Load<Sprite>(Slave.GetImagePath(Handcard3Red, team));
            Handcard6.GetComponent<Handcards>().cardid = Handcard3Red;
            Player2.GetComponent<Handcards>().GetCardTexts(Handcard3Red);

            GameObject.Find("Field").GetComponent<GameManager>().animationDone = true;

            DestroyImmediate(OwnGO);
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


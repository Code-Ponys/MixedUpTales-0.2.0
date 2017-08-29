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
            if (reconstructed) return;
            DeactivateSlider();
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
            GameObject.Find("CardInfoText").GetComponent<Text>().text = "Your cards have been changed.";

            CardID cardID1blue = GameObject.Find("HandCard1" + Team.blue).GetComponent<Handcards>().cardid;
            CardID cardID2blue = GameObject.Find("HandCard2" + Team.blue).GetComponent<Handcards>().cardid;
            CardID cardID3blue = GameObject.Find("HandCard3" + Team.blue).GetComponent<Handcards>().cardid;

            CardID cardID1red = GameObject.Find("HandCard1" + Team.red).GetComponent<Handcards>().cardid;
            CardID cardID2red = GameObject.Find("HandCard2" + Team.red).GetComponent<Handcards>().cardid;
            CardID cardID3red = GameObject.Find("HandCard3" + Team.red).GetComponent<Handcards>().cardid;

            int PointCardCounter1blue = GameObject.Find("HandCard1" + Team.blue).GetComponent<Handcards>().PointCardCounter;
            int PointCardCounter2blue = GameObject.Find("HandCard2" + Team.blue).GetComponent<Handcards>().PointCardCounter;
            int PointCardCounter3blue = GameObject.Find("HandCard3" + Team.blue).GetComponent<Handcards>().PointCardCounter;

            int PointCardCounter1red = GameObject.Find("HandCard1" + Team.red).GetComponent<Handcards>().PointCardCounter;
            int PointCardCounter2red = GameObject.Find("HandCard2" + Team.red).GetComponent<Handcards>().PointCardCounter;
            int PointCardCounter3red = GameObject.Find("HandCard3" + Team.red).GetComponent<Handcards>().PointCardCounter;

            Team team = Team.red;
            GameObject Handcard1 = GameObject.Find("HandCard1" + team);
            Handcard1.GetComponent<Handcards>().PointCardCounter = PointCardCounter1blue;
            Handcard1.GetComponent<Handcards>().cardid = cardID1blue;
            GameObject Handcard2 = GameObject.Find("HandCard2" + team);
            Handcard2.GetComponent<Handcards>().PointCardCounter = PointCardCounter2blue;
            Handcard2.GetComponent<Handcards>().cardid = cardID2blue;
            GameObject Handcard3 = GameObject.Find("HandCard3" + team);
            Handcard3.GetComponent<Handcards>().PointCardCounter = PointCardCounter3blue;
            Handcard3.GetComponent<Handcards>().cardid = cardID3blue;

            team = Team.blue;
            GameObject Handcard4 = GameObject.Find("HandCard1" + team);
            Handcard4.GetComponent<Handcards>().PointCardCounter = PointCardCounter1red;
            Handcard4.GetComponent<Handcards>().cardid = cardID1red;
            GameObject Handcard5 = GameObject.Find("HandCard2" + team);
            Handcard5.GetComponent<Handcards>().PointCardCounter = PointCardCounter2red;
            Handcard5.GetComponent<Handcards>().cardid = cardID2red;
            GameObject Handcard6 = GameObject.Find("HandCard3" + team);
            Handcard6.GetComponent<Handcards>().PointCardCounter = PointCardCounter3red;
            Handcard6.GetComponent<Handcards>().cardid = cardID3red;

            GameObject.Find("Field").GetComponent<GameManager>().animationDone = true;

            int red = F.GetComponent<GameManager>().PointCardCounterBlue;
            int blue = F.GetComponent<GameManager>().PointCardCounterRed;
            F.GetComponent<GameManager>().PointCardCounterRed = red;
            F.GetComponent<GameManager>().PointCardCounterBlue = blue;


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


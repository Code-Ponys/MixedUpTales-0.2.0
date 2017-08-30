using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;

public class Player : MonoBehaviour {

    public Team team;
    public List<CardID> Deck;
    bool refillCardsIsDone = false;
    public GameObject F;

    private void Start() {
        F = GameObject.Find("Field");
    }

    // Use this for initialization
    public void Init(Team team) {
        this.team = team;
        Deck = GenerateDeck();
        //Deckerstellung
        RefillHand();


    }

    // Update is called once per frame
    void Update() {

    }

    private List<CardID> GenerateDeck() {
        List<CardID> finalDeck = new List<CardID>();
        List<CardID> pointBlankCards = new List<CardID>();
        List<CardID> specialCards = new List<CardID>();
        int totalcards = 40;
        int pointcards = 15;
        int blankcards = 15;
        int doublecard = 3;
        int blockcard = 3;
        int deletecard = 3;
        int burncard = 3;
        int infernocard = 2;
        int changecard = 3;
        int cancercard = 1;
        int hotpotatoe = 4;
        int nukecard = 1;
        int vortexcard = 2;
        int anchorcard = 1;
        int shufflecard = 1;
        int specialcards = 10;
        int random = 0;

        while (pointBlankCards.Count != 30) {
            random = Random.Range(0, 2);
            if (random == 1 && blankcards != 0) {
                blankcards--;
                pointBlankCards.Add(CardID.Blankcard);
            }
            if (random == 0 && pointcards != 0) {
                pointBlankCards.Add(CardID.Pointcard);
                pointcards--;
            }
        }

        while (specialCards.Count != 10) {
            random = Random.Range(0, 28);
            if (random >= 0 && random <= 3 && doublecard != 0) {
                specialCards.Add(CardID.Doublecard);
                doublecard--;
            } else if (random >= 4 && random <= 6 && blockcard != 0) {
                specialCards.Add(CardID.Blockcard);
                blockcard--;
            } else if (random >= 7 && random <= 9 && deletecard != 0) {
                specialCards.Add(CardID.Deletecard);
                deletecard--;
            } else if (random >= 10 && random <= 12 && burncard != 0) {
                specialCards.Add(CardID.Burncard);
                burncard--;
            } else if (random >= 13 && random <= 14 && infernocard != 0) {
                specialCards.Add(CardID.Infernocard);
                infernocard--;
            } else if (random >= 15 && random <= 17 && changecard != 0) {
                specialCards.Add(CardID.Changecard);
                changecard--;
            } else if (random == 18 && cancercard != 0) {
                specialCards.Add(CardID.Cancercard);
                cancercard--;
            } else if (random >= 19 && random <= 22 && hotpotatoe != 0) {
                specialCards.Add(CardID.HotPotatoe);
                hotpotatoe--;
            } else if (random == 23 && specialcards != 0 && nukecard != 0) {
                specialCards.Add(CardID.Nukecard);
                nukecard--;
            } else if (random >= 24 && random <= 25 && vortexcard != 0) {
                specialCards.Add(CardID.Vortexcard);
                vortexcard--;
            } else if (random == 26 && anchorcard != 0) {
                specialCards.Add(CardID.Anchorcard);
                anchorcard--;
            } else if (random == 27 && shufflecard != 0) {
                specialCards.Add(CardID.Shufflecard);
                shufflecard--;
            }
        }

        foreach (CardID Card in specialCards) {
            random = Random.Range(0, pointBlankCards.Count);
            pointBlankCards.Insert(random, Card);
        }

        
        Debug.Log(team + " GenerateDeck");
        return pointBlankCards;
    }

    public void RefillHand() {
        print("RefillHand");
        refillCardsIsDone = false;
        if (Deck.Count == 0) return;
        while (refillCardsIsDone == false) {
            CardID card = Deck[0];
            if (GameObject.Find("HandCard1" + team).GetComponent<Handcards>().cardid == CardID.none) {
                GameObject Handcard = GameObject.Find("HandCard1" + team);
                if (card != CardID.Pointcard && card != CardID.Doublecard) {
                    Handcard.GetComponent<Handcards>().cardid = card;
                } else {
                    F.GetComponent<GameManager>().GetPointCardNumber(team);
                    if (team == Team.red) {
                        Handcard.GetComponent<Handcards>().PointCardCounter = F.GetComponent<GameManager>().PointCardCounterRed;
                    } else {
                        Handcard.GetComponent<Handcards>().PointCardCounter = F.GetComponent<GameManager>().PointCardCounterBlue;
                    }
                    if (card == CardID.Doublecard) {
                        Handcard.GetComponent<Handcards>().cardid = card;
                    } else {
                        Handcard.GetComponent<Handcards>().cardid = card;
                    }
                }
                Handcard.GetComponent<Handcards>().cardid = card;
                Deck.RemoveAt(0);
                continue;
            } else if (GameObject.Find("HandCard2" + team).GetComponent<Handcards>().cardid == CardID.none) {
                GameObject Handcard = GameObject.Find("HandCard2" + team);
                if (card != CardID.Pointcard && card != CardID.Doublecard) {
                    Handcard.GetComponent<Handcards>().cardid = card;
                } else {
                    int counter = F.GetComponent<GameManager>().GetPointCardNumber(team);
                    if (team == Team.red) {
                        Handcard.GetComponent<Handcards>().PointCardCounter = F.GetComponent<GameManager>().PointCardCounterRed;
                    } else {
                        Handcard.GetComponent<Handcards>().PointCardCounter = F.GetComponent<GameManager>().PointCardCounterBlue;
                    }
                    if (card == CardID.Doublecard) {
                        Handcard.GetComponent<Handcards>().cardid = card;
                    } else {
                        Handcard.GetComponent<Handcards>().cardid = card;
                    }
                }
                Handcard.GetComponent<Handcards>().cardid = card;
                Deck.RemoveAt(0);
                continue;
            } else if (GameObject.Find("HandCard3" + team).GetComponent<Handcards>().cardid == CardID.none) {
                GameObject Handcard = GameObject.Find("HandCard3" + team);
                if (card != CardID.Pointcard && card != CardID.Doublecard) {
                    Handcard.GetComponent<Handcards>().cardid = card;
                } else {
                    int counter = F.GetComponent<GameManager>().GetPointCardNumber(team);
                    if (team == Team.red) {
                        Handcard.GetComponent<Handcards>().PointCardCounter = F.GetComponent<GameManager>().PointCardCounterRed;
                    } else {
                        Handcard.GetComponent<Handcards>().PointCardCounter = F.GetComponent<GameManager>().PointCardCounterBlue;
                    }
                    if (card == CardID.Doublecard) {
                        Handcard.GetComponent<Handcards>().cardid = card;
                    } else {
                        Handcard.GetComponent<Handcards>().cardid = card;
                    }
                }
                Handcard.GetComponent<Handcards>().cardid = card;
                Deck.RemoveAt(0);
                continue;
            }
            refillCardsIsDone = true;
            return;
        }
        return;
    }
}
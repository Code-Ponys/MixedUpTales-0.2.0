using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handcards : MonoBehaviour {

    CardID currentcardid = CardID.none;
    public CardID cardid = CardID.none;
    public int PointCardCounter;
    public GameObject Handcard;
    public Team team;
    Image image;

    private void Start() {
        image = Handcard.GetComponent<Image>();
    }


    private void Update() {
        if (cardid != currentcardid) {
            currentcardid = cardid;

            if (cardid != CardID.Pointcard && cardid != CardID.Doublecard) {
                image.sprite = Resources.Load<Sprite>(Slave.GetImagePath(cardid, team));
            } else {
                if (cardid == CardID.Doublecard) {
                    image.sprite = Resources.Load<Sprite>(Slave.GetImagePath(cardid, team));
                } else {
                    image.sprite = Resources.Load<Sprite>(Slave.GetImagePath(team, PointCardCounter));
                }
            }
            GetCardTexts(currentcardid);
        }
    }

    public void GetCardTexts(CardID card) {
        if (GameObject.Find("HandCard1" + team).GetComponent<Handcards>().cardid == card) {
            GameObject.Find("Kartenname1" + team).GetComponent<Text>().text = Slave.GetCardName(card);
            GameObject.Find("Kartentext1" + team).GetComponent<Text>().text = Slave.GetCardDescription(card);
        }
        if (GameObject.Find("HandCard2" + team).GetComponent<Handcards>().cardid == card) {
            GameObject.Find("Kartenname2" + team).GetComponent<Text>().text = Slave.GetCardName(card);
            GameObject.Find("Kartentext2" + team).GetComponent<Text>().text = Slave.GetCardDescription(card);
        }
        if (GameObject.Find("HandCard3" + team).GetComponent<Handcards>().cardid == card) {
            GameObject.Find("Kartenname3" + team).GetComponent<Text>().text = Slave.GetCardName(card);
            GameObject.Find("Kartentext3" + team).GetComponent<Text>().text = Slave.GetCardDescription(card);
        }
    }
}

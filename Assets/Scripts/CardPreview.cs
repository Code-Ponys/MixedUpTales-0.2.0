using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour {

    CardID currentcardid = CardID.none;
    public CardID cardid = CardID.none;
    public int PointCardCounter;
    int Pointcardcounterold;
    public GameObject Handcard;
    public Team team;
    Image image;

    private void Start() {
        image = Handcard.GetComponent<Image>();
    }


    private void Update() {
        if (cardid != currentcardid || PointCardCounter != Pointcardcounterold) {
            currentcardid = cardid;
            Pointcardcounterold = PointCardCounter;

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
        GameObject.Find("currentChoosedCardName").GetComponent<Text>().text = Slave.GetCardName(card);
    }

}

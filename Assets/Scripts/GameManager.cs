using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cards;
using UnityEngine.EventSystems;
using System;

public class GameManager : MonoBehaviour {
    public List<Card> CardsAffectedLastRound = new List<Card>();
    public Field Field;
    public MousePos MP;
    public FieldProperties FP;
    

    public Player[] players;
    public Player PlayerBlue { get { return players[0]; } }
    public Player PlayerRed { get { return players[1]; } }
    public int[,] distance;
    public GameObject PlayerName;

    public GameObject currentChoosedCardGO;
    GameObject CardPreview;
    Image CardPreviewImage;

    public Image SideBarBlue;
    public Image SideBarRed;
    public Canvas ChangePlayer;
    public Canvas WinScreen;
    string currentChoosedCardName;

    public Team currentPlayer;
    public CardID currentChoosedCard;
    public CardID lastSetCard;
    public bool anchorFieldVisible;
    public bool deleteIndicatorVisible;
    public bool animationDone;
    public bool cardlocked;
    private bool changeIndicatorVisible;
    float triggerDelayedNewRound;
    private bool shuffleIndicatorVisible;
    public int PointCardCounterRed;
    public int PointCardCounterBlue;


    // Use this for initialization
    void Start() {
        players = new Player[2];
        players[0] = GameObject.Find("PlayerBlue").GetComponent<Player>();
        players[1] = GameObject.Find("PlayerRed").GetComponent<Player>();
        players[0].Init(Team.blue);
        players[1].Init(Team.red);
        currentPlayer = Team.blue;

        PlayerName = GameObject.Find("TextSpieler");
        PlayerName.GetComponent<Text>().text = "Player 1";
        PlayerBlue.RefillHand();

        //Image SideBarBlue = GameObject.Find("SideMenu Blue").GetComponent<Image>();
        //Image SideBarRed = GameObject.Find("SideMenu Red").GetComponent<Image>();
        SideBarRed.enabled = false;
        ChangePlayer.enabled = false;
        WinScreen.enabled = false;
        animationDone = false;
        CardPreview = GameObject.Find("currentChoosedCard");
    }

    // Update is called once per frame
    void Update() {
        ToggleDeleteCardFieldVisibility();

        if (animationDone == true) {
            RemovePlacedCardFromHand();
            TogglePlayerScreen();
            animationDone = false;
        }
        if (triggerDelayedNewRound != 0) {
            triggerDelayedNewRound = -Time.deltaTime;
        }
        if (triggerDelayedNewRound != 0 && triggerDelayedNewRound < 0.5f) {
            triggerDelayedNewRound = 0;
            if (lastSetCard == CardID.Deletecard
            || lastSetCard == CardID.Burncard
            || lastSetCard == CardID.Cancercard
            || lastSetCard == CardID.Infernocard) {
                RemoveUnconnectedCards();
                lastSetCard = CardID.none;
            }
            cardlocked = false;
            TogglePlayerScreen();
        }
    }



    public void ChangeToScene(string SceneToChangeTo) {
        SceneManager.LoadScene(SceneToChangeTo);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public virtual GameObject GenerateFieldCard(CardID cardid, int x, int y, Team team) {
        if (cardid == CardID.ChoosedCard) {
            cardid = currentChoosedCard;
        }
        if (currentChoosedCard == CardID.placed || cardid == CardID.none) {
            return null;
        }
        string pf_path = Slave.GetImagePathPf(cardid, currentPlayer);
        string cardname = Slave.GetCardName(cardid, x, y);

        print("Create: " + cardname);

        GameObject Card = (GameObject)Instantiate(Resources.Load(pf_path));
        if (cardid == CardID.FieldIndicator) {
            GameObject FieldIndicatorParent = GameObject.Find("FieldIndicator");
            Card.transform.parent = FieldIndicatorParent.transform;
            Card.transform.position = new Vector3(x, y, -1);
        } else if (cardid == CardID.FieldIndicatorRed) {
            GameObject FieldIndicatorParentRed = GameObject.Find("FieldIndicator");
            Card.transform.parent = FieldIndicatorParentRed.transform;
            Card.transform.position = new Vector3(x, y, -1);
        } else if (cardid == CardID.CardIndicator) {
            GameObject CardIndicatorParentRed = GameObject.Find("CardIndicator");
            Card.transform.parent = CardIndicatorParentRed.transform;
            Card.transform.position = new Vector3(x, y, -3);
        } else {
            GameObject FieldParent = GameObject.Find("Field");
            Card.transform.parent = FieldParent.transform;
            Card.transform.position = new Vector3(x, y, -2);
        }
        Card.transform.localScale = new Vector3(0.320f, 0.320f, 0);
        Card.name = cardname;

        if (cardid == CardID.Startpoint || cardid == CardID.Anchorcard
            || cardid == CardID.Pointcard || cardid == CardID.Blockcard
            || cardid == CardID.Blankcard) {

            SetFieldIndicator(x, y);
            //Card.AddComponent<Card>();

            Camera.main.GetComponent<CameraManager>().CenterCamera(x, y);
        }

        switch (cardid) {
            default:
                Card.AddComponent<NotImplemented>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Blankcard:
                Card.AddComponent<BlankCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Pointcard:
                Card.AddComponent<PointCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Startpoint:
                Card.AddComponent<Startpoint>();
                Card.GetComponent<Card>().team = Team.system;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Blockcard:
                Card.AddComponent<BlockCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.FieldIndicator:
                Card.AddComponent<Indicator>();
                Card.GetComponent<Indicator>().setData(x, y, Team.system, IndicatorType.field, IndicatorColor.transparent);
                break;
            case CardID.FieldIndicatorRed:
                Card.AddComponent<Indicator>();
                Card.GetComponent<Indicator>().setData(x, y, Team.system, IndicatorType.field, IndicatorColor.red);
                Card.GetComponent<Indicator>().currentcolor = IndicatorColor.red;
                break;
            case CardID.Doublecard:
                Card.AddComponent<DoubleCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Deletecard:
                Card.AddComponent<DeleteCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Burncard:
                Card.AddComponent<BurnCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Infernocard:
                Card.AddComponent<InfernoCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Changecard:
                Card.AddComponent<ChangeCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Cancercard:
                Card.AddComponent<CancerCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.HotPotatoe:
                Card.AddComponent<HotPotatoe>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Nukecard:
                Card.AddComponent<NukeCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Vortexcard:
                Card.AddComponent<VortexCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Anchorcard:
                Card.AddComponent<AnchorCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Shufflecard:
                Card.AddComponent<ShuffleCard>();
                Card.GetComponent<Card>().team = team;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.CardIndicator:
                Card.AddComponent<Indicator>();
                Card.GetComponent<Indicator>().setData(x, y, Team.system, IndicatorType.card, IndicatorColor.transparent);
                break;
        }

        if (cardid == CardID.Startpoint || cardid == CardID.Anchorcard
            || cardid == CardID.Pointcard || cardid == CardID.Blockcard
            || cardid == CardID.Blankcard) {
            GameObject.Find("Field").GetComponent<Field>().cardsOnField.Add(Card);
        }

        if (cardid != CardID.Startpoint) {
            lastSetCard = cardid;
        }

        return Card;
    }

    public virtual GameObject GenerateFieldCard(CardID cardid, int x, int y) {
        if (cardid == CardID.ChoosedCard) {
            cardid = currentChoosedCard;
        }
        if (currentChoosedCard == CardID.placed || cardid == CardID.none) {
            return null;
        }
        string pf_path = Slave.GetImagePathPf(cardid, currentPlayer);
        string cardname = Slave.GetCardName(cardid, x, y);
        if (cardid != CardID.FieldIndicator && cardid != CardID.CardIndicator) {
            print("Create: " + cardid + " at " + x + "," + y);
        }

        GameObject Card = (GameObject)Instantiate(Resources.Load(pf_path));
        if (cardid == CardID.FieldIndicator) {
            GameObject FieldIndicatorParent = GameObject.Find("FieldIndicator");
            Card.transform.parent = FieldIndicatorParent.transform;
            Card.transform.position = new Vector3(x, y, -1);
        } else if (cardid == CardID.FieldIndicatorRed) {
            GameObject FieldIndicatorParentRed = GameObject.Find("FieldIndicator");
            Card.transform.parent = FieldIndicatorParentRed.transform;
            Card.transform.position = new Vector3(x, y, -1);
        } else if (cardid == CardID.CardIndicator) {
            GameObject CardIndicatorParentRed = GameObject.Find("CardIndicator");
            Card.transform.parent = CardIndicatorParentRed.transform;
            Card.transform.position = new Vector3(x, y, -3);
        } else {
            GameObject FieldParent = GameObject.Find("Field");
            Card.transform.parent = FieldParent.transform;
            Card.transform.position = new Vector3(x, y, -2);
        }
        Card.transform.localScale = new Vector3(0.320f, 0.320f, 0);
        Card.name = cardname;

        if (cardid == CardID.Startpoint || cardid == CardID.Anchorcard
            || cardid == CardID.Pointcard || cardid == CardID.Blockcard
            || cardid == CardID.Blankcard) {

            SetFieldIndicator(x, y);

            Camera.main.GetComponent<CameraManager>().CenterCamera(x, y);
        }

        switch (cardid) {
            default:
                Card.AddComponent<NotImplemented>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Blankcard:
                Card.AddComponent<BlankCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Pointcard:
                Card.AddComponent<PointCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Startpoint:
                Card.AddComponent<Startpoint>();
                Card.GetComponent<Card>().team = Team.system;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Blockcard:
                Card.AddComponent<BlockCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.FieldIndicator:
                Card.AddComponent<Indicator>();
                Card.GetComponent<Indicator>().setData(x, y, Team.system, IndicatorType.field, IndicatorColor.transparent);
                break;
            case CardID.FieldIndicatorRed:
                Card.AddComponent<Indicator>();
                Card.GetComponent<Indicator>().setData(x, y, Team.system, IndicatorType.field, IndicatorColor.red);
                Card.GetComponent<Indicator>().currentcolor = IndicatorColor.red;
                break;
            case CardID.Doublecard:
                Card.AddComponent<DoubleCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Deletecard:
                Card.AddComponent<DeleteCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Burncard:
                Card.AddComponent<BurnCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Infernocard:
                Card.AddComponent<InfernoCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Changecard:
                Card.AddComponent<ChangeCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Cancercard:
                Card.AddComponent<CancerCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.HotPotatoe:
                Card.AddComponent<HotPotatoe>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Nukecard:
                Card.AddComponent<NukeCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Vortexcard:
                Card.AddComponent<VortexCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Anchorcard:
                Card.AddComponent<AnchorCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.Shufflecard:
                Card.AddComponent<ShuffleCard>();
                Card.GetComponent<Card>().team = currentPlayer;
                Card.GetComponent<Card>().x = x;
                Card.GetComponent<Card>().y = y;
                Card.GetComponent<Card>().cardid = cardid;
                break;
            case CardID.CardIndicator:
                Card.AddComponent<Indicator>();
                Card.GetComponent<Indicator>().setData(x, y, Team.system, IndicatorType.card, IndicatorColor.transparent);
                break;
        }

        if (cardid == CardID.Anchorcard
            || cardid == CardID.Pointcard || cardid == CardID.Blockcard
            || cardid == CardID.Blankcard) {
            GameObject.Find("Field").GetComponent<Field>().cardsOnField.Add(Card);
        }

        if (cardid != CardID.Startpoint) {
            lastSetCard = cardid;
        }

        return Card;
    }

    public bool IsFieldOccupied(int x, int y) {
        if (x == 0 && y == 0) {
            return true;
        }

        if (GameObject.Find("SideMenu Blue").GetComponent<SideBarMove>().panelactive || GameObject.Find("SideMenu Red").GetComponent<SideBarMove>().panelactive) {
            return true;
        }
        if (currentChoosedCard == CardID.Changecard
            & GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y)).GetComponent<Indicator>().indicatorColor != IndicatorColor.yellowcovered) {
            return true;
        }
        if (GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y)).GetComponent<Indicator>().indicatorColor == IndicatorColor.yellowcovered
            && currentChoosedCard == CardID.Changecard || currentChoosedCard == CardID.Deletecard
            || currentChoosedCard == CardID.Changecard || currentChoosedCard == CardID.Shufflecard) {
            return false;
        }
        if (GameObject.Find(Slave.GetCardName(CardID.Card, x, y)) != null) {
            return true;
        }
        if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)) == null) {
            return true;
        }
        if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)).GetComponent<Indicator>().indicatorState == IndicatorState.anchorfield
            && currentChoosedCard == CardID.Anchorcard) {
            return false;
        }
        if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)).GetComponent<Indicator>().indicatorState == IndicatorState.unreachable
            || GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)).GetComponent<Indicator>().indicatorState == IndicatorState.anchorfield) {
            return true;
        }
        if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)).GetComponent<Indicator>().indicatorState == IndicatorState.blocked
            && GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)).GetComponent<Indicator>().currentcolor == IndicatorColor.red) {
            return true;
        }
        return false;
    }

    public void NewRound() {
        if (currentPlayer == Team.blue) {
            currentPlayer = Team.red;
        } else {
            currentPlayer = Team.blue;
        }
        print("New Round. Turn of Player " + currentPlayer);
        currentChoosedCard = CardID.none;
        animationDone = false;

        //TODO Change Handcards
        //TODO Fade in Black Scene
        if (currentPlayer == Team.blue) {
            SideBarBlue.enabled = true;
            SideBarRed.enabled = false;
            players[0].RefillHand();
            PlayerName.GetComponent<Text>().text = "Player 1";
        } else {
            SideBarBlue.enabled = false;
            SideBarRed.enabled = true;
            players[1].RefillHand();
            PlayerName.GetComponent<Text>().text = "Player 2";
        }
        currentChoosedCardGO = null;
        CardPreview.GetComponent<CardPreview>().cardid = CardID.none;

        triggerDelayedNewRound = 1;



    }

    public void OnCardClick() {
        if (!cardlocked) {
            string name = EventSystem.current.currentSelectedGameObject.name;
            currentChoosedCardName = name;
            currentChoosedCardGO = GameObject.Find(name);
            currentChoosedCard = currentChoosedCardGO.GetComponent<Handcards>().cardid;
            CardPreview.GetComponent<CardPreview>().PointCardCounter = currentChoosedCardGO.GetComponent<Handcards>().PointCardCounter;
            CardPreview.GetComponent<CardPreview>().team = currentChoosedCardGO.GetComponent<Handcards>().team;
            CardPreview.GetComponent<CardPreview>().cardid = currentChoosedCardGO.GetComponent<Handcards>().cardid;
        }
    }

    public void TogglePlayerScreen() {
        ChangePlayer.enabled = !ChangePlayer.enabled;
    }

    void RemovePlacedCardFromHand() {

        GameObject.Find(currentChoosedCardName).GetComponent<Handcards>().cardid = CardID.none;
    }

    //void ToggleAnchorFieldVisibility() {
    //    if (currentChoosedCard == CardID.Anchorcard && anchorFieldVisible == false) {
    //        anchorFieldVisible = true;
    //        for (int x = Camera.main.GetComponent<CameraManager>().min_x - 2; x <= Camera.main.GetComponent<CameraManager>().max_x + 2; x++) {
    //            for (int y = Camera.main.GetComponent<CameraManager>().min_y - 2; y <= Camera.main.GetComponent<CameraManager>().max_y + 2; y++) {
    //                if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)) != null) {
    //                    GameObject Indicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y));
    //                    if (Indicator.GetComponent<Indicator>().indicatorState == IndicatorState.anchorfield) {
    //                        SpriteRenderer rend = Indicator.GetComponent<SpriteRenderer>();
    //                        rend.sprite = Resources.Load<Sprite>(Slave.GetImagePath(CardID.FieldIndicatorYellow, Team.system));
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    if (currentChoosedCard != CardID.Anchorcard && anchorFieldVisible == true) {
    //        anchorFieldVisible = false;
    //        print("Deactivate Anchorcards");
    //        for (int x = Camera.main.GetComponent<CameraManager>().min_x - 2; x <= Camera.main.GetComponent<CameraManager>().max_x + 2; x++) {
    //            for (int y = Camera.main.GetComponent<CameraManager>().min_y - 2; y <= Camera.main.GetComponent<CameraManager>().max_y + 2; y++) {
    //                if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)) != null) {
    //                    GameObject Indicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y));
    //                    if (Indicator.GetComponent<Indicator>().indicatorState == IndicatorState.anchorfield) {
    //                        SpriteRenderer rend = Indicator.GetComponent<SpriteRenderer>();
    //                        rend.sprite = Resources.Load<Sprite>(Slave.GetImagePath(CardID.FieldIndicatorBlack, Team.system));
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}



    public void RenewIndicators() {
        for (int x = Camera.main.GetComponent<CameraManager>().min_x - 3; x <= Camera.main.GetComponent<CameraManager>().max_x + 3; x++) {
            for (int y = Camera.main.GetComponent<CameraManager>().min_y - 3; y <= Camera.main.GetComponent<CameraManager>().max_y + 3; y++) {
                if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y)) != null) {
                    GameObject Indicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y));
                    if (Indicator.GetComponent<Indicator>().indicatorState != IndicatorState.blocked) {
                        Indicator.GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                    }
                }
            }
        }

        for (int x = Camera.main.GetComponent<CameraManager>().min_x; x <= Camera.main.GetComponent<CameraManager>().max_x; x++) {
            for (int y = Camera.main.GetComponent<CameraManager>().min_y; y <= Camera.main.GetComponent<CameraManager>().max_y; y++) {
                if (GameObject.Find(Slave.GetCardName(CardID.Card, x, y)) != null) {
                    SetFieldIndicator(x, y);
                }
            }
        }
    }

    public void SetFieldIndicator(int x, int y) {
        GameObject IndicatorLeft = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x - 1, y));
        if (IndicatorLeft != null && IndicatorLeft.GetComponent<Indicator>().indicatorState != IndicatorState.blocked) {
            IndicatorLeft.GetComponent<Indicator>().indicatorState = IndicatorState.reachable;
        } else {
            if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicatorRed, x - 1, y)) == null && GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x - 1, y)) == null) {
                GenerateFieldCard(CardID.FieldIndicatorRed, x - 1, y);
            }
        }

        GameObject IndicatorRight = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x + 1, y));
        if (IndicatorRight != null && IndicatorRight.GetComponent<Indicator>().indicatorState != IndicatorState.blocked) {
            IndicatorRight.GetComponent<Indicator>().indicatorState = IndicatorState.reachable;
        } else {
            if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicatorRed, x + 1, y)) == null && GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x + 1, y)) == null) {
                GenerateFieldCard(CardID.FieldIndicatorRed, x + 1, y);
            }
        }

        GameObject IndicatorDown = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y - 1));
        if (IndicatorDown != null && IndicatorDown.GetComponent<Indicator>().indicatorState != IndicatorState.blocked) {
            IndicatorDown.GetComponent<Indicator>().indicatorState = IndicatorState.reachable;
        } else {
            if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicatorRed, x, y - 1)) == null && GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y - 1)) == null) {
                GenerateFieldCard(CardID.FieldIndicatorRed, x, y - 1);
            }
        }

        GameObject IndicatorUp = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y + 1));
        if (IndicatorUp != null && IndicatorUp.GetComponent<Indicator>().indicatorState != IndicatorState.blocked) {
            IndicatorUp.GetComponent<Indicator>().indicatorState = IndicatorState.reachable;
        } else {
            if (GameObject.Find(Slave.GetCardName(CardID.FieldIndicatorRed, x, y + 1)) == null && GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, x, y + 1)) == null) {
                GenerateFieldCard(CardID.FieldIndicatorRed, x, y + 1);
            }
        }

        for (int a = x - 2; a <= x + 2; a++) {
            for (int b = y - 2; b <= y + 2; b++) {
                GameObject Indicator = GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, a, b));
                if (Indicator != null && Indicator.GetComponent<Indicator>().indicatorState != IndicatorState.blocked) {
                    IndicatorState state = Indicator.GetComponent<Indicator>().indicatorState;
                    if (state == IndicatorState.unreachable) {
                        Indicator.GetComponent<Indicator>().indicatorState = IndicatorState.anchorfield;
                    }
                }
            }
        }
    }

    void ToggleDeleteCardFieldVisibility() {
        if (currentChoosedCard == CardID.Deletecard && deleteIndicatorVisible == false) {
            deleteIndicatorVisible = true;
            for (int i = 0; i < Field.cardsOnField.Count; i++) {
                GameObject Card = Field.cardsOnField[i];
                if (Card.GetComponent<Card>().team != Team.system) {
                    ShowCardIndicators(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
                }
            }
        } else if (currentChoosedCard != CardID.Deletecard && deleteIndicatorVisible == true) {
            deleteIndicatorVisible = false;
            HideCardIndicator();
        }
        if (currentChoosedCard == CardID.Changecard && changeIndicatorVisible == false) {
            changeIndicatorVisible = true;
            for (int i = 0; i < Field.cardsOnField.Count; i++) {
                GameObject Card = Field.cardsOnField[i];
                if (Card.GetComponent<Card>().team != currentPlayer && Card.GetComponent<Card>().cardid == CardID.Pointcard) {
                    ShowCardIndicators(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
                }
            }
        } else if (currentChoosedCard != CardID.Changecard && changeIndicatorVisible == true) {
            changeIndicatorVisible = false;
            HideCardIndicator();
        }
        if (currentChoosedCard == CardID.Shufflecard && shuffleIndicatorVisible == false) {
            shuffleIndicatorVisible = true;
            for (int i = 0; i < Field.cardsOnField.Count; i++) {
                GameObject Card = Field.cardsOnField[i];
                int x = Card.GetComponent<Card>().x;
                int y = Card.GetComponent<Card>().y;

                GameObject CardLeft = GameObject.Find(Slave.GetCardName(CardID.Card, x - 1, y));
                GameObject CardRight = GameObject.Find(Slave.GetCardName(CardID.Card, x + 1, y));
                GameObject CardUp = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 1));
                GameObject CardDown = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 1));
                if (Card.GetComponent<Card>().team == currentPlayer || Card.GetComponent<Card>().team == Team.system) continue;
                if (CardLeft != null) {
                    if (CardLeft.GetComponent<Card>().cardid == CardID.Pointcard
                        || CardLeft.GetComponent<Card>().cardid == CardID.Blankcard
                        && CardLeft.GetComponent<Card>().team == currentPlayer) {
                        ShowCardIndicators(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
                    }
                }
                if (CardRight != null) {
                    if (CardRight.GetComponent<Card>().cardid == CardID.Pointcard
                          || CardRight.GetComponent<Card>().cardid == CardID.Blankcard
                          && CardRight.GetComponent<Card>().team == currentPlayer) {
                        ShowCardIndicators(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
                    }
                }
                if (CardUp != null) {
                    if (CardUp.GetComponent<Card>().cardid == CardID.Pointcard
                  || CardUp.GetComponent<Card>().cardid == CardID.Blankcard
                  && CardUp.GetComponent<Card>().team == currentPlayer) {
                        ShowCardIndicators(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
                    }
                }
                if (CardDown) {
                    if (CardDown.GetComponent<Card>().cardid == CardID.Pointcard
                          || CardDown.GetComponent<Card>().cardid == CardID.Blankcard
                          && CardDown.GetComponent<Card>().team == currentPlayer) {
                        ShowCardIndicators(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
                    }
                }
            }
        } else if (currentChoosedCard != CardID.Shufflecard && shuffleIndicatorVisible == true) {
            shuffleIndicatorVisible = false;
            HideCardIndicator();
        }
    }

    void ShowCardIndicators(int x, int y) {
        GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y));
        CardIndicator.GetComponent<Indicator>().indicatorColor = IndicatorColor.yellowcovered;
    }

    void HideCardIndicator() {
        for (int x = Camera.main.GetComponent<CameraManager>().min_x; x <= Camera.main.GetComponent<CameraManager>().max_x; x++) {
            for (int y = Camera.main.GetComponent<CameraManager>().min_y; y <= Camera.main.GetComponent<CameraManager>().max_y; y++) {
                GameObject CardIndicator = GameObject.Find(Slave.GetCardName(CardID.CardIndicator, x, y));
                CardIndicator.GetComponent<Indicator>().indicatorColor = IndicatorColor.transparent;
            }
        }
    }

    public void RemoveUnconnectedCards() {
        print("Removing unconnected Cards");
        MarkUnconnectedCards();
        DeleteUnconnectedCards();
        RenewIndicators();
    }

    void DeleteUnconnectedCards() {
        GameObject F = GameObject.Find("Field");

        for (int x = Camera.main.GetComponent<CameraManager>().min_x; x <= Camera.main.GetComponent<CameraManager>().max_x; x++) {
            for (int y = Camera.main.GetComponent<CameraManager>().min_y; y <= Camera.main.GetComponent<CameraManager>().max_y; y++) {
                GameObject Card = GameObject.Find(Slave.GetCardName(CardID.Card, x, y));
                if (Card != null
                    && Card.GetComponent<Card>().visited == false) {
                    RemoveCard(Card);
                    DestroyImmediate(Card);
                } else if (Card != null
                     && Card.GetComponent<Card>().visited == true) {
                    Card.GetComponent<Card>().visited = false;
                }
            }
        }
    }

    void MarkUnconnectedCards() {
        List<GameObject> anchor = new List<GameObject>();
        for (int i = 0; i < Field.cardsOnField.Count; i++) {
            if (Field.cardsOnField[i].GetComponent<Card>().cardid == CardID.Anchorcard
                || Field.cardsOnField[i].GetComponent<Card>().cardid == CardID.Startpoint) {
                anchor.Add(Field.cardsOnField[i]);
            }
        }

        for (int i = 0; i < anchor.Count; i++) {
            Queue<GameObject> ToDo = new Queue<GameObject>();
            ToDo.Enqueue(anchor[i]);
            anchor[i].GetComponent<Card>().visited = true;

            while (ToDo.Count > 0) {
                GameObject CurrentGameObject = ToDo.Dequeue();
                int x = CurrentGameObject.GetComponent<Card>().x;
                int y = CurrentGameObject.GetComponent<Card>().y;

                //rechts
                GameObject CardRight = GameObject.Find(Slave.GetCardName(CardID.Card, x + 1, y));
                if (CardRight != null) {
                    if (CardRight.GetComponent<Card>().visited != true) {
                        CardRight.GetComponent<Card>().visited = true;
                        ToDo.Enqueue(CardRight);
                    }
                }
                //links
                GameObject CardLeft = GameObject.Find(Slave.GetCardName(CardID.Card, x - 1, y));
                if (CardLeft != null) {
                    if (CardLeft.GetComponent<Card>().visited != true) {
                        CardLeft.GetComponent<Card>().visited = true;
                        ToDo.Enqueue(CardLeft);
                    }
                }
                //oben
                GameObject CardUp = GameObject.Find(Slave.GetCardName(CardID.Card, x, y + 1));
                if (CardUp != null) {
                    if (CardUp.GetComponent<Card>().visited != true) {
                        CardUp.GetComponent<Card>().visited = true;
                        ToDo.Enqueue(CardUp);
                    }
                }
                //unten
                GameObject CardDown = GameObject.Find(Slave.GetCardName(CardID.Card, x, y - 1));
                if (CardDown != null) {
                    if (CardDown.GetComponent<Card>().visited != true) {
                        CardDown.GetComponent<Card>().visited = true;
                        ToDo.Enqueue(CardDown);
                    }
                }
            }
        }
    }

    public int GetPointCardNumber(Team team) {
        if (team == Team.red) {
            PointCardCounterRed++;
            PointCardCounterRed = PointCardCounterRed % 15;
            print("PointCardCounterRed set to: " + PointCardCounterRed);
            return PointCardCounterRed;
        } else {
            PointCardCounterBlue++;
            PointCardCounterBlue = PointCardCounterBlue % 15;
            print("PointCardCounterBlue set to: " + PointCardCounterBlue);

            return PointCardCounterBlue % 15;
        }
    }
    public void RemoveCard(GameObject DeletedCard) {
        if (DeletedCard == null || DeletedCard.GetComponent<Card>().cardid == CardID.Startpoint) return;
        for (int i = 0; i < Field.GetComponent<Field>().cardsOnField.Count; i++) {
            if (Field.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().cardid == DeletedCard.GetComponent<Card>().cardid
                && Field.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().x == DeletedCard.GetComponent<Card>().x
                && Field.GetComponent<Field>().cardsOnField[i].GetComponent<Card>().y == DeletedCard.GetComponent<Card>().y) {
                Field.GetComponent<Field>().cardsOnField.RemoveAt(i);
                break;
            }
        }
        if (DeletedCard.GetComponent<Card>().cardid == CardID.Blockcard) {
            Block blockdirection = GameObject.Find(Slave.GetCardName(CardID.Card, DeletedCard.GetComponent<Card>().x, DeletedCard.GetComponent<Card>().y)).GetComponent<BlockCard>().blockDirection;
            switch (blockdirection) {
                case Block.right:
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x + 1, DeletedCard.GetComponent<Card>().y)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x + 1, DeletedCard.GetComponent<Card>().y)).GetComponent<Indicator>().team = Team.system;
                    break;
                case Block.left:
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x - 1, DeletedCard.GetComponent<Card>().y)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x - 1, DeletedCard.GetComponent<Card>().y)).GetComponent<Indicator>().team = Team.system;
                    break;
                case Block.up:
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x, DeletedCard.GetComponent<Card>().y + 1)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x, DeletedCard.GetComponent<Card>().y + 1)).GetComponent<Indicator>().team = Team.system;
                    break;
                case Block.down:
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x, DeletedCard.GetComponent<Card>().y - 1)).GetComponent<Indicator>().indicatorState = IndicatorState.unreachable;
                    GameObject.Find(Slave.GetCardName(CardID.FieldIndicator, DeletedCard.GetComponent<Card>().x, DeletedCard.GetComponent<Card>().y - 1)).GetComponent<Indicator>().team = Team.system;
                    break;
            }
        }
        print("RemovedCard " + DeletedCard.GetComponent<Card>().cardid + " at " + DeletedCard.GetComponent<Card>().x + "," + DeletedCard.GetComponent<Card>().y + "!");
        Card MyCard = new Card();
        MyCard.lastevent = LastEvent.destroyed;
        MyCard.cardid = DeletedCard.GetComponent<Card>().cardid;
        MyCard.PointCardCounter = DeletedCard.GetComponent<Card>().PointCardCounter;
        MyCard.team = DeletedCard.GetComponent<Card>().team;
        MyCard.x = DeletedCard.GetComponent<Card>().x;
        MyCard.y = DeletedCard.GetComponent<Card>().y;
        MyCard.lastevent = LastEvent.destroyed;
        CardsAffectedLastRound.Add(MyCard);
        DestroyImmediate(DeletedCard);
    }
}

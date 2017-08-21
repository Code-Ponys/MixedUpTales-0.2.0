using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Card : MonoBehaviour {

    public CardID cardid;
    public Team team;
    public int x;
    public int y;
    public bool visited;
    public int PointCardCounter;
    public LastEvent lastevent;
}
using UnityEngine;

[System.Serializable]
public class FieldProperties : System.Object {
    public int _size = 51;
}

public struct MousePos {
    public int x;
    public int y;
}

public enum CardID {
    Anchorcard,
    Blankcard, Blockcard,Burncard,
    Cancercard, Changecard, Deletecard,
    Doublecard, HotPotatoe, Infernocard,
    Nukecard, Pointcard, Shufflecard,
    Startpoint, Vortexcard,
    FieldIndicator, FieldIndicatorRed, FieldIndicatorGreen, FieldIndicatorBlack, FieldIndicatorYellow,
    none, ChoosedCard, placed,
    CardIndicator, CardIndicatorRed, Card
}
public enum Team {
    blue = 0, red = 1, system = -1,
}
public enum Block {
    up, right, left, down
}

public enum IndicatorType {
    card, field
}

public enum IndicatorColor {
    transparent, black, green, yellow, red, redcovered, greencovered, yellowcovered
}

public class Materials {
    //Material transparent = new Material();
}
public enum IndicatorState {
    blocked, reachable, unreachable, anchorfield
}

public enum LastEvent {
    placed, destroyed,changed
}
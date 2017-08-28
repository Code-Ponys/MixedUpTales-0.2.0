using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static public class Slave {


    static public string GetImagePathPf(CardID card, Team team) {
        if (card != CardID.FieldIndicator
            && card != CardID.FieldIndicatorRed
            && card != CardID.Startpoint
            && card != CardID.CardIndicator) {
            return "cards/" + team + "/pf_" + card;
        }
        switch (card) {
            default:
                return "cards/" + team + "/pf_Errorcard";
            case CardID.FieldIndicator:
                return "emptycards/pf_transparent";
            case CardID.FieldIndicatorRed:
                return "emptycards/pf_red";
            case CardID.Startpoint:
                return "cards/pf_Startpoint";
            case CardID.CardIndicator:
                return "emptycards/pf_transparent";
        }
    }

    static public string GetImagePath(CardID card, Team team) {
        switch (card) {
            case CardID.none:
                return "emptycards/transparent";
            case CardID.Blankcard:
                return "cards/" + team + "/Blankcard";
            case CardID.Pointcard:
                return "cards/" + team + "/Pointcard";
            case CardID.Startpoint:
                return "cards/" + team + "/Startpoint";
            case CardID.Blockcard:
                return "cards/" + team + "/Blockcard";
            case CardID.Doublecard:
                return "cards/" + team + "/Doublecard";
            case CardID.Deletecard:
                return "cards/" + team + "/Deletecard";
            case CardID.Burncard:
                return "cards/" + team + "/Burncard";
            case CardID.Infernocard:
                return "cards/" + team + "/Infernocard";
            case CardID.Changecard:
                return "cards/" + team + "/Changecard";
            case CardID.Cancercard:
                return "cards/" + team + "/Cancercard";
            case CardID.HotPotatoe:
                return "cards/" + team + "/HotPotatoe";
            case CardID.Nukecard:
                return "cards/" + team + "/Nukecard";
            case CardID.Vortexcard:
                return "cards/" + team + "/Vortexcard";
            case CardID.Anchorcard:
                return "cards/" + team + "/Anchorcard";
            case CardID.Shufflecard:
                return "cards/" + team + "/Shufflecard";
            case CardID.CardIndicator:
                return "emptycards/transparent";
            case CardID.CardIndicatorRed:
                return "emptycards/redcovered";
            case CardID.FieldIndicatorGreen:
                return "emptycards/green";
            case CardID.FieldIndicatorBlack:
                return "emptycards/transparent";
            case CardID.FieldIndicatorYellow:
                return "emptycards/yellow";
            case CardID.FieldIndicatorRed:
                return "emptycards/red";
            default:
                return "cards/" + team + "/Errorcard";
        }
        //return "cards/" + team + "/" + card;


    }

    static public string GetImagePath(Team team, int counter) {

        return "cards/" + team + "/" + team + counter;
    }

    static public string GetImagePath(IndicatorColor color) {
        return "emptycards/" + color;
    }


    static public string GetCardName(CardID cardid, int x, int y) {
        switch (cardid) {
            default:
                return "Error " + x + "," + y;
            case CardID.Reconstruct:
                return "Card " + x + "," + y + " Reconstruct";
            case CardID.Card:
                return "Card " + x + "," + y;
            case CardID.Blankcard:
                return "Card " + x + "," + y;
            case CardID.Pointcard:
                return "Card " + x + "," + y;
            case CardID.Startpoint:
                return "Card " + x + "," + y;
            case CardID.Blockcard:
                return "Card " + x + "," + y;
            case CardID.FieldIndicator:
                return "FieldIndicator " + x + "," + y;
            case CardID.FieldIndicatorRed:
                return "FieldIndicatorRed " + x + "," + y;
            case CardID.Doublecard:
                return "Doublecard " + x + "," + y;
            case CardID.Deletecard:
                return "Deletecard " + x + "," + y;
            case CardID.Burncard:
                return "Burncard " + x + "," + y;
            case CardID.Infernocard:
                return "Infernocard " + x + "," + y;
            case CardID.Changecard:
                return "Changecard " + x + "," + y;
            case CardID.Cancercard:
                return "Cancercard " + x + "," + y;
            case CardID.HotPotatoe:
                return "HotPotatoe " + x + "," + y;
            case CardID.Nukecard:
                return "Nukecard " + x + "," + y;
            case CardID.Vortexcard:
                return "Vortexcard " + x + "," + y;
            case CardID.Anchorcard:
                return "Card " + x + "," + y;
            case CardID.Shufflecard:
                return "Shufflecard " + x + "," + y;
            case CardID.CardIndicator:
                return "CardIndicator " + x + "," + y;
        }
    }

    public static string GetCardName(CardID card) {
        switch (card) {
            default:
                return "";
            case CardID.none:
                return "";
            case CardID.Blankcard:
                return "Blankcard";
            case CardID.Pointcard:
                return "Pointcard";
            case CardID.Blockcard:
                return "Blockcard";
            case CardID.Doublecard:
                return "Doublecard";
            case CardID.Deletecard:
                return "Deletecard";
            case CardID.Burncard:
                return "Burncard";
            case CardID.Infernocard:
                return "Infernocard";
            case CardID.Changecard:
                return "Changecard";
            case CardID.Cancercard:
                return "Cancercard";
            case CardID.HotPotatoe:
                return "Discardcard";
            case CardID.Nukecard:
                return "Restartcard";
            case CardID.Vortexcard:
                return "Exchangecard";
            case CardID.Anchorcard:
                return "Anchorcard";
            case CardID.Shufflecard:
                return "Shufflecard";
        }
    }

    public static string GetCardDescription(CardID card) {
        switch (card) {
            default:
                return "";
            case CardID.none:
                return "";
            case CardID.Blankcard:
                return "Use this card to block your opponent's moves.";
            case CardID.Pointcard:
                return "Place 3 of these cards diagonally, horizontally or vertically in a row to win.";
            case CardID.Blockcard:
                return "Blocks a field and reserves an empty, adjacent one for you to use later.";
            case CardID.Doublecard:
                return "Places first a Blankcard and later a Pointcard from your deck.";
            case CardID.Deletecard:
                return "Destroys any card from the playfield.";
            case CardID.Burncard:
                return "Destroys one adjacent card and itself.";
            case CardID.Infernocard:
                return "Clears 3 fields in any straight direction, starting from its position.";
            case CardID.Changecard:
                return "Turns an opponent's pointcard into a blankcard.";
            case CardID.Cancercard:
                return "Turns all cards in its line and column into Blankcards.";
            case CardID.HotPotatoe:
                return "The opponent must discard all Specialcards on his hand.";
            case CardID.Nukecard:
                return "All cards are removed from the playfield.";
            case CardID.Vortexcard:
                return "Exchange your deck and handcards with your opponent.";
            case CardID.Anchorcard:
                return "Creates a new starting point, max. 2 fields away from other cards.";
            case CardID.Shufflecard:
                return "Switches places of your card with an opponents card, if they’re adjacent.";
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Scripte used for both player and dealer

    public CardScript cardScript;
    public DeckScript deckScript;

    // total value of hand
    public int handValue = 0;

    public int money = 1000;

    //array of cards on the tabl
    public GameObject[] hand;

    // index of next card to be turned over
    public int cardIndex = 0;
    //keep track of aces
    List<CardScript> aceList = new List<CardScript>();

    // Start is called before the first frame update
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    public int GetCard() {
        //get a card, we use deal card to assign the sprite and value to card on the table
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // add card value
        handValue += cardValue;
        //check for ace
        if(cardValue == 1) {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }

        AceCheck();

        cardIndex++;
        return handValue;
    }

    public void AceCheck() {
        foreach(CardScript ace in aceList) {
            if(handValue +10 < 22 && ace.GetValueOfCard() == 1) {
                ace.SetValue(11);
                handValue += 10;
            } else if (handValue > 21 && ace.GetValueOfCard() == 11) {
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    public void AdjustMoney(int amount) {
        money += amount;
    }

    public int GetMoney() {
        return money;
    }

    public void ResetHand() {
        for(int i = 0; i < hand.Length; i++) {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}

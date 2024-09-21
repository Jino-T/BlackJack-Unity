using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Buttons
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;

    private int standClicks = 0;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI  dealerScoreText;
    public TextMeshProUGUI  betsText;
    public TextMeshProUGUI  cashText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI  standBtnText;

    // Card hiding dealers card
    public GameObject hideCard;
    int pot = 0;
    public GameObject ee;

    // Start is called before the first frame update
    void Start()
    {
        //Control button visibility
        dealBtn.gameObject.SetActive(true);
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        ee.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetMoney() < 0) {
            ee.gameObject.SetActive(true);
        } else {
            ee.gameObject.SetActive(false);
        }
        
    }

    public void DealClicked() {
        playerScript.ResetHand();
        dealerScript.ResetHand();

        mainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        scoreText.text = "Hand: " + playerScript.handValue.ToString();
        dealerScoreText.text = "Dealer Hand: " + dealerScript.handValue.ToString();

        hideCard.GetComponent<Renderer>().enabled = true;

        //Control button visibility
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        betBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        //Set Pot Size
        pot = 40;
        betsText.text = "Bets: $" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
    }
    public void HitClicked() {
        if(playerScript.cardIndex <= 10) {
            playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if(playerScript.handValue > 20) RoundOver();
        }

    }

    public void StandClicked() {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standBtnText.text = "Call";

    }

    private void HitDealer() {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10) {
            dealerScript.GetCard();
            dealerScoreText.text = "Dealer Hand: " + dealerScript.handValue.ToString();
            if(dealerScript.handValue > 20) RoundOver();
        }
    }

    void RoundOver() {
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        //if stand hasn't been clicked twice and no 21s or busts
        if(standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;

        //double bust
        if(playerBust && dealerBust) {
            mainText.text = "Double Bust! Bets Returned"; 
            playerScript.AdjustMoney(pot / 2);

        //dealer win
        } else if (playerBust || !dealerBust && dealerScript.handValue >  playerScript.handValue) {
            mainText.text = "Dealer Wins! :("; 
        //player win
        } else if (dealerBust || dealerScript.handValue <  playerScript.handValue) {
            mainText.text = "You Win! :)"; 
            playerScript.AdjustMoney(pot);
        //tie
        } else if (dealerScript.handValue ==  playerScript.handValue) {
            mainText.text = "Push! Bets Returned"; 
            playerScript.AdjustMoney(pot/2);
        } else {
            roundOver = false;
        }
        //start things over
        if(roundOver) {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            betBtn.gameObject.SetActive(false);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + playerScript.GetMoney().ToString();
            standClicks = 0;
        }
        
    }

    public void BetClicked() {
        Text newBet = betBtn.GetComponentInChildren(typeof(Text)) as Text;

        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        pot += (20*2);
        betsText.text = "Bets: $" + pot.ToString();
    }
}

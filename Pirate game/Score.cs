using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    // een singleton
    public static Score Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
       
    }

    
    [SerializeField] private TMP_Text HighScore, CurrentScore , Currency;
    public int ScoreNumber , Gold ,MainMenu;
    

    private string PlayerName;
    [SerializeField] private GameObject Inputfield;

    [SerializeField] private GameObject UiItems;

    
    private void Start()
    {
        Time.timeScale = 0f;       // als de game wordt gestart wordt tijd still gezet zodat de player de tijd heeft om zijn naam in te vullen
    }


    //deze functie is gelinkt aan de confirm button dit voort all je informatie in en ook de oudere highscore en start dan de game
    public void EnterPlayerInfo()
    {
        PlayerName = Inputfield.GetComponent<TMP_Text>().text;              // dit zet de tekst die je in de inputbar hebt gezet in een string die dan de player naam word
        CurrentScore.text = "name: "+ PlayerName + " score: " + ScoreNumber.ToString();     // dit zet de player zijn naam en score(die zal altijd 0 hier zijn) in het currentscore object zodat je het in game kan zien
        HighScore.text = "name: " + PlayerPrefs.GetString("Player") + " HighScore: " + PlayerPrefs.GetInt("HighScore"); // hier wordt de oude highscore en zijn houder in het highscore object gezet

        Currency.text = "gold:" + Gold.ToString();

        // de game wordt gestart en ui objects gaan uit zodat je de game kan zien
        Time.timeScale = 1f;   
        UiItems.SetActive(false);
    }


    // dit is een functie die overal aangeroepen kan worden om score toe te voegen in de huidige game
    public void Addscore(int _score, int _gold)
    {
        ScoreNumber += _score;    // de doorgegeven hoeveelheid wordt toegevoegd
        Gold += _gold;
        CurrentScore.text = "name: " + PlayerName + " score: " + ScoreNumber.ToString();  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
        Currency.text = "gold:" + Gold.ToString();  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
        
        CheckHighScore();
    }

    public void RemoveGold(int _cost)
    {
        Gold -= _cost;
        Currency.text = "gold:" + Gold.ToString();
    }

    //hier wordt gecheckt of een nieuwe highscore is gehaald
    private void CheckHighScore()
    {
        if (ScoreNumber > PlayerPrefs.GetInt("HighScore"))    // hij kijkt of de huidige score hoger is dan de high score
        {
            HighScore.text = "name: " + PlayerName + " HighScore: " + ScoreNumber;  // omdat de playerprefs niet direct updaten wordt dan de huidige score en player toegevoegd aan het highscore gobject

            // de nieuwe highscore wordt gesaved
            PlayerPrefs.SetInt("HighScore", ScoreNumber);
            PlayerPrefs.SetString("Player", PlayerName);
        }
        else
        {
            HighScore.text = "name: " + PlayerPrefs.GetString("Player") + "HighScore: " + PlayerPrefs.GetInt("HighScore"); // er verandert niks aan de highscore
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }
}

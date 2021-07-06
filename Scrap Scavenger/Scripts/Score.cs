using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : Singleton<Score>
{
    // een singleton
    //public static Score Instance;

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //}
    
    [SerializeField] private TMP_Text HighScore, CurrentScore , Currency;
    public int Scrap ,MainMenu;
    public float ScoreNumber;
    float CurrentTime;

    private void Start()
    {
        CurrentScore.text = " SCORE: " + ScoreNumber.ToString("F0");                                 // dit zet de player zijn naam en score(die zal altijd 0 hier zijn) in het currentscore object zodat je het in game kan zien
        HighScore.text = " HIGHSCORE: " + PlayerPrefs.GetFloat("HighScore").ToString("F1");                       // hier wordt de oude highscore en zijn houder in het highscore object gezet

        Currency.text = Scrap.ToString();
    }

    // dit is een functie die overal aangeroepen kan worden om score toe te voegen in de huidige game
    public void Addscore(int _score, int _gold)
    {
        ScoreNumber += _score;    // de doorgegeven hoeveelheid wordt toegevoegd
        Scrap += _gold;
        CurrentScore.text = " SCORE: " + ScoreNumber.ToString();  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
        Currency.text = Scrap.ToString();  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
        
        CheckHighScore();
    }

    public void RemoveGold(int _cost)
    {
        Scrap -= _cost;
        Currency.text = Scrap.ToString();
    }

    private void Update()
    {
        if (deadCheck.Instance.dead == false && PlayerState.Instance.Cs == PlayerState.CurrentState.MOVING)
        {
            ScoreNumber += Time.deltaTime;
            CurrentScore.text = " SCORE: " + ScoreNumber.ToString("F1");  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten

            CheckHighScore();
        }
    }
    //hier wordt gecheckt of een nieuwe highscore is gehaald
    private void CheckHighScore()
    {
        if (ScoreNumber > PlayerPrefs.GetFloat("HighScore"))    // hij kijkt of de huidige score hoger is dan de high score
        {
            HighScore.text = " HIGHSCORE " + ScoreNumber.ToString("F1"); ;  // omdat de playerprefs niet direct updaten wordt dan de huidige score en player toegevoegd aan het highscore gobject

            // de nieuwe highscore wordt gesaved
            PlayerPrefs.SetFloat("HighScore", ScoreNumber);
        }
        else
        {
            HighScore.text = "HIGHSCORE " + PlayerPrefs.GetFloat("HighScore").ToString("F1"); // er verandert niks aan de highscore
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }
}

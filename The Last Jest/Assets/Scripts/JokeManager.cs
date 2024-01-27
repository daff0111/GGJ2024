using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static JokeManager;

public class JokeManager : MonoBehaviour
{
    public enum JokeStructure
    {
        SubjectVerbObject,
        SubjectIsAdjective,
    }

    public enum Noun
    {
        King,
        Executioner,
        Heir,
        Audience,
        Jester,
        Queen,
        Pig,
        Food,
    }

    public enum Verb
    {
        Is,
        Farts,
        Loves,
        Hates,
        Kills,
        Serves,
    }

    public enum Adjective
    {
        Ugly,
        Funny,
        Fat,
        Poor,
        Stupid,
        Small,
        Big,

    }

    public JesterCharacter Jester;
    public Character King;
    public Character Queen;
    public Character Heir;
    public ExecutionerCharacter Executioner;

    protected int JokeCount = 0;
    protected bool GameOver = false;
    public int Level = 0;

    // Start is called before the first frame update
    void Start()
    {
        JokeCount = 0;
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reload Game
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            return;
        }
    }

    void TellJoke()
    {
        JokeCount++;
        
        if (Heir.GetAngryMeter() <= 0)
        {
            // Heir is Angry, King gets angry too
            King.AddAngryMeter(-30);
        }

        if (King.GetAngryMeter() <= 0) 
        {
            // You Died - King Orders your death
            EndGame();

        }

        if (Executioner.GetAngryMeter() <= 0)
        {
            // You Died - The Executioner is angry and he kills you
            Executioner.RevealAngryFace();
            EndGame();
        }

        if(Queen.GetAngryMeter() <= 0)
        {
            // Queen is Angry - nex day she'll be in the executioner place
        }
    }

    public void SubmitJoke(Noun subject, Verb verb, Noun jokeObject)
    {
        Debug.LogFormat("{0} {1} {2}", subject.ToString(), verb.ToString(), jokeObject.ToString());
        if (GameOver)
        {
            return;
        }

        switch (subject)
        {
            case Noun.Queen:
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Crying);
                Heir.AddEmotionReaction(EEmotionType.Angry);
                Executioner.AddEmotionReaction(EEmotionType.Embarrassed);
                //Audience.AddReaction
                break;
            case Noun.King:
                King.AddEmotionReaction(EEmotionType.Angry);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Neutral);
                Executioner.AddEmotionReaction(EEmotionType.Embarrassed);
                //Audience.AddReaction
                break;
            case Noun.Audience:
                King.AddEmotionReaction(EEmotionType.Embarrassed);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Angry);
                Executioner.AddEmotionReaction(EEmotionType.Happy);
                //Audience.AddReaction
                break;
            case Noun.Heir:
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Embarrassed);
                Heir.AddEmotionReaction(EEmotionType.Crying);
                Executioner.AddEmotionReaction(EEmotionType.Neutral);
                //Audience.AddReaction
                break;
            case Noun.Executioner:
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Neutral);
                Executioner.AddEmotionReaction(EEmotionType.Angry);
                //Audience.AddReaction
                break;
            case Noun.Food:
                King.AddEmotionReaction(EEmotionType.Embarrassed);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Neutral);
                Executioner.AddEmotionReaction(EEmotionType.Happy);
                //Audience.AddReaction
                break;
            default:
                break;
        }

        TellJoke();
    }

    public void SubmitJoke(Noun subject, Adjective adjective)
    {
        Debug.LogFormat("{0} is {1}", subject.ToString(), adjective.ToString());
        switch (subject)
        {
            case Noun.Queen:
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Angry);
                Heir.AddEmotionReaction(EEmotionType.Happy);
                Executioner.AddEmotionReaction(EEmotionType.Neutral);
                //Audience.AddReaction
                break;
            case Noun.King:
                King.AddEmotionReaction(EEmotionType.Angry);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Happy);
                Executioner.AddEmotionReaction(EEmotionType.Embarrassed);
                //Audience.AddReaction
                break;
            case Noun.Audience:
                King.AddEmotionReaction(EEmotionType.Embarrassed);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Happy);
                Executioner.AddEmotionReaction(EEmotionType.Happy);
                //Audience.AddReaction
                break;
            case Noun.Heir:
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Crying);
                Executioner.AddEmotionReaction(EEmotionType.Neutral);
                //Audience.AddReaction
                break;
            case Noun.Executioner:
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Heir.AddEmotionReaction(EEmotionType.Crying);
                Executioner.AddEmotionReaction(EEmotionType.Angry);
                //Audience.AddReaction
                break;
            default:
                break;
        }
        TellJoke();
    }

    void EndGame()
    {
        Executioner.KillJester();
        GameOver = true;
        Debug.Log("YOU DIED - You told " + JokeCount + " Jokes. Press 'R' to Restart");
    }
}
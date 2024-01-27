using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static JokeManager;

public class JokeManager : MonoBehaviour
{
    public enum EJokeType
    {
        A,
        B,
        C,
        D
    };

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
    public Character Princess;
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            TellJoke(EJokeType.A);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TellJoke(EJokeType.B);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TellJoke(EJokeType.C);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TellJoke(EJokeType.D);
        }
    }

    void TellJoke(EJokeType joke)
    {
        JokeCount++;
        Debug.Log("Joke "+ joke.ToString() + " Told");
        switch (joke) 
        {
            case EJokeType.A:
                Debug.Log("The Queen likes to slap the Executioner");
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Angry);
                Princess.AddEmotionReaction(EEmotionType.Angry);
                Executioner.AddEmotionReaction(EEmotionType.Embarrassed);
                //Public.AddReaction
                break;
            case EJokeType.B:
                Debug.Log("The King has a big butt");
                King.AddEmotionReaction(EEmotionType.Angry);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Princess.AddEmotionReaction(EEmotionType.Neutral);
                Executioner.AddEmotionReaction(EEmotionType.Embarrassed);
                //Public.AddReaction
                break;
            case EJokeType.C:
                Debug.Log("The Princess calls the king Poopface");
                King.AddEmotionReaction(EEmotionType.Embarrassed);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Princess.AddEmotionReaction(EEmotionType.Angry);
                Executioner.AddEmotionReaction(EEmotionType.Happy);
                //Public.AddReaction
                break;
            case EJokeType.D:
                Debug.Log("The Princess is a spoiled Bratt");
                King.AddEmotionReaction(EEmotionType.Happy);
                Queen.AddEmotionReaction(EEmotionType.Happy);
                Princess.AddEmotionReaction(EEmotionType.Crying);
                Executioner.AddEmotionReaction(EEmotionType.Neutral);
                //Public.AddReaction
                break;
            default:
                break;
        }
        
        if (Princess.GetAngryMeter() <= 0)
        {
            // Princess is Angry, King gets angry too
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
    }

    public void SubmitJoke(Noun subject, Adjective adjective)
    {
        Debug.LogFormat("{0} is {1}", subject.ToString(), adjective.ToString());
    }

    void EndGame()
    {
        Executioner.KillJester();
        GameOver = true;
        Debug.Log("YOU DIED - You told " + JokeCount + " Jokes. Press 'R' to Restart");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class JokeManager : MonoBehaviour
{
    public enum EJokeType
    {
        A,
        B,
        C,
        D
    };

    public JesterCharacter Jester;
    public Character King;
    public Character Queen;
    public Character Princess;
    public ExecutionerCharacter Executioner;

    protected int JokeCount = 0;
    protected bool GameOver = false;

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
                King.AddAngryMeter(20);
                Queen.AddAngryMeter(-20);
                Executioner.AddAngryMeter(-20);
                break;
            case EJokeType.B:
                Debug.Log("The King has a big butt");
                King.AddAngryMeter(-40);
                Queen.AddAngryMeter(20);
                Princess.AddAngryMeter(20);
                break;
            case EJokeType.C:
                Debug.Log("The Princess calls the king Poopface");
                King.AddAngryMeter(-10);
                Queen.AddAngryMeter(10);
                Princess.AddAngryMeter(-20);
                Executioner.AddAngryMeter(20);
                break;
            case EJokeType.D:
                Debug.Log("The Princess is a spoiled Bratt");
                King.AddAngryMeter(20);
                Queen.AddAngryMeter(10);
                Princess.AddAngryMeter(-30);
                Executioner.AddAngryMeter(20);
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

    void EndGame()
    {
        Executioner.KillJester();
        GameOver = true;
        Debug.Log("YOU DIED - You told " + JokeCount + " Jokes. Press 'R' to Restart");
    }
}
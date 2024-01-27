using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JokeManager;

public class JokeManager : MonoBehaviour
{
    public enum EJokeType
    {
        A,
        B,
        C
    };

    public enum JokeStructure
    {
        SubjectVerbObject,
        SubjectIsAdjective,
    }

    public enum Noun
    {
        King,
        Queen,
    }

    public enum Verb
    {
        Is,
        Eats,
        Farts,
        Kills,
    }

    public enum Adjective
    {
        Fat,
        Ugly,
        Poor,
        Vain,

    }

    public JesterCharacter Jester;
    public Character King;
    public Character Queen;
    public Character Princess;
    public ExecutionerCharacter Executioner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void TellJoke(EJokeType joke)
    {
        Debug.Log("Joke "+ joke.ToString() + " Told");
        switch (joke) 
        {
            case EJokeType.A:
                
                King.AddAngryMeter(20);
                Queen.AddAngryMeter(-20);
                Executioner.AddAngryMeter(-20);
                break;
            case EJokeType.B:
                King.AddAngryMeter(-40);
                Queen.AddAngryMeter(20);
                Princess.AddAngryMeter(20);
                break;
            case EJokeType.C:
                King.AddAngryMeter(-30);
                Queen.AddAngryMeter(10);
                Princess.AddAngryMeter(-20);
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
            Executioner.KillJester();
        }

        if (Executioner.GetAngryMeter() <= 0)
        {
            // You Died - The Executioner is angry and he kills you
            Executioner.KillJester();
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
}

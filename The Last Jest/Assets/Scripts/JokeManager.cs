using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static JokeManager;

public class JokeManager : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterMultipliersSet
    {
        public CharacterMultiplier KingMultiplier;
        public CharacterMultiplier QueenMultiplier;
        public CharacterMultiplier HeirMultiplier;
        public CharacterMultiplier AudienceMultiplier;
        public CharacterMultiplier ExecutionerMultiplier;
    }

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
    public AudienceCharacter Audience;

    protected int JokeCount = 0;
    protected bool GameOver = false;
    public int Level = 0;

    public CharacterMultipliersSet Level1MultiplierSet;
    public CharacterMultipliersSet Level2MultiplierSet;
    public CharacterMultipliersSet Level3MultiplierSet;

    // Start is called before the first frame update
    void Start()
    {
        JokeCount = 0;
        GameOver = false;
        StartLevel(1);
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
            // Queen is Angry - next day she'll be in the executioner place
        }

        if (Audience.GetAngryMeter() <= 0)
        {
            // Audience is Angry - not sure what to do ?
        }
    }

    public void SubmitJoke(Noun subject, Verb verb, Noun jokeObject)
    {
        if (GameOver)
        {
            return;
        }
        Debug.LogFormat("{0} {1} {2}", subject.ToString(), verb.ToString(), jokeObject.ToString());

        switch (Level)
        {
            case 1:
                if(verb == Verb.Farts)
                {
                    Character characterSubject = GetCharacterWithNoun(subject);
                    Character characterObject = GetCharacterWithNoun(jokeObject);
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 3);
                    if(characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Embarrassed, 3);

                    // Audience laughs at Fart
                    Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                    // King likes farts
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Happy);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Neutral);
                    // Heir is not ammused
                    if(characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner likes farts
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Happy);
                }
                if (verb == Verb.Is)
                {
                    if(jokeObject == Noun.Pig)
                    {
                        GetCharacterWithNoun(subject).AddEmotionReaction(EEmotionType.Angry, 3);
                        Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                    }
                    else
                    {
                        // Bad Joke
                        King.AddEmotionReaction(EEmotionType.Neutral);
                        Queen.AddEmotionReaction(EEmotionType.Neutral);
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                        Audience.AddEmotionReaction(EEmotionType.Angry, 2);
                    }
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

        TellJoke();
    }

    public void SubmitJoke(Noun subject, Adjective adjective)
    {
        if (GameOver)
        {
            return;
        }
        Debug.LogFormat("{0} is {1}", subject.ToString(), adjective.ToString());

        switch(Level)
        {
            case 1:
                Character characterSubject = GetCharacterWithNoun(subject);
                if (adjective == Adjective.Ugly)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Angry, 2);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                }
                if (adjective == Adjective.Funny)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 3);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                // King is not ammused
                if (characterSubject != King)
                    King.AddEmotionReaction(EEmotionType.Neutral);
                // Queen is not ammused
                if (characterSubject != Queen)
                    Queen.AddEmotionReaction(EEmotionType.Neutral);
                // Heir is not ammused
                if (characterSubject != Heir)
                    Heir.AddEmotionReaction(EEmotionType.Neutral);
                // Executioner is not ammused
                if (characterSubject != Executioner)
                    Executioner.AddEmotionReaction(EEmotionType.Neutral);
                break;
            case 2:
                break; 
            case 3:
                break;
            default: 
                break;
        }
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
    void StartLevel(int level)
    {
        Level = level;
        //Set Multipliers
        switch (Level)
        {
            case 1:
                SetMultipliersForCharacters(Level1MultiplierSet);
                break;
            case 2:
                SetMultipliersForCharacters(Level2MultiplierSet);
                break;
            case 3:
                SetMultipliersForCharacters(Level3MultiplierSet);
                break;
            default:
                break;
        }
    }

    void SetMultipliersForCharacters(CharacterMultipliersSet multiplierSet)
    {
        King.SetMultipliers(multiplierSet.KingMultiplier);
        Queen.SetMultipliers(multiplierSet.QueenMultiplier);
        Heir.SetMultipliers(multiplierSet.HeirMultiplier);
        Audience.SetMultipliers(multiplierSet.AudienceMultiplier);
        Executioner.SetMultipliers(multiplierSet.ExecutionerMultiplier);
    }

    Character GetCharacterWithNoun(Noun characterNoun)
    {
        switch (characterNoun)
        {
            case Noun.Queen:
                return Queen;
            case Noun.King:
                return King;
            case Noun.Audience:
                return Audience;
            case Noun.Heir:
                return Heir;
            case Noun.Executioner:
                return Executioner;
            case Noun.Jester: 
                return Jester;
            default:
                break;
        }
        return null;
    }
}
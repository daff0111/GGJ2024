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
        None,
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
        None,
        Is,
        Farts,
        Loves,
        Hates,
        Kills,
        Serves,
    }

    public enum Adjective
    {
        None,
        Ugly,
        Funny,
        Fat,
        Poor,
        Stupid,
        Small,
        Big,
        Pig,
    }

    public UIManager UIManagerObject;
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

    void TellJoke()
    {
        JokeCount++;
        
        Executioner.PrepareKill(King.GetAngryMeter() <= 20 || Executioner.GetAngryMeter() <= 15);

        if (Heir.GetAngryMeter() <= 0)
        {
            // Heir is Angry, King gets angry too
            King.AddAngryMeter(-30);
        }

        if (King.GetAngryMeter() <= 0) 
        {
            // You Died - King Orders your death
            KillJester();

        }

        if (Executioner.GetAngryMeter() <= 0)
        {
            // You Died - The Executioner is angry and he kills you
            Executioner.RevealAngryFace();
            KillJester();
        }

        if(Queen.GetAngryMeter() <= 0)
        {
            // You Died - Queen Orders your death. Maybe do something different?
            KillJester();
        }

        if (Audience.GetAngryMeter() <= 0)
        {
            // Audience is Angry - not sure what to do ?
        }
        // You are dead
        if (GameOver)
            return;

        StartNextRound();

        if (JokeCount == 3)
        {
            StartLevel(2);
        }
        if (JokeCount == 6)
        {
            StartLevel(3);
        }
        if (JokeCount == 9)
        {
            GameOver = true;
            Debug.Log("YOU WON - You told " + JokeCount + " Jokes. Press 'R' to Restart");
            UIManagerObject.HandleGameOver(false, JokeCount);
        }
    }

    private void StartNextRound()
    {
        UIManagerObject.InitializeRound(JokeCount % 3 + 1);
    }

    public void SubmitJoke(Noun subject, Verb verb, Noun jokeObject)
    {
        if (GameOver)
        {
            return;
        }
        Debug.LogFormat("{0} {1} {2}", subject.ToString(), verb.ToString(), jokeObject.ToString());

        Character characterSubject = GetCharacterWithNoun(subject);
        Character characterObject = GetCharacterWithNoun(jokeObject);
        if (characterSubject == null)
        {
            // Bad Joke
            King.AddEmotionReaction(EEmotionType.Angry);
            Queen.AddEmotionReaction(EEmotionType.Neutral);
            Heir.AddEmotionReaction(EEmotionType.Neutral);
            Executioner.AddEmotionReaction(EEmotionType.Neutral);
            Audience.AddEmotionReaction(EEmotionType.Angry, 2);
            TellJoke();
            return;
        }

        switch (Level)
        {
            case 1:
                if(verb == Verb.Farts)
                {
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
                        Queen.AddEmotionReaction(EEmotionType.Embarrassed);
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
                        characterSubject.AddEmotionReaction(EEmotionType.Angry, 3);
                        Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                        if (characterSubject != King)
                            King.AddEmotionReaction(EEmotionType.Happy);
                        if (characterSubject != Queen)
                            Queen.AddEmotionReaction(EEmotionType.Embarrassed);
                        if (characterSubject != Heir)
                            Heir.AddEmotionReaction(EEmotionType.Happy);
                        if (characterSubject != Executioner)
                            Executioner.AddEmotionReaction(EEmotionType.Happy);
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
                if (verb == Verb.Hates)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Embarrassed, 1);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Angry, 1);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Neutral);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                if (verb == Verb.Loves)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Neutral);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Embarrassed, 2);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is Happy
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Happy);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                break;
            case 2:
                if (verb == Verb.Farts)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 3);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Embarrassed, 3);

                    // Audience laughs at Fart
                    Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                    // King likes farts
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Happy);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Embarrassed);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner likes farts
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Happy);
                }
                if (verb == Verb.Hates)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Embarrassed, 1);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Angry, 1);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Angry);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                if (verb == Verb.Loves)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Neutral);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Embarrassed, 2);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Happy);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                if (verb == Verb.Is)
                {
                    if (jokeObject == Noun.Pig)
                    {
                        characterSubject.AddEmotionReaction(EEmotionType.Angry, 3);
                        Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                        if (characterSubject != King)
                            King.AddEmotionReaction(EEmotionType.Happy);
                        if (characterSubject != Queen)
                            Queen.AddEmotionReaction(EEmotionType.Embarrassed);
                        if (characterSubject != Heir)
                            Heir.AddEmotionReaction(EEmotionType.Happy);
                        if (characterSubject != Executioner)
                            Executioner.AddEmotionReaction(EEmotionType.Happy);
                    }
                    else
                    {
                        // Bad Joke
                        King.AddEmotionReaction(EEmotionType.Angry);
                        Queen.AddEmotionReaction(EEmotionType.Neutral);
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                        Audience.AddEmotionReaction(EEmotionType.Angry, 2);
                    }
                }
                break;
            case 3:
                if (verb == Verb.Farts)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 3);
                    if (characterObject != null)
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
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner likes farts
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Happy);
                }
                if (verb == Verb.Hates)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Embarrassed, 1);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Angry, 1);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Angry);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                if (verb == Verb.Loves)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Neutral);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Embarrassed, 2);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Happy);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                if (verb == Verb.Kills)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Neutral);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Crying, 2);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Crying);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                    // Executioner is embarrassed
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Happy);
                }
                if (verb == Verb.Serves)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Embarrassed, 3);
                    if (characterObject != null)
                        characterObject.AddEmotionReaction(EEmotionType.Neutral);

                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                    // King is not ammused
                    if (characterSubject != King && characterObject != King)
                        King.AddEmotionReaction(EEmotionType.Neutral);
                    // Queen is not ammused
                    if (characterSubject != Queen && characterObject != Queen)
                        Queen.AddEmotionReaction(EEmotionType.Neutral);
                    // Heir is not ammused
                    if (characterSubject != Heir && characterObject != Heir)
                        Heir.AddEmotionReaction(EEmotionType.Happy);
                    // Executioner is not ammused
                    if (characterSubject != Executioner && characterObject != Executioner)
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                }
                if (verb == Verb.Is)
                {
                    if (jokeObject == Noun.Pig)
                    {
                        characterSubject.AddEmotionReaction(EEmotionType.Embarrassed, 1);
                        Audience.AddEmotionReaction(EEmotionType.Happy, 1);
                        if (characterSubject != King)
                            King.AddEmotionReaction(EEmotionType.Angry);
                        if (characterSubject != Queen)
                            Queen.AddEmotionReaction(EEmotionType.Happy);
                        if (characterSubject != Heir)
                            Heir.AddEmotionReaction(EEmotionType.Happy);
                        if (characterSubject != Executioner)
                            Executioner.AddEmotionReaction(EEmotionType.Happy);
                    }
                    else
                    {
                        // Bad Joke
                        King.AddEmotionReaction(EEmotionType.Neutral);
                        Queen.AddEmotionReaction(EEmotionType.Neutral);
                        Heir.AddEmotionReaction(EEmotionType.Neutral);
                        Executioner.AddEmotionReaction(EEmotionType.Neutral);
                        Audience.AddEmotionReaction(EEmotionType.Angry, 3);
                    }
                }
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
        Character characterSubject = GetCharacterWithNoun(subject);
        if (characterSubject == null)
        {
            // Bad Joke
            King.AddEmotionReaction(EEmotionType.Angry);
            Queen.AddEmotionReaction(EEmotionType.Neutral);
            Heir.AddEmotionReaction(EEmotionType.Neutral);
            Executioner.AddEmotionReaction(EEmotionType.Neutral);
            Audience.AddEmotionReaction(EEmotionType.Angry, 2);
            TellJoke();
            return;
        }
        switch (Level)
        {
            case 1:
                if (adjective == Adjective.Ugly)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Angry, 2);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                }
                if (adjective == Adjective.Funny)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 3);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 1);
                }
                // King is not ammused
                if (characterSubject != King)
                    King.AddEmotionReaction(EEmotionType.Happy);
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
                if (adjective == Adjective.Fat)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Crying, 2);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                if (adjective == Adjective.Poor)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Crying, 1);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                if (adjective == Adjective.Stupid)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Crying, 2);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                // King is not ammused
                if (characterSubject != King)
                    King.AddEmotionReaction(EEmotionType.Neutral);
                // Queen is not ammused
                if (characterSubject != Queen)
                    Queen.AddEmotionReaction(EEmotionType.Happy);
                // Heir is not ammused
                if (characterSubject != Heir)
                    Heir.AddEmotionReaction(EEmotionType.Neutral);
                // Executioner is not ammused
                if (characterSubject != Executioner)
                    Executioner.AddEmotionReaction(EEmotionType.Neutral);
                break; 
            case 3:
                if (adjective == Adjective.Ugly)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Angry, 3);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 3);
                }
                if (adjective == Adjective.Funny)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 3);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 1);
                }
                if (adjective == Adjective.Fat)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Crying, 2);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                if (adjective == Adjective.Poor)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Crying, 1);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                if (adjective == Adjective.Stupid)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Crying, 2);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                if (adjective == Adjective.Small)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Embarrassed, 3);
                    Audience.AddEmotionReaction(EEmotionType.Happy, 2);
                }
                if (adjective == Adjective.Big)
                {
                    characterSubject.AddEmotionReaction(EEmotionType.Happy, 2);
                    Audience.AddEmotionReaction(EEmotionType.Neutral);
                }
                // King is not ammused
                if (characterSubject != King)
                    King.AddEmotionReaction(EEmotionType.Neutral);
                // Queen is not ammused
                if (characterSubject != Queen)
                    Queen.AddEmotionReaction(EEmotionType.Neutral);
                // Heir is not ammused
                if (characterSubject != Heir)
                    Heir.AddEmotionReaction(EEmotionType.Happy);
                // Executioner is not ammused
                if (characterSubject != Executioner)
                    Executioner.AddEmotionReaction(EEmotionType.Neutral);
                break;
            default: 
                break;
        }

        TellJoke();
    }

    void KillJester()
    {
        StartCoroutine(Executioner.KillJester());
        GameOver = true;
        Debug.Log("YOU DIED - You told " + JokeCount + " Jokes. Press 'R' to Restart");
        UIManagerObject.HandleGameOver(true, JokeCount);
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
                Heir.AddEmotionReaction(EEmotionType.Crying); // Bunny Died
                break;
            case 3:
                SetMultipliersForCharacters(Level3MultiplierSet);
                break;
            default:
                break;
        }
        UIManagerObject.StartLevel(level);
    }

    void SetMultipliersForCharacters(CharacterMultipliersSet multiplierSet)
    {
        const float recoveryMeter = 5;
        King.SetMultipliers(multiplierSet.KingMultiplier);
        King.AddAngryMeter(recoveryMeter);
        Queen.SetMultipliers(multiplierSet.QueenMultiplier);
        Queen.AddAngryMeter(recoveryMeter);
        Heir.SetMultipliers(multiplierSet.HeirMultiplier);
        Heir.AddAngryMeter(recoveryMeter);
        Audience.SetMultipliers(multiplierSet.AudienceMultiplier);
        Audience.AddAngryMeter(recoveryMeter);
        Executioner.SetMultipliers(multiplierSet.ExecutionerMultiplier);
        Executioner.AddAngryMeter(recoveryMeter);
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
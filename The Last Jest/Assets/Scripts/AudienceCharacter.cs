using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceCharacter : Character
{
    public Character[] audienceCharacters;

    protected override void Start()
    {
        CharacterType = ECharacterType.Audience;
        base.Start();
        foreach (Character aCharacter in audienceCharacters)
        {
            aCharacter.StartingAngryMeter = StartingAngryMeter;
            aCharacter.CharacterType = ECharacterType.Audience;
        }
    }

    public override void AddEmotionReaction(EEmotionType emotionReaction, float multiplier = 1)
    {
        base.AddEmotionReaction(emotionReaction, multiplier);
        foreach (Character aCharacter in audienceCharacters)
        {
            aCharacter.SetAngryMeter(GetAngryMeter());
        }
        // Add Sound Effects
        switch (emotionReaction)
        {
            case EEmotionType.Happy:
                break;
            case EEmotionType.Neutral:
                //FaceMesh.material = MaterialNeutralFace;
                break;
            case EEmotionType.Crying:
                //FaceMesh.material = MaterialCryingFace;
                break;
            case EEmotionType.Embarrassed:
                //FaceMesh.material = MaterialEmbarrassedFace;
                break;
            case EEmotionType.Angry:
                break;
            default:
                break;
        }
    }
}

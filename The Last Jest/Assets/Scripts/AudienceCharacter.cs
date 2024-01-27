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
        }
    }

    public override void AddEmotionReaction(EEmotionType emotionReaction, float multiplier = 1)
    {
        Debug.Log("Audience " + emotionReaction.ToString());
        AddAngryMeter(GetReactionMeter(emotionReaction) * multiplier);
        switch (emotionReaction)
        {
            case EEmotionType.Happy:
                //FaceMesh.material = MaterialHappyFace;
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
                //FaceMesh.material = MaterialAngryFace;
                break;
            default:
                break;
        }
        foreach (Character aCharacter in audienceCharacters)
        {
            aCharacter.SetAngryMeter(GetAngryMeter());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudienceCharacter : Character
{
    public Character[] audienceCharacters;
    [Header("Audio Effects")]
    public StudioEventEmitter AngryAudienceSound;
    public StudioEventEmitter HappyAudienceSound;
    public StudioEventEmitter ClappingAudienceSound;

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
        if(AngryAudienceSound)
            AngryAudienceSound.Stop();
        if(HappyAudienceSound)
            HappyAudienceSound.Stop();
        if(ClappingAudienceSound)
            ClappingAudienceSound.Stop();
        // Add Sound Effects
        switch (emotionReaction)
        {
            case EEmotionType.Happy:
                if(HappyAudienceSound)
                    HappyAudienceSound.Play();
                break;
            case EEmotionType.Neutral:
                if(ClappingAudienceSound)
                    ClappingAudienceSound.Play();
                break;
            case EEmotionType.Crying:
                if(AngryAudienceSound)
                    AngryAudienceSound.Play();
                break;
            case EEmotionType.Embarrassed:
                if(ClappingAudienceSound)
                    ClappingAudienceSound.Play();
                break;
            case EEmotionType.Angry:
                if(AngryAudienceSound)
                    AngryAudienceSound.Play();
                break;
            default:
                break;
        }
    }
}

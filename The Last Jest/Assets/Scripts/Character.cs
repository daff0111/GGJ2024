using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public enum EEmotionType
{
    Happy,
    Neutral,
    Embarrassed,
    Crying,
    Angry
};

[System.Serializable]
public struct CharacterMultiplier
{
    public float HappyMultiplier;
    public float EmbarrassedMultiplier;
    public float CryingMultiplier;
    public float AngryMultiplier;
}

public class Character : MonoBehaviour
{
    public enum ECharacterType 
    {
        None = 0,
        King,
        Queen,
        Heir,
        Jester,
        Executioner,
        Audience
    };

    public enum ECharacterState
    {
        Neutral,
        Happy,
        Angry
    };

    public ECharacterType CharacterType;
    public float StartingAngryMeter = 50;

    SkinnedMeshModifier headModifier;
    [Header("Effects")]
    public VisualEffect AngryEffect;
    public VisualEffect AngryEffect1;
    public VisualEffect HappyEffect;

    protected float AngryMeter = 50;
    protected CharacterMultiplier Multipliers;

    // TEST TEXT
    protected TMP_Text CharacterText;

    private void Awake()
    {
        headModifier = GetComponentInChildren<SkinnedMeshModifier>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetAngryMeter(StartingAngryMeter);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetAngryMeter()
    { 
        return AngryMeter; 
    }

    public void SetAngryMeter(float newValue)
    { 
        AngryMeter = newValue;

        if (headModifier)
        {
            headModifier.SetSliderValue((((StartingAngryMeter - newValue) / StartingAngryMeter) * 180) - 50); //Magic number to match Happyness on the face
        }
    }

    public void AddAngryMeter(float newValue)
    {
        float angryness = AngryMeter + newValue;
        if(angryness > StartingAngryMeter)
        {
            angryness = StartingAngryMeter;
        }
        else if(angryness < 0)
        {
            angryness = 0;
        }

        SetAngryMeter(angryness);
    }

    bool IsRoyalCharacter()
    {
        return CharacterType != ECharacterType.Jester && CharacterType != ECharacterType.None;
    }

    void UpdateCharacterText()
    {
        CharacterText.text = CharacterType.ToString() + "\n" + AngryMeter;
        if (AngryMeter <= 25)
        {
            CharacterText.color = new Color(255, 0, 0, 255);
        }
        else if (AngryMeter <= 60)
        {
            CharacterText.color = new Color(255, 255, 0, 255);
        }
        else
        {
            CharacterText.color = new Color(0, 255, 0, 255);
        }
    }

    public virtual void AddEmotionReaction(EEmotionType emotionReaction, float multiplier = 1)
    {
        AddAngryMeter(GetReactionMeter(emotionReaction) * multiplier);
        if (AngryEffect != null)
            AngryEffect.enabled = false;
        if(AngryEffect1 != null)
            AngryEffect1.enabled = false;
        if (HappyEffect != null)
            HappyEffect.enabled = false;

        if (!IsRoyalCharacter())
            return;

        switch (emotionReaction) 
        {
            case EEmotionType.Happy:
                if (HappyEffect)
                    HappyEffect.enabled = true;
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
                if (AngryEffect)
                    AngryEffect.enabled = true;
                if (AngryEffect1)
                    AngryEffect1.enabled = true;
                break;
            default:
                break;
        }
    }

    protected float GetReactionMeter(EEmotionType emotionReaction)
    {
        // Map this values somehow depending on Character and Day?
        switch (emotionReaction)
        {
            case EEmotionType.Happy:
                return 10 * Multipliers.HappyMultiplier;
            case EEmotionType.Neutral:
                return -5;
            case EEmotionType.Embarrassed:
                return -10 * Multipliers.EmbarrassedMultiplier;
            case EEmotionType.Crying:
                return -10 * Multipliers.CryingMultiplier;
            case EEmotionType.Angry:
                return -10 * Multipliers.AngryMultiplier;
            default:
                break;
        }
        return 0;
    }

    public void SetMultipliers(CharacterMultiplier newMultiplier)
    {
        Multipliers = newMultiplier;
    }
}
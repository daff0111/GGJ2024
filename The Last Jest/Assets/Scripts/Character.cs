using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("Emotion Materials")]
    public Material MaterialHappyFace;
    public Material MaterialNeutralFace;
    public Material MaterialCryingFace;
    public Material MaterialEmbarrassedFace;
    public Material MaterialAngryFace;
    [Header("Child Objects")]
    public GameObject FaceObject;
    public GameObject TextObject;

    SkinnedMeshModifier headModifier;

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
        CharacterText = TextObject.GetComponent<TMP_Text>();
        
        SetAngryMeter(StartingAngryMeter);
        /*if (IsRoyalCharacter())
            FaceMesh.material = MaterialNeutralFace;*/

        // TEST TEXT
        CharacterText.gameObject.GetComponent<MeshRenderer>().enabled = IsRoyalCharacter();
        CharacterText.text = CharacterType.ToString() + "\n" + AngryMeter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetAngryMeter()
    { 
        return AngryMeter; 
    }

    void SetAngryMeter(float newValue)
    { 
        AngryMeter = newValue;

        headModifier.SetSliderValue(newValue);

        if (!IsRoyalCharacter())
            return;

        // TEST TEXT
        UpdateCharacterText();
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
        return CharacterType != ECharacterType.Jester && CharacterType != ECharacterType.None && CharacterType != ECharacterType.Executioner;
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

    public virtual void AddEmotionReaction(EEmotionType emotionReaction)
    {
        AddAngryMeter(GetReactionMeter(emotionReaction));
        if (!IsRoyalCharacter())
            return;
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
    }

    protected float GetReactionMeter(EEmotionType emotionReaction)
    {
        // Map this values somehow depending on Character and Day?
        switch (emotionReaction)
        {
            case EEmotionType.Happy:
                return 5 * Multipliers.HappyMultiplier;
            case EEmotionType.Neutral:
                return 0;
            case EEmotionType.Embarrassed:
                return -5 * Multipliers.EmbarrassedMultiplier;
            case EEmotionType.Crying:
                return -5 * Multipliers.CryingMultiplier;
            case EEmotionType.Angry:
                return -5 * Multipliers.AngryMultiplier;
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
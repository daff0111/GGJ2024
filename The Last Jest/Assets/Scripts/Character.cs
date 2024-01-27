using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public enum ECharacterType 
    {
        None = 0,
        King,
        Queen,
        Princess,
        Jester,
        Executioner
    };

    public enum ECharacterState
    {
        Neutral,
        Happy,
        Angry
    };

    public ECharacterType CharacterType;
    public Material MaterialHappyFace;
    public Material MaterialNeutralFace;
    public Material MaterialAngryFace;
    public GameObject FaceObject;
    public GameObject TextObject;

    private MeshRenderer FaceMesh;
    private float AngryMeter = 50;

    // TEST TEXT
    private TMP_Text CharacterText;

    // Start is called before the first frame update
    void Start()
    {
        FaceMesh = FaceObject.GetComponent<MeshRenderer>();
        CharacterText = TextObject.GetComponent<TMP_Text>();
        
        if(IsRoyalCharacter())
            SetAngryMeter(50);

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

        if (!IsRoyalCharacter())
            return;

        if(AngryMeter <= 25)
        {
            FaceMesh.material = MaterialAngryFace;
        }
        else if (AngryMeter <= 60)
        {
            FaceMesh.material = MaterialNeutralFace;
            
        }
        else
        {
            FaceMesh.material = MaterialHappyFace;
        }
        // TEST TEXT
        UpdateCharacterText();
    }

    public void AddAngryMeter(float newValue)
    {
        float angryness = AngryMeter + newValue;
        if(angryness > 100)
        {
            angryness = 100;
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

    void AddReaction()
    {

    }
}
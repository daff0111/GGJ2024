using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="IconContainer", menuName ="IconContainer")]
[Serializable]
public class IconContainer : ScriptableObject
{
    [SerializeField]
    public List<NounSpritePair> NounSprites;
    [SerializeField]
    public List<VerbSpritePair> VerbSprites;
    [SerializeField]
    public List<AdjectiveSpritePair> AdjectiveSprites;
}

[Serializable]
public struct NounSpritePair
{
    public JokeManager.Noun Noun;
    public Sprite Sprite;
    public int UnlockLevel;
}

[Serializable]
public struct VerbSpritePair
{
    public JokeManager.Verb Verb;
    public Sprite Sprite;
    public int UnlockLevel;
}

[Serializable]
public struct AdjectiveSpritePair
{
    public JokeManager.Adjective Adejctive;
    public Sprite Sprite;
    public int UnlockLevel;
}
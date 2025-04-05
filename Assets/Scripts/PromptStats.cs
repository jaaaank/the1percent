using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prompt Stats", menuName = "Prompt Stats")]

public class PromptStats : ScriptableObject
{
    public bool hasThirdOption;

    [TextArea(3,10)]
    public string promptDescription;

    [TextArea(3,4)]
    public string wealthyAdvice;
    [TextArea(3, 4)]
    public string commonAdvice;

    public string yesText;
    public string midText;
    public string noText;

    public float yesMoney;
    public float midMoney;
    public float noMoney;

    [TextArea(3, 5)]
    public string yesResponse;
    [TextArea(3, 5)]
    public string midResponse;
    [TextArea(3, 5)]
    public string noResponse;

    public SerializedDictionary<string, SerializedDictionary<string, float>> yesCountyEffects;
    public SerializedDictionary<string, SerializedDictionary<string, float>> midCountyEffects;
    public SerializedDictionary<string, SerializedDictionary<string, float>> noCountyEffects;
}

using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prompt Stats", menuName = "Prompt Stats")]

public class PromptStats : ScriptableObject
{
    [TextArea(3,10)]
    public string promptDescription;

    public string yesText;
    public string noText;

    [TextArea(3, 10)]
    public string yesResponse;
    [TextArea(3, 10)]
    public string noResponse;

    public SerializedDictionary<string, float> yesStats;
    public SerializedDictionary<string, float> noStats;  
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    public Button yes;
    public Button no;

    public GameManager manager;
    public Dictionary<string, float> yesEffects;
    public Dictionary<string, float> noEffects;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        yesEffects.Add("money", 0);
        yesEffects.Add("upperPop", 0);
        yesEffects.Add("middlePop", 0);
        yesEffects.Add("lowerPop", 0);
        yesEffects.Add("upperApp", 0);
        yesEffects.Add("middlelowerApp", 0);
        yesEffects.Add("lowerApp", 0);
        
        noEffects.Add("money", 0);
        noEffects.Add("upperPop", 0);
        noEffects.Add("middlePop", 0);
        noEffects.Add("lowerPop", 0);
        noEffects.Add("upperApp", 0);
        noEffects.Add("middlelowerApp", 0);
        noEffects.Add("lowerApp", 0);
    }

    public void buttonPressed(bool choice)
    {

    }
}

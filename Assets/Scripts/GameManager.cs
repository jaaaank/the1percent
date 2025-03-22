using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public float money;

    //each range from 0-100
    float upperApp = 50f;
    float middleApp = 50f;
    float lowerApp = 50f;

    //range from 0-100, all 3 should always add up to 100
    float upperPop = 19f;
    float middlePop = 51f;
    float lowerPop = 30f;

    public TextMeshProUGUI scores;
    public TextMeshProUGUI pops;

    void Start()
    {
        scores.text = "Lower Class Approval: " + lowerApp + "%\nMiddle Class Approval: " + middleApp + "%\nUpper Class Approval: " + upperApp + "%";
        pops.text = "Lower Class Population: " + lowerPop + "%\nMiddle Class Population: " + middlePop + "%\nUpper Class Population: " + upperPop + "%" ;
    }

    public void updateScores(SerializedDictionary<string, float> stats)
    {
        money += stats["money"];

        upperApp += stats["upperApp"];
        middleApp += stats["middleApp"];
        lowerApp += stats["lowerApp"];

        upperPop += stats["upperPop"];
        middlePop += stats["middlePop"];
        lowerPop += stats["lowerPop"];

        scores.text = "Lower Class Approval: " + lowerApp + "%\nMiddle Class Approval: " + middleApp + "%\nUpper Class Approval: " + upperApp + "%";
        pops.text = "Lower Class Population: " + lowerPop + "%\nMiddle Class Population: " + middlePop + "%\nUpper Class Population: " + upperPop + "%";
    }
}

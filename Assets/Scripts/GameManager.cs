using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public float money;

    //each range from 0-1
    float upperApproval = .5f;
    float middleApproval = .5f;
    float lowerApproval = .5f;

    //range from 0-1, all 3 should always add up to 1
    float upperPop = .19f;
    float middlePop = .51f;
    float lowerPop = .3f;

    public TextMeshProUGUI scores;
    public TextMeshProUGUI pops;


    // Start is called before the first frame update
    void Start()
    {
        scores.text = "Lower Class Approval: " + lowerApproval*100 + "%\nMiddle Class Approval: " + middleApproval*100 + "%\nUpper Class Approval: " + upperApproval * 100 + "%";
        pops.text = "Lower Class Population: " + lowerPop*100 + "%\nMiddle Class Population: " + middlePop*100 + "%\nUpper Class Population: " + upperPop*100 + "%" ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPressed(bool choice)
    {
        if (choice) {
            upperApproval += .1f;
            lowerApproval -= .1f;
        }
    
        if (!choice) {
            upperApproval -= .05f;
            lowerApproval += .10f;
        }
        scores.text = "Lower Class Approval: " + lowerApproval * 100 + "%\nMiddle Class Approval: "+ middleApproval*100+"%\nUpper Class Approval: " + upperApproval * 100 + "%";
    }
}

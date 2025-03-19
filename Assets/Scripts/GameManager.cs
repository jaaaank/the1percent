using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public float money;
    public string playerName;
    public float upperClassApproval;
    public float lowerClassApproval;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPressed(bool choice)
    {
        if (choice) {
            upperClassApproval += 1;
            lowerClassApproval -= 1;
        }

        if (!choice) {
            lowerClassApproval -= 1;
            lowerClassApproval += 1;
        }
        print("Lower Class Approval: " + lowerClassApproval);
        print("Upper Class Approval: " + upperClassApproval);
    }
}

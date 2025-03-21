using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prompt Stats", menuName = "Prompt Stats")]

public class PromptStats : ScriptableObject
{
   public float yesMoney;
   public float noMoney;

   public float yesLowerPop;
   public float yesMiddlePop;
   public float yesUpperPop;
   public float yesLowerApp;
   public float yesMiddleApp;
   public float yesUpperApp;
     
   public float noLowerPop;
   public float noMiddlePop;
   public float noUpperPop;
   public float noLowerApp;
   public float noMiddleApp;
   public float noUpperApp;
    
}

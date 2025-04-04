using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PromptCanvas : MonoBehaviour
{
    public float money = 100;

    public List<PromptStats> prompts;

    public PromptStats currentPrompt;

    public TextMeshProUGUI description;
    public TextMeshProUGUI advice;
    public TextMeshProUGUI natHapiness;

    string randPoor;
    string randRich;

    public GameObject Map;
    public GameObject AdRoom;

    [Header("Buttons")]
    public Button yButton;
    public Button mButton;
    public Button nButton;
    public Button aButton;
    public Button eaButton;
    public Button richButton;
    public Button poorButton;

    [Header("Counties")]
    public GameObject counties;
    public float nationalHappiness;

    private void Start()
    {
        gameObject.SetActive(false);
        eaButton.gameObject.SetActive(false);
        AdRoom.SetActive(false);
        getPrompt();
    }

    public void getPrompt()
    {
        gameObject.SetActive(true);
        int choice = Random.Range(0, prompts.Count);
        currentPrompt = prompts[choice];
        //this VV is for making sure prompts don't repeat
        //prompts.RemoveAt(choice);

        if (currentPrompt.hasThirdOption)
        {
            mButton.gameObject.SetActive(true);
            mButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.midText;
            yButton.gameObject.transform.localPosition = new Vector3(-500, 25, 0);
            nButton.gameObject.transform.localPosition = new Vector3(500, 25, 0);

        } 
        else
        {
            mButton.gameObject.SetActive(false);
            yButton.gameObject.transform.localPosition = new Vector3(-300, 25, 0);
            nButton.gameObject.transform.localPosition = new Vector3(300, 25, 0);
        }

        yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText;
        nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText;

        description.text = currentPrompt.promptDescription;

        //The following three if statements are quite possible the most disgusting fucking code I've ever written. I hate this engine so much.

        if (currentPrompt.promptDescription.Contains("PoorCounty"))
        {
            //EVENTUALLY replace this VV with an actual random pick of poor counties
            randPoor = "Loid";
            description.text = currentPrompt.promptDescription.Replace("PoorCounty", randPoor);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText.Replace("PoorCounty", randPoor);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText.Replace("PoorCounty", randPoor);
        }
        if (currentPrompt.promptDescription.Contains("RichCounty"))
        {
            //EVENTUALLY replace this VV also with an actual random pick of poor counties
            randRich = "Resare";
            description.text = currentPrompt.promptDescription.Replace("RichCounty", randRich);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText.Replace("RichCounty", randRich);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText.Replace("RichCounty", randRich);
        }
        if (currentPrompt.promptDescription.Contains("RichCounty")&&(currentPrompt.promptDescription.Contains("PoorCounty")))
        {
            //EVENTUALLY replace this VV also with an actual random pick of poor counties
            randRich = "Resare";
            description.text = currentPrompt.promptDescription.Replace("RichCounty", randRich);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText.Replace("RichCounty", randRich);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText.Replace("RichCounty", randRich);
            //EVENTUALLY replace this VV with an actual random pick of poor counties
            randPoor = "Loid";
            description.text = description.text.Replace("PoorCounty", randPoor);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text.Replace("PoorCounty", randPoor);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text.Replace("PoorCounty", randPoor);
        }



        //Cleaned up this VV code a little so it just loops through all the counties and we don't have to count them up individually
        for (int i = 0; i < counties.transform.childCount-1; i++)
        {
            nationalHappiness += counties.transform.GetChild(i).gameObject.GetComponent<County>().hapiness;
        }
        nationalHappiness /= counties.transform.childCount;
        //nationalHappiness = (county1.GetComponent<County>().hapiness + county2.GetComponent<County>().hapiness + county3.GetComponent<County>().hapiness) / numCounties;
        natHapiness.text = "National Approval: " + nationalHappiness + "%";

    }

    public void yesPressed()
    {
        money += currentPrompt.yesMoney;
        updateCounties(currentPrompt.yesCountyEffects);
        getPrompt();
    }

    public void midPressed()
    {
        money += currentPrompt.midMoney;
        updateCounties(currentPrompt.midCountyEffects);
        getPrompt();
    }

    public void noPressed() 
    {
        money += currentPrompt.noMoney;
        updateCounties(currentPrompt.noCountyEffects);
        getPrompt();
    }

    public void updateCounties(SerializedDictionary<string, SerializedDictionary<string, float>> stats)
    {
        if (currentPrompt.promptDescription.Contains("PoorCounty"))
        {
            GameObject.Find(randPoor).GetComponent<County>().food += stats["PoorCounty"]["food"];
            GameObject.Find(randPoor).GetComponent<County>().meds += stats["PoorCounty"]["meds"];
            GameObject.Find(randPoor).GetComponent<County>().wealth += stats["PoorCounty"]["wealth"];
            GameObject.Find(randPoor).GetComponent<County>().updateHapiness();
        }
        if (currentPrompt.promptDescription.Contains("RichCounty"))
        {
            GameObject.Find(randRich).GetComponent<County>().food += stats["RichCounty"]["food"];
            GameObject.Find(randRich).GetComponent<County>().meds += stats["RichCounty"]["meds"];
            GameObject.Find(randRich).GetComponent<County>().wealth += stats["RichCounty"]["wealth"];
            GameObject.Find(randRich).GetComponent<County>().updateHapiness();
        }

        foreach (var county in stats.Keys) 
        {
            if (county != "PoorCounty" && county != "RichCounty")
            {
                print(county);
                print(stats[county]["food"]);
                print(stats[county]["meds"]);
                print(stats[county]["wealth"]);
                GameObject.Find(county).GetComponent<County>().food += stats[county]["food"];
                GameObject.Find(county).GetComponent<County>().meds += stats[county]["meds"];
                GameObject.Find(county).GetComponent<County>().wealth += stats[county]["wealth"];
                GameObject.Find(county).GetComponent<County>().updateHapiness();
            }
        }
    }

    public void AdvisorsScreen()
    {
        //Main screen buttons
        eaButton.gameObject.SetActive(true);
        aButton.gameObject.SetActive(false);
        yButton.gameObject.SetActive(false);
        nButton.gameObject.SetActive(false);
        mButton.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        //Advisors
        poorButton.gameObject.SetActive(true);
        richButton.gameObject.SetActive(true);
        //counties
        counties.SetActive(false);
        //background
        AdRoom.SetActive(true);
        Map.SetActive(false);
    }

    public void ExitAdvisors()
    {
        eaButton.gameObject.SetActive(false);
        aButton.gameObject.SetActive(true);
        poorButton.gameObject.SetActive(false);
        richButton.gameObject.SetActive(false);
        if (currentPrompt.hasThirdOption)
        {
            mButton.gameObject.SetActive(true);
        }
        yButton.gameObject.SetActive(true);
        nButton.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        counties.SetActive(true);
        AdRoom.SetActive(false);
        Map.SetActive(true);
        advice.gameObject.SetActive(false);
    }

    public void RichAdvice()
    {
        advice.gameObject.SetActive(true);
        advice.text = currentPrompt.wealthyAdvice;
    }

    public void PoorAdvice()
    {
        advice.gameObject.SetActive(true);
        advice.text = currentPrompt.commonAdvice;
    }

}

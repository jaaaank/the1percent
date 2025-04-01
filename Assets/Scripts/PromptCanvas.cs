using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptCanvas : MonoBehaviour
{
    public float money = 100;

    public List<PromptStats> prompts;

    public PromptStats currentPrompt;

    public TextMeshProUGUI description;
    public TextMeshProUGUI advice;
    public TextMeshProUGUI natHapiness;

    int choice;

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
    public GameObject county1;
    public GameObject county2;
    public GameObject county3;
    public float numCounties;
    public float nationalHappiness;

    private void Start()
    {
        gameObject.SetActive(false);
        eaButton.gameObject.SetActive(false);
        AdRoom.SetActive(false);
        numCounties = 3;
        getPrompt();
    }

    public void getPrompt()
    {
        gameObject.SetActive(true);
        choice = Random.Range(0, prompts.Count);
        currentPrompt = prompts[choice];
        //prompts.RemoveAt(choice);
        if (currentPrompt.hasThirdOption)
        {
            mButton.gameObject.SetActive(true);
            mButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.midText;
            yButton.gameObject.transform.localPosition = new Vector3(-500, -100, 0);
            nButton.gameObject.transform.localPosition = new Vector3(500, -100, 0);

        } 
        else
        {
            mButton.gameObject.SetActive(false);
            yButton.gameObject.transform.localPosition = new Vector3(-300, -100, 0);
            nButton.gameObject.transform.localPosition = new Vector3(300, -100, 0);
        }

        yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText;
        nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText;

        description.text = currentPrompt.promptDescription;

        nationalHappiness = (county1.GetComponent<County>().hapiness + county2.GetComponent<County>().hapiness + county3.GetComponent<County>().hapiness) / numCounties;
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
        foreach (var county in stats.Keys) 
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
        county1.SetActive(false);
        county2.SetActive(false);
        county3.SetActive(false);
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
        currentPrompt = prompts[choice];
        if (currentPrompt.hasThirdOption)
        {
            mButton.gameObject.SetActive(true);
            yButton.gameObject.SetActive(true);
            nButton.gameObject.SetActive(true);
            mButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.midText;
            yButton.gameObject.transform.localPosition = new Vector3(-500, -100, 0);
            nButton.gameObject.transform.localPosition = new Vector3(500, -100, 0);

        }
        else
        {
            yButton.gameObject.SetActive(true);
            nButton.gameObject.SetActive(true);
            mButton.gameObject.SetActive(false);
            yButton.gameObject.transform.localPosition = new Vector3(-300, -100, 0);
            nButton.gameObject.transform.localPosition = new Vector3(300, -100, 0);
        }
        description.gameObject.SetActive(true);

        county1.SetActive(true);
        county2.SetActive(true);
        county3.SetActive(true);

        AdRoom.SetActive(false);
        Map.SetActive(true);
        advice.gameObject.SetActive(false);
    }

    public void RichAdvice()
    {
        advice.gameObject.SetActive(true);
        advice.text = "I'm Rico Dinero and I support this message.";
    }

    public void PoorAdvice()
    {
        advice.gameObject.SetActive(true);
        advice.text = "I'm Anthony Arvin and I support this message.";
    }

}

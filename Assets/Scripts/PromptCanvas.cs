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

    public Button yButton;
    public Button mButton;
    public Button nButton;

    private void Start()
    {
        gameObject.SetActive(false);
        getPrompt();
    }

    public void getPrompt()
    {
        gameObject.SetActive(true);
        int choice = Random.Range(0, prompts.Count);
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

}

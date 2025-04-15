using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PromptCanvas : MonoBehaviour
{
    public float money = 100;

    public List<PromptStats> prompts;

    public PromptStats currentPrompt;

    public TextMeshProUGUI description;
    public TextMeshProUGUI advice;
    public TextMeshProUGUI natHapiness;
    public TextMeshProUGUI natTreasury;

    string randPoor;
    string randRich;

    public GameObject responseCanvas;
    public TextMeshProUGUI response;

    [Header ("Backgrounds")]
    public GameObject Map;
    public GameObject AdRoom;
    public GameObject MidtermBg;

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

    [Header("Midterms")]
    public int choicesMade;
    public Button midtermEscape;
    public TextMeshProUGUI midtermScore;

    private void Start()
    {
        gameObject.SetActive(false);
        eaButton.gameObject.SetActive(false);
        AdRoom.SetActive(false);
        choicesMade = 0;
        getPrompt();
    }

    public void getPrompt()
    {
        responseCanvas.SetActive(false);
        gameObject.SetActive(true);
        int choice = Random.Range(0, prompts.Count);
        currentPrompt = prompts[choice];
        //this VV is for making sure prompts don't repeat
        prompts.RemoveAt(choice);

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
            randPoor = randmCounty("Poor");
            description.text = currentPrompt.promptDescription.Replace("PoorCounty", randPoor);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText.Replace("PoorCounty", randPoor);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText.Replace("PoorCounty", randPoor);
        }
        if (currentPrompt.promptDescription.Contains("RichCounty"))
        {
            randRich = randmCounty("Wealthy");
            description.text = currentPrompt.promptDescription.Replace("RichCounty", randRich);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText.Replace("RichCounty", randRich);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText.Replace("RichCounty", randRich);
        }
        if (currentPrompt.promptDescription.Contains("RichCounty")&&(currentPrompt.promptDescription.Contains("PoorCounty")))
        {
            randRich = randmCounty("Wealthy");
            description.text = currentPrompt.promptDescription.Replace("RichCounty", randRich);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.yesText.Replace("RichCounty", randRich);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentPrompt.noText.Replace("RichCounty", randRich);
            randPoor = randmCounty("Poor");
            description.text = description.text.Replace("PoorCounty", randPoor);
            yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = yButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text.Replace("PoorCounty", randPoor);
            nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = nButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text.Replace("PoorCounty", randPoor);
        }

        for (int i = 0; i < counties.transform.childCount-1; i++)
        {
            nationalHappiness += counties.transform.GetChild(i).gameObject.GetComponent<County>().hapiness;
        }
        nationalHappiness /= counties.transform.childCount;
        natHapiness.text = "National Approval: " + nationalHappiness + "%";

        choicesMade++;
        if (choicesMade == 5)
        {
            MidtermEntrance();
        }
        natTreasury.text = "National Treasury: "+money;
        if (money <= 0)
        {
            SceneManager.LoadScene("NoMoney");
        }
        if (prompts.Count == 0) 
        {
            EndScreen();
        }
    }

    public void replay()
    {
        SceneManager.LoadScene("Paxia");
    }

    public string randmCounty(string wealth)
    {
        List<string> toPickFrom = new List<string>();
        if (wealth == "Wealthy")
        {
            foreach (County i in counties.gameObject.GetComponentsInChildren<County>())
            {
                if (i.hapiness > 50)
                {
                    toPickFrom.Add(i.gameObject.name);
                }
            }
        }
        if (wealth == "Poor")
        {
            foreach (County i in counties.gameObject.GetComponentsInChildren<County>())
            {
                if (i.hapiness < 50)
                {
                    toPickFrom.Add(i.gameObject.name);
                }
            }
        }
        if (toPickFrom.Count == 0)
        {//if somehow all counties are above or below 50 happiness there is a 1/9 chance that it could pick Rein twice which would not break the game but would be weird
            return "Rein";
        }
        else
        {
            int pickRand = Random.Range(0, toPickFrom.Count - 1);
            return toPickFrom[pickRand];
        }
    }
    public void yesPressed()
    {
        money += currentPrompt.yesMoney;
        updateCounties(currentPrompt.yesCountyEffects);
        gameObject.SetActive(false);
        responseCanvas.SetActive(true);
        response.text = currentPrompt.yesResponse;
    }

    public void midPressed()
    {
        money += currentPrompt.midMoney;
        updateCounties(currentPrompt.midCountyEffects);
        gameObject.SetActive(false);
        responseCanvas.SetActive(true);
        response.text = currentPrompt.midResponse;
    }

    public void noPressed() 
    {
        money += currentPrompt.noMoney;
        updateCounties(currentPrompt.noCountyEffects);
        gameObject.SetActive(false);
        responseCanvas.SetActive(true);
        response.text = currentPrompt.noResponse;
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

    public void MidtermExit()
    {
        
        aButton.gameObject.SetActive(true);
        if (currentPrompt.hasThirdOption)
        {
            mButton.gameObject.SetActive(true);
        }
        yButton.gameObject.SetActive(true);
        nButton.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        counties.SetActive(true);
        Map.SetActive(true);
        //Hiding Midterm Screens
        midtermScore.gameObject.SetActive(false);
        midtermEscape.gameObject.SetActive(false);
        MidtermBg.SetActive(false);
    }

    public void MidtermEntrance()
    {

        //Main screen buttons
        aButton.gameObject.SetActive(false);
        yButton.gameObject.SetActive(false);
        nButton.gameObject.SetActive(false);
        mButton.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        //counties
        counties.SetActive(false);
        Map.SetActive(false);
        //Showing Midterm Screens
        midtermScore.gameObject.SetActive(true);
        midtermEscape.gameObject.SetActive(true);
        MidtermBg.SetActive(true);

        if (nationalHappiness >= 66)
        {
            midtermScore.text = ("Things seem to be looking good for the current president's approval ratings. But time will tell if it is enough for the next election in a few months.");
        } else if (nationalHappiness < 66 && nationalHappiness >= 33)
        {
            midtermScore.text = ("Things are dicey for the current president's approval rating. But there is still time to recover for the next election in a few months.");
        } else
        {
            midtermScore.text = ("Things aren't looking good for the current president's approval. With only so many months left will the President be able to recover?");
        }
    }

    public void EndScreen()
    {
        //Main screen buttons
        aButton.gameObject.SetActive(false);
        yButton.gameObject.SetActive(false);
        nButton.gameObject.SetActive(false);
        mButton.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        //counties
        counties.SetActive(false);
        Map.SetActive(false);
        //Showing Midterm Screens
        MidtermBg.gameObject.SetActive(true);
        midtermScore.gameObject.SetActive(true);

        if (nationalHappiness >= 75)
        {
            midtermScore.text = ("You have earned the trust of your people and have been reelected for a second term in a landslide victory.");
        }
        else if (nationalHappiness < 75 && nationalHappiness >= 50)
        {
            midtermScore.text = ("The race was close but you won just slighly.");
        }
        else if (nationalHappiness < 50 &&  nationalHappiness >= 25)
        {
            midtermScore.text = ("The race was close but your opponent pulled ahead, winning the election. You will be remembered as a decent president");
        } else
        {
            midtermScore.text = ("You have lost the election in a landslide. You will be remembered as the worst president in Paxia's history.");
        }
    }

}

using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
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

    [Header("Backgrounds")]
    public GameObject Map;
    public GameObject AdRoom;
    public GameObject MidtermBg;

    [Header("Buttons")]  
    public Button mButton;
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

    [Header("Tutorial Bools")]
    public int sequence;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        eaButton.gameObject.SetActive(false);
        AdRoom.SetActive(false);
        sequence = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene("Paxia");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void AdvisorsScreen()
    {
        //Main screen buttons
        eaButton.gameObject.SetActive(true);
        aButton.gameObject.SetActive(false);
        //yButton.gameObject.SetActive(false);
        //nButton.gameObject.SetActive(false);
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
        mButton.gameObject.SetActive(true);
        /*yButton.gameObject.SetActive(true);
        nButton.gameObject.SetActive(true);*/
        description.gameObject.SetActive(true);
        counties.SetActive(true);
        AdRoom.SetActive(false);
        Map.SetActive(true);
        advice.gameObject.SetActive(false);
        sequence++;
    }

    public void RichAdvice()
    {
        advice.gameObject.SetActive(true);
        advice.text = "Hi, I'm Rico Dinero, your financial advisor.";
    }

    public void PoorAdvice()
    {
        advice.gameObject.SetActive(true);
        advice.text = "Hi, I'm Anthony Arvin, I look forward to working with you.";
    }

    public void ContButton()
    {
        if (sequence == 0)
        {
            sequence++;
        }
        else if (sequence == 1)
        {
            sequence++;
            description.text = ("The more red a county is, the more they disapprove of you as their president. The greener, the more they approve of you. You will make choices that determine thair approval ratings for your next election.");

        }
        else if (sequence == 2)
        {
            mButton.gameObject.SetActive(false);
            aButton.gameObject.SetActive(true);
            description.text = ("To help with your choices you have two advisors, Anthony Arvin and Rico Dinero. You can meet them through the button in the bottom left.");

        }
        else if (sequence == 3)
        {
            mButton.gameObject.SetActive(true);
            natTreasury.gameObject.SetActive(true);
            description.text = ("On the bottom right is your national treasury. You will be ousted if we run out of money and someone else will take your place.");
            sequence++;
        }
        else if (sequence == 4)
        {
            description.text = ("That's it! Press continue to start your term.");
            sequence++;

        }
        else
        {
            SceneManager.LoadScene("Paxia");
        }
    }
}

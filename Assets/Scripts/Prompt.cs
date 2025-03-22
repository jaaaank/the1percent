using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    public Button yesB;
    public Button noB;
    public GameObject response;

    public PromptStats stats;

    public GameManager manager;
    PromptContainer promptContainer;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        promptContainer = GameObject.Find("PromptContainer").GetComponent<PromptContainer>();
        loadPrompt();
    }

    void loadPrompt()
    {
        int promptChoice = Random.Range(0, promptContainer.prompts.Length);
        stats = promptContainer.prompts[promptChoice];
        gameObject.GetComponent<TextMeshProUGUI>().text = stats.promptDescription;
        yesB.GetComponentInChildren<TextMeshProUGUI>().text = stats.yesText;
        noB.GetComponentInChildren<TextMeshProUGUI>().text = stats.noText;
    }
    public void choicePressed(bool choice)
    {
        response.SetActive(true);
        gameObject.SetActive(false);
        if (choice)
        {
            manager.updateScores(stats.yesStats);
            response.GetComponent<TextMeshProUGUI>().text = stats.yesResponse;
        }
        else
        {
            manager.updateScores(stats.noStats);
            response.GetComponent<TextMeshProUGUI>().text = stats.noResponse;
        }
        }

    public void continuePressed()
    {
        gameObject.SetActive(true);
        response.SetActive(false);
        loadPrompt();
    }
}

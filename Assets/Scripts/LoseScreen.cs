using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public void playAgain()
    {
        SceneManager.LoadScene("Paxia");
    }
}

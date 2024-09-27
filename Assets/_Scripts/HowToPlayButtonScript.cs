using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayButtonScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
}

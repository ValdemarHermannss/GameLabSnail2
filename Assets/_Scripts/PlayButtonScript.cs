using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public void PlayGame()
    {
        DataStore.Collectibles = new();
        SceneManager.LoadScene("Level_1_Scene");
    }
}

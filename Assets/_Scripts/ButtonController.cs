using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int numberOfButtons;
    public int buttonsPressed = 0;

    public void ButtonsPressed()
    {
        buttonsPressed++;
        Debug.Log(buttonsPressed);
        if (numberOfButtons > 0 && numberOfButtons == buttonsPressed)
        {
            Debug.Log("Destroy Door");
            Destroy(gameObject);
        }
    }
}

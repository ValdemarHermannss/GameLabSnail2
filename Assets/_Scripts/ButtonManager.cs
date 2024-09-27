using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public bool isInRange;
    public bool isPressed = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    [SerializeField] private GameObject door;


    [SerializeField] TMP_Text pickUpText;

    public SpriteRenderer renderer;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isInRange && !isPressed)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                isPressed = true;
                renderer.color = Color.green;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snail") && !isPressed)
        {
            isInRange = true;
            Debug.Log("Player in range");
            pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snail") && !isPressed)
        {
            isInRange = false;
            pickUpText.gameObject.SetActive(false);
        }
    }

    public void IPressedTheButton()
    {
        Debug.Log("i pressed the button");
        
    }
}

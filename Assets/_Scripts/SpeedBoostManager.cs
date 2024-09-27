using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBoostManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;

        if (hitObject.CompareTag("Snail"))
        {
            
        }
    }
}

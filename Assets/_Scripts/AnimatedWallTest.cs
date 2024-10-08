using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatedWallTest : MonoBehaviour
{
    
    public Vector3 minScale = new Vector3(1f, 0.5f, 1f); //Min Scale
    public Vector3 maxScale = new Vector3(1f, 1.5f, 1f); //Max Scale
    public float speed = 2f; //Speed

    private bool isGrowing = true;
    private bool isCrushing = false;
    private bool isInDanger = false;

    // Update is called once per frame
    void Update()
    {

        if (isGrowing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * speed);


            if (Vector3.Distance(transform.localScale, maxScale) < 0.01f)
                isGrowing = false;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, minScale, Time.deltaTime * speed);


            if (Vector3.Distance(transform.localScale, minScale) < 0.01f)
                isGrowing = true;
        }
        isCrushing = !isGrowing;     
    } 
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DangerZone"))
        {
            isInDanger = true;
            Debug.Log("this can't be good");
        }

        if (isInDanger == true)
        {
           if( other.gameObject.CompareTag("Snail"))
           {
                DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("GameOverScene");

           }
        }
    }*/
}



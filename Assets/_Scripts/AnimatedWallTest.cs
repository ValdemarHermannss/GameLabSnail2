using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatedWallTest : MonoBehaviour
{
    private bool isCollidingWithWall = false;
    private bool isCollidingWithMovingDoor = false;
    
    public Vector3 minScale = new Vector3(1f, 0.5f, 1f); //Min Scale
    public Vector3 maxScale = new Vector3(1f, 1.5f, 1f); //Max Scale
    public float speed = 2f; //Speed

    private bool isGrowing = true;
    private bool isCrushing = false;

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
    private void OnCollisionEnter2D(Collision2D collision) // Checks if it collides with the player and the position of the player when it collides.
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            isCollidingWithWall = true;
            Debug.Log("Colliding with a wall");
        }
        
        if (collision.gameObject.CompareTag("MovingDoor"))
        {
            isCollidingWithMovingDoor = true;
            Debug.Log("Colliding with the moving door");
        }

        if (isCollidingWithWall && isCollidingWithMovingDoor)
        {
            DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameOverScene");
        }
    }
    private void OnCollissionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Walls"))
            {
                isCollidingWithWall = false;
            }

            if (collision.gameObject.CompareTag("MovingDoor"))
            {
                isCollidingWithMovingDoor = false;
            }
        }
        
        /* if (collision.gameObject.tag == "MovingDoor" && "Walls")
             DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameOverScene");




        /*if (collision.gameObject.tag == "Snail")
        {
            Vector2 playerPosition = collision.transform.position;

            Vector2 wallBottomEdge = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2));
            
            
            if (isCrushing && playerPosition.y < wallBottomEdge.y) // If the player is under the bottom edge of the wall it will m
            {
                DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("GameOverScene");
            }
        } */
    }



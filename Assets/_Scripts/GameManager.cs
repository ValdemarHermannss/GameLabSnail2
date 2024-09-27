using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private Rigidbody2D rb;
    private TrailRenderer tr;

    public Color currentColor;

    private float Timer;
    [SerializeField] private CountdownTimer countdownTimer;

    public GameObject particleEffectPrefab;


    public enum availablecolors
    {
        white,
        red,
        green,
        blue
    }

    public availablecolors currentColorEnum;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.material = new Material(Shader.Find("Sprites/Default"));

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        tr.colorGradient = gradient;

        rb = GetComponent<Rigidbody2D>();

        currentColor = GetComponent<TrailRenderer>().material.color;
        currentColor = Color.green;
        DataStore.CurrentScene = SceneManager.GetActiveScene().name;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Door disabling code. It checks if the door has been collided with and if it's with the right color.
        if (collision.gameObject.tag == "RedDoor")
        {
            Debug.Log("Collided with Door");

            if (currentColorEnum == availablecolors.red)
            {
                StartCoroutine(DisableDoorCollider(collision.gameObject));
                Debug.Log("Got through red door");
            }
        }

        if (collision.gameObject.tag == "GreenDoor")
        {
            Debug.Log("Collided with Door");

            if (currentColorEnum == availablecolors.green)
            {
                StartCoroutine(DisableDoorCollider(collision.gameObject));
                Debug.Log("Got through green door");
            }
        }

        if (collision.gameObject.tag == "BlueDoor")
        {
            Debug.Log("Collided with Door");

            if (currentColorEnum == availablecolors.blue)
            {
                StartCoroutine(DisableDoorCollider(collision.gameObject));
                Debug.Log("Got through blue door");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            // Next level and previous level checking code. Respawn point stuff not implemented yet.
            if (other.gameObject.tag == "NextLevel")
            {
                if (DataStore.Collectibles.Count >= 5)
                {
                    SceneManager.LoadScene("SpecialWinningScene");
                    return;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("Player finished level goal");
            }
            if (other.gameObject.tag == "PreviousLevel")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
 
        if (other.gameObject.tag == "Trap")
        {
            // Kills the player
            DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameOverScene");
        }
       
        if (other.gameObject.tag == "RedPaint")
        {
            // Changes the color of the trail
            currentColor = Color.red;
            GetComponent<TrailRenderer>().material.color = currentColor;
            Debug.Log("Snail Collided with " + other.gameObject.tag);
            currentColorEnum = availablecolors.red;
        }

        if (other.gameObject.tag == "GreenPaint")
        {
            // Changes the color of the trail
            currentColor = Color.green;
            GetComponent<TrailRenderer>().material.color = currentColor;
            Debug.Log("Snail Collided with " + other.gameObject.tag);
            currentColorEnum = availablecolors.green;
        }

        if (other.gameObject.tag == "BluePaint")
        {
            // Changes the color of the trail
            currentColor = Color.blue;
            GetComponent<TrailRenderer>().material.color = currentColor;
            Debug.Log("Snail Collided with " + other.gameObject.tag);
            currentColorEnum = availablecolors.blue;
        }

        if (other.gameObject.tag == "RedDoor")
        {
            Debug.Log("Collided with Door");

            if (currentColorEnum == availablecolors.red)
            {
                StartCoroutine(DisableDoorCollider(other.gameObject));
                Debug.Log("Got through red door");
            }
        }

        if (other.gameObject.CompareTag("Stopwatch"))
        {
            Debug.Log("Picked up " + other.gameObject.tag);
            CountdownTimer.instance.timeLeft += 5;
            GameObject g = Instantiate(particleEffectPrefab);
            g.transform.position = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("RecipeScrap"))
        {
            Debug.Log("Recipe Scrap collected ");
            GameObject g = Instantiate(particleEffectPrefab);
            g.transform.position = other.gameObject.transform.position;

            if (!DataStore.Collectibles.Contains(SceneManager.GetActiveScene().name))
            {
                DataStore.Collectibles.Add(SceneManager.GetActiveScene().name);
            }

            Debug.Log(DataStore.Collectibles.Count);
            Destroy(other.gameObject);


        }

        
    }

    // Collider disabling door
    private IEnumerator DisableDoorCollider(GameObject gameObject)
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(5f);
        collider.enabled = true;
    }
}


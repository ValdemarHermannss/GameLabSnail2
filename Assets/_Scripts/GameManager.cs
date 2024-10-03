using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    private Rigidbody2D rb;
    private TrailRenderer tr;

    public Color currentColor;

    private float Timer;
    [SerializeField] private CountdownTimer countdownTimer;

    public GameObject particleEffectPrefab;
    public GameObject NextLevelParticlePrefab;

    public TMP_Text levelFinishedText;

    public bool isInDanger = false;


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
        levelFinishedText.GetComponent<TMP_Text>().enabled = false;

            //Trail
        tr = GetComponent<TrailRenderer>();
        tr.material = new Material(Shader.Find("Sprites/Default"));

            // Trail renderer code
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

        //Colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
            // Doors. Checking wether you can pass through the colored door or not.
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

        if(collision.gameObject.tag == "MovingDoor")
        {
            if(isInDanger == true)
            {
                DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

        //Triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        {
                // Next level portal
            if (other.gameObject.tag == "NextLevel")
            {
                levelFinishedText.GetComponent<TMP_Text>().enabled = true;
                GetComponent<CountdownTimer>().levelFinished = true;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                GameObject g = Instantiate(NextLevelParticlePrefab);
                g.transform.position = other.gameObject.transform.position;
                StartCoroutine(LoadNextLevel());
            }
        }
 
            //Traps
        if (other.gameObject.tag == "Trap")
        {
            // Kills the player
            DataStore.Collectibles.Remove(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameOverScene");
        }
       
            //Trail color changes
        if (other.gameObject.tag == "RedPaint")
        {
            currentColor = Color.red;
            GetComponent<TrailRenderer>().material.color = currentColor;
            Debug.Log("Snail Collided with " + other.gameObject.tag);
            currentColorEnum = availablecolors.red;
        }

        if (other.gameObject.tag == "GreenPaint")
        {
            currentColor = Color.green;
            GetComponent<TrailRenderer>().material.color = currentColor;
            Debug.Log("Snail Collided with " + other.gameObject.tag);
            currentColorEnum = availablecolors.green;
        }

        if (other.gameObject.tag == "BluePaint")
        {
            currentColor = Color.blue;
            GetComponent<TrailRenderer>().material.color = currentColor;
            Debug.Log("Snail Collided with " + other.gameObject.tag);
            currentColorEnum = availablecolors.blue;
        }


            //Time pickups
        if (other.gameObject.CompareTag("Stopwatch"))
        {
            Debug.Log("Picked up " + other.gameObject.tag);
            CountdownTimer.instance.timeLeft += 5;
            GameObject g = Instantiate(particleEffectPrefab);
            g.transform.position = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "DangerZone")
        {
            Debug.Log("In the Zone");
        }

            //Recipe scraps
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("DangerZone"))
        {
            isInDanger = true;
        }
        else
        {
            isInDanger = false;
        }
    }

    void LoadSpecialWin()
    {
        SceneManager.LoadScene("SpecialWinningScene");
        return;
    }

        // A code which allows you to pass through colored doors if your trail matches the color.
    private IEnumerator DisableDoorCollider(GameObject gameObject)
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(5f);
        collider.enabled = true;
    }

        // A delay before you enter the next level. To let the player breathe.
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(5f);
        if (DataStore.Collectibles.Count >= 5)
        {
            LoadSpecialWin();
        }
        else
        {
            Debug.Log("Player finished level goal");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}


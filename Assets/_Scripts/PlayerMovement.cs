using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    GameObject particalEffectPREFAB;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);


        if(movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        {


            if (other.CompareTag("SpeedBoost"))
            {
                Debug.Log("Speed");
                StartCoroutine(SpeedBoost(2));
            }
        }
        IEnumerator SpeedBoost(float speedBoost)
        {
            speedBoost = 4f;
            speed = speed + speedBoost;
            yield return new WaitForSeconds(1);
            speed = 3f;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    public float speed;
    private PlayerControllerX playerControllerScript;
    private float leftBound = -20;
    private float rigthBound = 20;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        if (transform.position.x > 0)
        {
            speed *= -1; //Change dirrection
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If game is not over, move to the left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime,Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if ((transform.position.x < leftBound || transform.position.x > rigthBound) && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    //Game VARIABLES
    public int playerCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce,ForceMode.Impulse);
        }

        //Check if out of bounds
        if (transform.position.y > 16) {
            playerRb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            GameOver();
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            //Check if collided with Big
            if (other.gameObject.name.Contains("Big"))
            {
                playerCounter += 5; //Add 5 to counter
            }
            else {
                playerCounter++; //Add 1
            }
            Debug.Log($"Current score: {playerCounter}");
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground") && !gameOver) {
            GameOver();
        }

    }

    //Function that controls Gameover
    private void GameOver() {
        explosionParticle.Play();
        playerAudio.PlayOneShot(explodeSound, 1.0f);
        gameOver = true;
        Debug.Log("Game Over!");
    }

}

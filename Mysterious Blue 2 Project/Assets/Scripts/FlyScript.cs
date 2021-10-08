using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour
{
    //enemy health
    public int enemyHealth;
    private int currentEnemyHealth;

    //enemy speed
    public float speed;

    //enemy direction
    private bool isMovingDown;

    //enemy audio
    private AudioSource audioSource;
    public AudioClip enemyDie;

    // Start is called before the first frame update
    void Start()
    {
        //enemy direction
        isMovingDown = false;

        //for enemy health
        currentEnemyHealth = enemyHealth;

        // getting the audio source
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemy direction
        if (!isMovingDown)
        {
            print("up");
            transform.position += transform.up * speed * Time.deltaTime;

        }
        else
        {
            print("down");
            transform.position -= transform.up * speed * Time.deltaTime;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // collision with in visible wall
        if (other.gameObject.tag.Equals("wall"))
        {
            print("fly wall");

            isMovingDown = !isMovingDown;

            
        }

    }

    public void TakeDamage(int damage)
    {

        //take damage
        currentEnemyHealth -= damage;

        //dead
        if (currentEnemyHealth <= 0)
        {

            die();
        }

    }

    void die()
    {
        audioSource.PlayOneShot(enemyDie);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 2);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //enemy health
    public int enemyHealth;
    private int currentEnemyHealth;

    //enemy speed
    public float speed;

    //enemy direction
    private bool isMovingRight;

    //enemy audio
    private AudioSource audioSource;
    public AudioClip enemyDie;


    // Start is called before the first frame update
    void Start()
    {

        //enemy direction
        isMovingRight = true;

        //for enemy health
        currentEnemyHealth = enemyHealth;

        // getting the audio source
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        //enemy direction
        if (!isMovingRight)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
           
        }
        else
        {
            transform.position += transform.right * speed * Time.deltaTime;
            
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // collision with invisible wall
        if (other.gameObject.tag.Equals("wall"))
        {
            print("hit wall");

            isMovingRight = !isMovingRight;

            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            print("collide with enemy");

            isMovingRight = !isMovingRight;

            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void TakeDamage(int damage)
    {

        //take damage
        currentEnemyHealth -= damage;

        //dead
        if(currentEnemyHealth <= 0)
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

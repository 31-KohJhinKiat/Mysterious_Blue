using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class CharacterScript : MonoBehaviour
{
    //physics
    private Rigidbody2D rb;
    public float speed;
    private bool facingRight;
    public float jumpForce;
    private bool isGrounded;
    public Transform feetPosition;
    public float radius;
    public LayerMask groundMask;
    private int jumpCounter;

    //animation
    private Animator animator;

    //attack
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask enemyLayers;

    //health
    public int health;
    private int healthCount;
    public GameObject HealthText;

    //coin
    public int coin;
    private int coinCount;
    public GameObject coinText;

    //audio
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip walkSound;
    public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        healthCount = health;
        

    }

    // Update is called once per frame
    void Update()
    {
        //display health
        HealthText.GetComponent<Text>().text = "Health: " + healthCount;

        //disply number of coin collected
        coinText.GetComponent<Text>().text = "Coins: " + coinCount;

        //float hInput = Input.GetAxisRaw("Horizontal");
        float hInput = JoyStickManager.instance.GetDirection().x;
        float vInput = JoyStickManager.instance.GetDirection().y;

        //keyboard movement
        if (Input.GetKey(KeyCode.D))
        {
            hInput = 1;

        }

        if (Input.GetKey(KeyCode.A))
        {
            hInput = -1;

        }

        //jump
        float yVelocity = rb.velocity.y;
        if(yVelocity > 2 * jumpForce)
        {
            yVelocity = 2 * jumpForce;
        }

        //movement with pad
        animator.SetFloat("hSpeed", Mathf.Abs(hInput));

        

        rb.velocity = new Vector2(hInput * speed, yVelocity);

        if (hInput > 0 && facingRight == true)
        {
            flip();
            facingRight = false;
        }
        else if (hInput < 0 && facingRight == false)
        {
            flip();
            facingRight = true;
        }

        
        if (Physics2D.OverlapCircle(feetPosition.position, radius, groundMask))
        {
            isGrounded = true;
            jumpCounter = 0;
            animator.SetBool("isJump", false);
            

        }
        

        //jump button
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2)
        {
            if(jumpCounter == 0)
            {
                rb.velocity = new Vector2(hInput * speed, 0);
            }
            Jump();
        }

        else if (vInput > 0.5f && jumpCounter < 2)
        {
            Jump();
        }

        //fall off the screen
        if (transform.position.y <= -5)
        {
            SceneManager.LoadScene("loseScene");
        }

        //attack
        if (Input.GetKeyDown(KeyCode.J))
        {
            print("attack");
            Attack();
            
        }

    }

    //change directions
    private void flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //number of jumps
    public void Jump()
    {
        if (jumpCounter < 2)
        {
            animator.SetBool("isJump", true);
            rb.velocity += Vector2.up * jumpForce;
            isGrounded = false;
            jumpCounter++;
            audioSource.PlayOneShot(jumpSound);
        }
           
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision with coin
        if(collision.gameObject.tag.Equals("coin"))
        {
            Destroy(collision.gameObject);
            coinCount++;
            audioSource.PlayOneShot(coinSound);
        }

        if(collision.gameObject.tag.Equals("health"))
        {
            Destroy(collision.gameObject);
            healthCount++;
            audioSource.PlayOneShot(healthSound);
        }

        //collision with endgoal
        if (collision.gameObject.tag.Equals("EndGoal"))
        {
            SceneManager.LoadScene("winScene");
        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // walking sound
        if (collision.gameObject.tag == "grass")
        {
            isGrounded = true;
            print("on ground");
            audioSource.PlayOneShot(walkSound);
        }

        if (collision.gameObject.tag.Equals("enemy"))
        {
            //if (animator.SetBool("isAttack") == true)
          
            healthCount -= 1;
            audioSource.PlayOneShot(hitSound);
            
            if (healthCount <= 0)
            {

                SceneManager.LoadScene("LoseScene");

            }

        }
    }

    public void Attack()
    {
        //play attack animation
        animator.SetTrigger("attackTrigger");

        //detect if enemy is in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayers);

        //damage enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            
            // Get the enemy gameobject //
            GameObject enemyGameObject = enemy.gameObject;
            
            // Get the script component //
            EnemyScript enemyScript =  enemyGameObject.GetComponent<EnemyScript>();

            if(enemyScript != null)
            {
                // Then deduct health //
                enemyScript.TakeDamage(1);

                // for testing //
                print("enemyScript.enemyHealth: " + enemyScript.enemyHealth);
            }
            else
            {
                FlyScript flyScript = enemyGameObject.GetComponent<FlyScript>();

                // Then deduct health //
                flyScript.TakeDamage(1);

                // for testing //
                print("flyScript.enemyHealth: " + flyScript.enemyHealth);
            }
            
            
        }

        //sound effect
        audioSource.PlayOneShot(attackSound);

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

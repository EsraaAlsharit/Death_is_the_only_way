using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D play;
    public float speed = 5f;
    private float direction = 0f;
    public float jumpSpeed = 6f;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;

    private Vector3 respawnPoint;
    //public GameObject dedly;

    public Text deathtimes;

    //[SerializeField] private AudioSource walkSoundEffect;
    [SerializeField] private AudioSource damgeSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        deathtimes.text = "Dead: " + Death.deaths;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        direction = Input.GetAxis("Horizontal");
        
        if (direction > 0f)
        {
            //walkSoundEffect.Play();    
            play.velocity = new Vector2(direction * speed, play.velocity.y);
            transform.localScale = new Vector2(1F, 1F);


        }
        else if (direction < 0f)
        {
            //walkSoundEffect.Play();
            play.velocity = new Vector2(direction * speed, play.velocity.y);
            transform.localScale = new Vector2(-1F, 1F);
            
        }
        else
        {
            play.velocity = new Vector2(0, play.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            play.velocity = new Vector2(play.velocity.x, jumpSpeed);
        }

        playerAnimation.SetFloat("speed", Mathf.Abs(play.velocity.x));
        playerAnimation.SetBool("onground", isTouchingGround);

        

    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "next")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // Can also use SceneManager.LoadScene(1); to load a specific scene
            respawnPoint = transform.position;
        }
        else if(collision.tag == "dedly")
        {
            damgeSoundEffect.Play();
            Death.deaths += 1;
            deathtimes.text = "Dead: " + Death.deaths;
            //collision.gameObject.SetActive(false);
            transform.position = respawnPoint;
        }
         
       
    }

   

}

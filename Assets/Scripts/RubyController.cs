using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    public int cogCount;
    public Text cogsText;
    
    public GameObject projectilePrefab;
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;


    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;
    public AudioClip throClip;
    public AudioClip hitClip;
    public AudioClip bgMusic;
    public AudioClip brokeClip;
    public AudioClip speedClip;
    public AudioClip ribbitClip;

    public GameObject hitParticles;
    public GameObject healthIncrease;
    //public GameObject slugParticles;
    

    public Text score;
    public int scoreValue = 0;

     public GameObject winTextObject;
     public GameObject levelUpTextObject;
     public GameObject loseTextObject;
     
     //public GameObject brokeTextObject;

     private bool gameOver;

    public AudioClip musicClipWin;
    public AudioClip musicClipLoss;
    
    public static int level = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
         audioSource= GetComponent<AudioSource>();
         audioSource.clip = bgMusic;
        audioSource.Play();
        audioSource.loop = true;
        
        score.text = scoreValue.ToString();
        scoreValue = 0;

        ChangeCogs(4);

        SetCountText();

        
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        levelUpTextObject.SetActive(false);
        //brokeTextObject.SetActive(false);
        
        

        //Invoke("DisableText", 3f);//invoke after 3 seconds

        gameOver = false;
       
    
    }

     //void DisableText()
   // { 
      //brokeTextObject.SetActive(false);
    //}    

    void SetCountText()
    {
        score.text = "Bots fixed: " + scoreValue.ToString();
        if(scoreValue >= 4)
       {
            if(level == 1)
            {
                levelUpTextObject.SetActive(true);
                audioSource.Stop();
                audioSource.clip = musicClipWin;
                audioSource.Play();
                audioSource.loop = false;
            }
            else
            {
                winTextObject.SetActive(true);
                audioSource.Stop();
                 audioSource.clip = musicClipWin;
                 audioSource.Play();
                 audioSource.loop = false;
                EndGame();
            }
       }
        //cogs.text = "Cogs: " + cogCount.ToString();
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
                
        }

        
        if(Input.GetKeyDown(KeyCode.C) && !gameOver && cogCount > 0)
        {
            Launch();
            ChangeCogs(cogCount - 1);
            PlaySound(throClip);
        }

         if (Input.GetKeyDown(KeyCode.T))
         {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Inky"));

            if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    if(level == 1 && scoreValue >= 4)
                    {
                         SceneManager.LoadScene("Level2");
                         level = 2;
                    }
                    else
                    {
                        NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                        if (character != null)
                        {
                            
                            if (cogCount >= 3)
                            {
                            
                            ChangeCogs(cogCount - 3);
                            speed = 4.5f;
                            PlaySound(speedClip);
                            GameObject healthIncreaseObject = Instantiate(healthIncrease, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                            }
                            else
                            {
                            PlaySound(brokeClip);
                            audioSource.loop = false;
                            }

                        }  
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Inky"));

             if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    if(level == 1 && scoreValue >= 4)
                    {
                         SceneManager.LoadScene("Level2");
                         level = 2;
                    }
                    else
                    {
                        NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                        if (character != null)
                        {
                            character.DisplayDialog();
                            
                        }  
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

             if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    if(level == 1 && scoreValue >= 4)
                    {
                         SceneManager.LoadScene("Level2");
                         level = 2;
                    }
                    else
                    {
                        NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                        if (character != null)
                        {
                            character.DisplayDialog();
                            PlaySound(ribbitClip);
                            
                        }  
                    }
                }
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene
            }

        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if(gameOver)
            return;

        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            GameObject hitParticlesObject = Instantiate(hitParticles, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            PlaySound(hitClip);
            speed = 3.0f;

        }
        if (amount > 0)
        {
            GameObject healthIncreaseObject = Instantiate(healthIncrease, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

         if(currentHealth <= 0)
       {

           loseTextObject.SetActive(true);
           audioSource.Stop();
           audioSource.clip = musicClipLoss;
            audioSource.Play();
            audioSource.loop = false;
        
           EndGame();
       }
    }
    
    public void ChangeScore(int amount)
    {
        
            //animator.SetTrigger("Hit");
            
            //GameObject hitParticlesObject = Instantiate(hitParticles, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            //PlaySound(hitClip);
        
        
        scoreValue = scoreValue + amount;
        SetCountText();
        //UIHealthBar.instance.SetValue(currentScore);
    }

    public void ChangeCogs(int amount)
    {
        cogCount = amount;
        cogsText.text = "Cogs: " + cogCount.ToString();

        //RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

       // if(Input.GetKeyDown(KeyCode.Return) && hit.collider != null && cogCount >= 3)
        //{
        //    ChangeCogs(cogCount - 3);
          //  PlaySound(throClip);
       // }
        //else
        //{
           // brokeTextObject.SetActive(true);
       // }

       //RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

       // if(Input.GetKey(KeyCode.T) && cogCount > 3)
        //{
            //ChangeCogs(cogCount - 3);
            //GameObject healthIncreaseObject = Instantiate(healthIncrease, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        //}

        
    }

    void Launch()
    {

        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    
    }
     
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if(collision.collider.tag == "Enemy")
        //{
          //collision.collider.gameObject.SetActive(false);
          //lives = lives - 1;

          //SetCountText();
        //}
        
        //if(scoreValue == 4 && collision.collider.tag == "Coin")
        //{
           // transform.position = new Vector2(78.5f, 0f);
           // lives = 3;
        //}


    }

    //private void Trade()
   // {
       // gameOver = false;
       // RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

       // if(Input.GetKeyDown(KeyCode.K) && hit.collider != null && cogCount > 3)
        //{
           // ChangeCogs(cogCount - 3);
           // GameObject healthIncreaseObject = Instantiate(healthIncrease, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
      //  }
  //  }

    private void EndGame()
    {
        gameOver = true;
        speed = 0f;
        animator.SetFloat("Speed", 0f);
        levelUpTextObject.SetActive(false);
    }

    
}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Mario : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;

    Vector2 destination;
    Vector2 playerRun;
    public float speed = 4f;
    public float playerMaxHP = 3;
    public float currentPlayerHP;
    bool playerDeath;
    public bool hammer;
    private float hammerTimer;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
        currentPlayerHP = playerMaxHP;
        playerDeath = false;
        UIManager.instance.MarioHealthSlider.maxValue = playerMaxHP;
        UIManager.instance.MarioHealthSlider.value = currentPlayerHP;
        UpdatePlayerHealth();
    }

    private void FixedUpdate()
    {
        if (playerDeath) return;

        playerRun = destination - (Vector2)transform.position;

        if (playerRun.magnitude < 0.1)
        {
            playerRun = Vector2.zero;
        }

        rb.MovePosition(rb.position + playerRun.normalized * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeath) return;
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            anim.SetBool("Hammer", true);
            hammer = true;
        }
        else
        {
            anim.SetBool("Hammer", false);
        }

        if (playerRun != Vector2.zero)
        {
            anim.SetBool("playerIsRunning", true);
        }
        else
        {
            anim.SetBool("playerIsRunning", false);
        }

        SpriteFlip();

        if (hammer)
        {
            hammerTimer += Time.deltaTime;

            if (hammerTimer>1)
            {
                hammerTimer = 0;
                hammer = false;
            }
        }
        
    }

    public void SpriteFlip()
    {
        if (destination.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }


    public void DamagePlayer(int amountOfDamage)
    {
        currentPlayerHP -= amountOfDamage;
        UpdatePlayerHealth();

        if (currentPlayerHP == 0)
        {
            playerDeath = true;
            anim.SetBool("playerHurt", true);
            SceneManager.LoadScene(1);
        }
        else 
        {
            playerDeath = false;
            anim.SetBool("playerHurt", true);
        }
    }

    private void LateUpdate()
    {
        anim.SetBool("playerHurt", false);
    }

    private void UpdatePlayerHealth()
    {
        UIManager.instance.MarioHealthSlider.value = currentPlayerHP;
    }

}

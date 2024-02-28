using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Koopa : MonoBehaviour
{

    Vector2 chaseDirection;
    private Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;

    public Transform target;
    public float minimumSpeed = 2f;
    public float maximumSpeed = 3f;
    public float chaseSpeed = 2f;
    public float curveTimePosition;
    public float enemyMaxHP = 3;
    public float currentEnemyHP;
    public bool enemyIsRunning;
    public Transform spawningPoint;
    public AnimationCurve speedCurve;

    

   [SerializeField] int enemyDamageAmount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentEnemyHP = enemyMaxHP;
        chaseSpeed = minimumSpeed;

        UIManager.instance.KoopaHealthSlider.maxValue = enemyMaxHP;
        UIManager.instance.KoopaHealthSlider.value = currentEnemyHP;
        UpdateEnemyHealth();

        transform.position = spawningPoint.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize(); 
            Vector2 velocity = direction * chaseSpeed;
            rb.velocity = velocity;
        }
        SpriteFlip();

        curveTimePosition += Time.deltaTime;
        chaseSpeed = Mathf.Lerp(minimumSpeed, maximumSpeed, speedCurve.Evaluate(curveTimePosition));
    }

    public void SpriteFlip()
    {
        if(target.position.x < transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent <Mario>().hammer)
            {
                DamageEnemy(enemyDamageAmount);
                transform.position = spawningPoint.position;
                anim.SetBool("enemyHurt", true);
            }
            else
            {
                collision.gameObject.SendMessage("DamagePlayer", 1, SendMessageOptions.DontRequireReceiver);
                transform.position = spawningPoint.position;
            }
        }
    }

    private void UpdateEnemyHealth()
    {
        UIManager.instance.KoopaHealthSlider.value = currentEnemyHP;
    }

    public void DamageEnemy(int amountOfDamage)
    {
        currentEnemyHP -= amountOfDamage;
        anim.SetBool("enemyHurt", true);
        UpdateEnemyHealth();

        if (currentEnemyHP <= 0)
        {
            Destroy(gameObject);
            anim.SetBool("enemyHurt", true);
            SceneManager.LoadScene(2);
        }
             
    }

    private void LateUpdate()
    {
        anim.SetBool("enemyHurt", false);
    }
}

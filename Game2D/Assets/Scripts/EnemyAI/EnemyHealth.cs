using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHealth : MonoBehaviour
{
    public Transform groundCheck;
    Transform target;
    Rigidbody2D rgbEnemy;
    Animator animator;
    CapsuleCollider2D cap2d;
    CharacterController characterController;

    public float maxEnemyHealth = 100f;
    public float currentEnemyHealth;
    internal bool gotDamage;
    public bool enemyHurt;
    public bool enemyDead;
    int sayac = 0;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        characterController = FindObjectOfType<CharacterController>();
        rgbEnemy = GetComponent<Rigidbody2D>();
        cap2d = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        groundCheck = animator.GetComponent<EnemyHealth>().groundCheck;

        enemyDead = false;
        enemyHurt = false;
        currentEnemyHealth = maxEnemyHealth;
    }


    void Update()
    {
        //Düþman hasar aldý mý
        if(gotDamage)
        {
            //Karakter silaha göre hasar vurma switch case
            switch (characterController.silahNo)
            {
                case 1:
                    if (currentEnemyHealth > 0)
                    {
                        enemyHurt = true;
                        currentEnemyHealth -= characterController.silah1;
                    }
                    break;
                case 2:
                    if (currentEnemyHealth > 0)
                    {
                        enemyHurt = true;
                        currentEnemyHealth -= characterController.silah2;
                    }
                    break;
                case 3:
                    if (currentEnemyHealth > 0)
                    {
                        enemyHurt = true;
                        currentEnemyHealth -= characterController.silah3;
                    }
                    break;
                case 4:
                    if (currentEnemyHealth > 0)
                    {
                        enemyHurt = true;
                        currentEnemyHealth -= characterController.silah4;
                    }
                    break;
            }
            gotDamage = false;
        }
        
        //Düþman öldüyse tekrar tekrar vuruþ yapýlýlamasýn
        if (currentEnemyHealth <= 0 && sayac == 0)
        {
            sayac += 1;
            enemyDead = true;
        }          
        //"Düþman hasar alýnca" animasyon oynatýcýsý
        if (enemyHurt)
        {            
            animator.SetTrigger("isHurt");
            enemyHurt = false;
        }
        //Düþman Öldüðünde hiçbir hareket edememeli
        if (enemyDead)
        {          
            animator.SetTrigger("isDead");
            enemyDead = false;
            rgbEnemy.constraints = RigidbodyConstraints2D.FreezeAll;
            cap2d.enabled = false;
            Invoke("Destroy", 2);
        }

        //Düþman yüzünü playera çevirme
        if (currentEnemyHealth > 0)
        {
            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-5f, 5f);
            }
            else
            {
                transform.localScale = new Vector2(5f, 5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Düþman hasar aldýðýnda true dönen deðiþken
        if (collision.CompareTag("PlayerItem"))
        {
            gotDamage = true;
        }
    }
    
    //Düþman öldüðünde ekranda silen fonksiyon    
    void Destroy()
    {
        Destroy(gameObject);
        sayac = 0;
    }
  
}

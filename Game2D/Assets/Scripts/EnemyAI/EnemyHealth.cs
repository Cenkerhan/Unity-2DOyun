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
        //D��man hasar ald� m�
        if(gotDamage)
        {
            //Karakter silaha g�re hasar vurma switch case
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
        
        //D��man �ld�yse tekrar tekrar vuru� yap�l�lamas�n
        if (currentEnemyHealth <= 0 && sayac == 0)
        {
            sayac += 1;
            enemyDead = true;
        }          
        //"D��man hasar al�nca" animasyon oynat�c�s�
        if (enemyHurt)
        {            
            animator.SetTrigger("isHurt");
            enemyHurt = false;
        }
        //D��man �ld���nde hi�bir hareket edememeli
        if (enemyDead)
        {          
            animator.SetTrigger("isDead");
            enemyDead = false;
            rgbEnemy.constraints = RigidbodyConstraints2D.FreezeAll;
            cap2d.enabled = false;
            Invoke("Destroy", 2);
        }

        //D��man y�z�n� playera �evirme
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
        //D��man hasar ald���nda true d�nen de�i�ken
        if (collision.CompareTag("PlayerItem"))
        {
            gotDamage = true;
        }
    }
    
    //D��man �ld���nde ekranda silen fonksiyon    
    void Destroy()
    {
        Destroy(gameObject);
        sayac = 0;
    }
  
}

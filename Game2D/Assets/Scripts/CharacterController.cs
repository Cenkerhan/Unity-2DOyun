using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rgb;
    Vector3 velocity;
    public Animator animator;
    GiveDamage giveDamage;
    CharacterStats characterStats;
    EnemyHealth enemyHealth;

    //Silah sýrasý dizisi
    public Image[] weapons;
    internal int silah1 = 10;
    internal int silah2 = 20;
    internal int silah3 = 10;
    internal int silah4 = 20;
    internal int silahNo = 1;

    //Silahýn Eriþimi saðlandý mý
    internal bool swordAccess;
    internal bool heavySwordAccess;
    internal bool spearAccess;
    internal bool maceAccess;

    //Silah Collider
    public Collider2D swordCollider;
    public Collider2D heavySwordCollider;
    public Collider2D spearCollider;
    public Collider2D maceCollider;

    //Hýz, zýplama, yere deðiyor mu, saða bakýyor mu
    public float speedAmount;
    public float jumpAmount;
    internal bool isGrounded;
    //bool facingRight = true;

    //Saldýrý deðiþkenleri
    private bool attack;
    private float attackTimer;
    public float attackCooldown;

    private void Start()
    {

        Hýz();

        swordAccess = true;
        heavySwordAccess = false;
        spearAccess = false;
        maceAccess = false;
        weapons[0].enabled = true;        
        silahNo = 1;
        attack = false;

        rgb = GetComponent<Rigidbody2D>();
        characterStats = GetComponent<CharacterStats>();
        giveDamage = FindObjectOfType<GiveDamage>();
        enemyHealth = FindObjectOfType<EnemyHealth>();
    }

    private void Update()
    {
        //Hareket ve zýplama
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * speedAmount * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        UpdateAnimation();

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rgb.AddForce(Vector3.up * jumpAmount, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        if (animator.GetBool("isJumping") && Mathf.Approximately(rgb.velocity.y, 0))
        {
            animator.SetBool("isJumping", false);
        }

        //Karakter Döndürme
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            //facingRight = false;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            //facingRight = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        //Silah seçme resim açýp kapatma
        if (swordAccess)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                silahNo = 1;
                weapons[0].enabled = true;
                weapons[1].enabled = false;
                weapons[2].enabled = false;
                weapons[3].enabled = false;
            }
        }       
        if (heavySwordAccess)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                silahNo = 2;
                weapons[0].enabled = false;
                weapons[1].enabled = true;
                weapons[2].enabled = false;
                weapons[3].enabled = false;
            }
        }
        if (spearAccess)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                silahNo = 3;
                weapons[0].enabled = false;
                weapons[1].enabled = false;
                weapons[2].enabled = true;
                weapons[3].enabled = false;
            }
        }
        if (maceAccess)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                silahNo = 4;
                weapons[0].enabled = false;
                weapons[1].enabled = false;
                weapons[2].enabled = false;
                weapons[3].enabled = true;
            }
        }        

        //Saldýrý Fonksiyonu çaðýrma
        Attack();

        //Hasar alma animasyonu
        if (characterStats.ishurt)
        {
            animator.SetTrigger("isHurt");        
        }

        //Ölüp baþtan baþlama
        if (characterStats.isDead)
        {
            characterStats.isDead = false;
            animator.SetTrigger("isDead");
            speedAmount = 0f;
            jumpAmount = 0f;
            Invoke("Restart", 2);
        }
            
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hitCollider" && swordCollider.enabled)
        {
            enemyHealth.gotDamage = true;
        }
        if (collision.tag == "hitCollider" && heavySwordCollider.enabled)
        {
            enemyHealth.gotDamage = true;
        }
        if (collision.tag == "hitCollider" && spearCollider.enabled)
        {
            enemyHealth.gotDamage = true;
        }
        if (collision.tag == "hitCollider" && maceCollider.enabled)
        {
            enemyHealth.gotDamage = true;
        }
    }*/

    //Yerdeyken zýplama denetleyici
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            animator.SetBool("isJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            animator.SetBool("isJumping", true);
        }
    }

    //Animasyon Güncelleyici
    void UpdateAnimation()
    {
        animator.SetFloat("VelocityY", rgb.velocity.y);
    }

    //Saldýrý Fonksiyonu
    private void Attack()
    {
        if (Input.GetKeyDown("n") && !animator.GetBool("isJumping") && !attack)
        {
            attack = true;
            speedAmount = 0f;
            jumpAmount = 0f;
            if (weapons[0].enabled)
            {
                attackTimer = attackCooldown;
                animator.SetTrigger("Attacking");
                animator.SetTrigger("Sword");
            }
            if (weapons[1].enabled)
            {
                attackTimer = attackCooldown;
                animator.SetTrigger("Attacking");
                animator.SetTrigger("HeavySword");
            }
            if (weapons[2].enabled)
            {
                attackTimer = attackCooldown;
                animator.SetTrigger("Attacking");
                animator.SetTrigger("Spear");
            }
            if (weapons[3].enabled)
            {
                attackTimer = attackCooldown;
                animator.SetTrigger("Attacking");
                animator.SetTrigger("Mace");
            }
        }
        if (attack)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attack = false;
                Hýz();
            }
        }     
    }
    void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("SampleScene");
    }

    void Hýz()
    {
        speedAmount = 6f;
        jumpAmount = 12f;
    }
} 




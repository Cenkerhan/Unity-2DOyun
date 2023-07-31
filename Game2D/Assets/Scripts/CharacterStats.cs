using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    //UI
    public Image[] kalpler;


    GiveDamage giveDamage;
    Animator animator;
    Animator enemyAnimator;

    internal bool ishurt;
    internal bool isDead;
    internal int potCount;
    public int health = 10;
    int maxHealth = 10;

    public void Damage(int amount)
    {
        //Kalp sýradaki image kapat
        kalpler[health - 1].enabled = false;
    }

    public void Regeneration(int amount)
    {
        //Can arttýrma
        health += amount;
        for(int i = 0; i < health; i++)
        {
            kalpler[i].enabled = true;
        }
        potCount--;
    }
    void Start()
    {
        giveDamage = FindObjectOfType<GiveDamage>();
        animator = GetComponent<Animator>();
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        enemyAnimator = enemyObject.GetComponent<Animator>();
        
        health = maxHealth;
        potCount = 3;
        isDead = false;
    }
    void Update()
    {
        if (health > maxHealth)
            health = maxHealth;

        ReduceHealth();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (health < 10 && potCount > 0)
            {
                Regeneration(1);
            }
        }

    }

    //Can azaltma fonksiyonu
    void ReduceHealth()
    {
        if(health > 0)
        {
            if (ishurt)
            {
                health -= giveDamage.damage;
                for (int i = 0; i <= giveDamage.damage - 1; i++)
                {
                    kalpler[1 + health - i].enabled = false;
                }
                ishurt = false;
                if (health <= 0)
                {
                    isDead = true;
                    enemyAnimator.SetBool("playerDead", true);
                }
            }
        }      
    }

}

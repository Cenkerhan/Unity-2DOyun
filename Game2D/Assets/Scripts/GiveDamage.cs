using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    CharacterStats playerHealth;
    public int damage;

    void Start()
    {
        playerHealth = FindObjectOfType<CharacterStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Oyuncuya zarar ver
        if(collision.tag == "Player")
        {
            playerHealth.ishurt = true;
        }
    }
}

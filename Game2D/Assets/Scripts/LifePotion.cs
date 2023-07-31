using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotion : MonoBehaviour
{
    CharacterStats characterStats;
    // Start is called before the first frame update
    void Start()
    {
        characterStats = FindObjectOfType<CharacterStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (characterStats.potCount < 10)
            {
                characterStats.potCount++;
                Destroy(gameObject);
            }
        }
    }
}

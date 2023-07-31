using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifePotCount : MonoBehaviour
{
    public TextMeshProUGUI myText;
    CharacterStats myCharacterStats;

    void Start()
    {
        myCharacterStats = FindObjectOfType<CharacterStats>();
    }

    void Update()
    {
        myText.text = ": " + myCharacterStats.potCount.ToString();
    }
}

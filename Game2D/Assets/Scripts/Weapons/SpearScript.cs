using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    CharacterController characterController;
    private bool pickUpAllowed;
    private void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
    }
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
            PickUp();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            GameObject childObject = transform.Find("press3").gameObject;
            SpriteRenderer childRenderer = childObject.GetComponent<SpriteRenderer>();
            childRenderer.enabled = true;
            pickUpAllowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            GameObject childObject = transform.Find("press3").gameObject;
            SpriteRenderer childRenderer = childObject.GetComponent<SpriteRenderer>();
            childRenderer.enabled = false;
            pickUpAllowed = false;
        }
    }
    void PickUp()
    {
        characterController.spearAccess = true;
        Destroy(gameObject);
    }
}

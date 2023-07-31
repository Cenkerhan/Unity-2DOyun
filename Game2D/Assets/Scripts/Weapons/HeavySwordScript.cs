using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HeavySwordScript : MonoBehaviour
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
            GameObject childObject = transform.Find("press2").gameObject;
            SpriteRenderer childRenderer = childObject.GetComponent<SpriteRenderer>();
            childRenderer.enabled = true;
            pickUpAllowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            GameObject childObject = transform.Find("press2").gameObject;
            SpriteRenderer childRenderer = childObject.GetComponent<SpriteRenderer>();
            childRenderer.enabled = false;
            pickUpAllowed = false;
        }
    }
    void PickUp()
    {
        characterController.heavySwordAccess = true;
        Destroy(gameObject);
    }
}

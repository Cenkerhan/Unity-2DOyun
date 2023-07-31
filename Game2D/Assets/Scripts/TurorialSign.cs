using TMPro;
using UnityEngine;

public class TurorialSign : MonoBehaviour
{
    public TextMeshPro myText;
    public string turtorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            myText.text = turtorialText;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        myText.text = null;
    }
}

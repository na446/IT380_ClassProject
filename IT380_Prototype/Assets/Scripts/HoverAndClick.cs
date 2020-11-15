using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls when interactbales should do in the scene when clicked/hovered on.
public class HoverAndClick : MonoBehaviour
{
    public bool clicked = false;

    private SpriteRenderer currPic;
    public Sprite beforePic;
    public Sprite newSprite;
    public Sprite hoverSprite;

    void Hovering()
    {
        currPic.sprite = hoverSprite;
    }

    void ChangeSprite()
    {
        currPic.sprite = newSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        currPic = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()//If hovering over interactable, make item glow.
    {
        Hovering();
    }

    void OnDisable()
    {
        currPic.sprite = beforePic;
    }

    void OnMouseExit()
    {
        currPic.sprite = beforePic;
    }

    private void OnMouseDown()//If mouse clicked on interactable, make item do action.
    {
        if (gameObject.CompareTag("Interactable") && gameObject.GetComponent<AudioSource>() != null)
                gameObject.GetComponent<AudioSource>().Play();
        if (gameObject.CompareTag("Interactable") && gameObject.name == "Pillow")
            StartCoroutine(tiltThis());
        ChangeSprite();
        clicked = true;
    }

    IEnumerator tiltThis()
    {
        transform.RotateAround(transform.position, Vector3.forward, 30f);
        yield return new WaitForSeconds(0.5f);
        transform.RotateAround(transform.position, Vector3.forward, -30f);
    }

    private void OnMouseUp()
    {
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

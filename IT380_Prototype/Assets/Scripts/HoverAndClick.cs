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

    void ChangeSprite()
    {
        currPic.sprite = newSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        currPic = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()//If hovering over interactable, make item glow.
    {
        currPic.color = Color.yellow;
    }

    void OnDisable()
    {
        currPic.color = Color.white;
    }

    void OnMouseExit()
    {
        currPic.color = Color.white;
    }

    private void OnMouseDown()//If mouse clicked on interactable, make item do action.
    {
        if (gameObject.CompareTag("Interactable"))
            ChangeSprite();
        clicked = true;
    }

    private void OnMouseUp()
    {
        if (gameObject.CompareTag("Interactable"))
            currPic.sprite = beforePic;
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

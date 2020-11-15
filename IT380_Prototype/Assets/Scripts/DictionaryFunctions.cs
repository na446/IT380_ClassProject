using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This script is resposible for navigating the dictionary and showing only the letters and words the user has learned.
public class DictionaryFunctions : MonoBehaviour
{
    [HideInInspector] public bool clickedDic = false;
    [HideInInspector] public bool clickedBack = false;

    public TMP_Text line1;
    public TMP_Text line2;
    public TMP_Text line3;
    public TMP_Text line4;

    public Image currDef;

    public void dicPress()
    {
        clickedDic = true;
    }

    public void backPress()
    {
        clickedBack = true;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    public void getImage()
    {
        string LText = "";

        if (line1.text == "Alef")
            LText = "alef";
        else
            LText = "0";
        currDef.sprite = Resources.Load<Sprite>("pos/pos_" + LText);
    }

    public void notLearned()
    {
        currDef.sprite = Resources.Load<Sprite>("pos/pos_0");
    }

    public void firstTab()
    {
        getImage();
        line1.text = "Alef";
        line2.text = "Baa";
        line3.text = "Taa";
        line4.text = "Thaa";
    }

    public void lockedTab()
    {
        notLearned();
        line1.text = "WIP";
        line2.text = "WIP";
        line3.text = "WIP";
        line4.text = "WIP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

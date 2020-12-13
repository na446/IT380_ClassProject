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

    //Dictionary of strings that records the users progress of what letters they've learned so far.
    //Ideally this will later expand to values of final, medial, inital, and isolated -- so that letter wont be unlocked unless user traces the different positions the user traced.
    public static Dictionary<string, string> progressDic = new Dictionary<string, string>()
    {
        { "Alef", "Unlocked" },
        { "Baa", "Locked" },
        { "Taa", "Locked" },
        { "Thaa", "Locked" },
        { "Jeem", "Locked" },
        { "Hah", "Locked" },
        { "Khah", "Locked" },
        { "Dal", "Locked" },
        { "Thal", "Locked" },
        { "Raa", "Locked" },
        { "Zaay", "Locked" },
        { "Seen", "Locked" },
        { "Sheen", "Locked" },
        { "Saad", "Locked" },
        { "Daad", "Locked" },
        { "(Th)aa", "Locked" },
        { "Dhaa", "Locked" },
        { "Ayn", "Locked" },
        { "Ghayn", "Locked" },
        { "Faa", "Locked" },
        { "Qaaf", "Locked" },
        { "Kaaf", "Locked" },
        { "Laam", "Locked" },
        { "Meem", "Locked" },
        { "Noon", "Locked" },
        { "Haa", "Locked" },
        { "Waaw", "Locked" },
        { "Yaa", "Locked" }
    };

    public TMP_Text line1;
    public TMP_Text line2;
    public TMP_Text line3;
    public TMP_Text line4;

    public Image currDef;

    public PlaySound playSound;

    public void dicPress()
    {
        clickedDic = true;
        clickedBack = false;
        gameObject.SetActive(true);
    }

    public void backPress()
    {
        clickedDic = false;
        clickedBack = true;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "03Level01Scene" || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "KitchenScene")
            progressDic["Alef"] = "Unlocked";
        /*else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "FrontYardScene" || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ForestScene")
        {
            progressDic["Alef"] = "Unlocked";
            progressDic["Baa"] = "Unlocked";
        }*/

        firstTab();
    }

    public void learnedLetter(string letter)
    {
        dictionaryprogress dicProg = GameObject.Find("DictionaryProgress").GetComponent<dictionaryprogress>();
        progressDic[letter] = "Unlocked";
        dicProg.DicUpdate(letter);
    }

    public void getImage(TMP_Text line)
    {
        string currState = "";

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "02TutorialScene")
        {
            if (progressDic.TryGetValue(line.text, out currState) && currState == "Unlocked")
            {
                playSound.changeSound(line.text);
                try
                {
                    currDef.sprite = Resources.Load<Sprite>("pos/pos_" + line.text.ToLower());
                }
                catch (System.Exception)
                {
                    Debug.Log("File is not in Resources folder.");
                }
            }
            else
            {
                playSound.changeSound(null);
                notLearned();
            }
        }
        else
        {
            dictionaryprogress dicProg = GameObject.Find("DictionaryProgress").GetComponent<dictionaryprogress>();

            if (dicProg.checkLetter(line.text, currState))
            {
                playSound.changeSound(line.text);
                try
                {
                    currDef.sprite = Resources.Load<Sprite>("pos/pos_" + line.text.ToLower());
                }
                catch (System.Exception)
                {
                    Debug.Log("File is not in Resources folder.");
                }
                if (dicProg.checkedLetter == line.text)
                    dicProg.letterChecked();
            }
            else
            {
                playSound.changeSound(null);
                notLearned();
            }
        }
    }

    public void notLearned()
    {
        currDef.sprite = Resources.Load<Sprite>("pos/pos_0");
    }

    public void firstTab()
    {
        line1.text = "Alef";
        line2.text = "Baa";
        line3.text = "Taa";
        line4.text = "Thaa";
        getImage(line1);
    }

    public void twoTab()
    {
        line1.text = "Jeem";
        line2.text = "Hah";
        line3.text = "Khah";
        line4.text = "Dal";
        getImage(line1);
    }

    public void threeTab()
    {
        line1.text = "Thal";
        line2.text = "Raa";
        line3.text = "Zaay";
        line4.text = "Seen";
        getImage(line1);
    }

    public void fourTab()
    {
        line1.text = "Sheen";
        line2.text = "Saad";
        line3.text = "Daad";
        line4.text = "(Th)aa";
        getImage(line1);
    }

    public void fiveTab()
    {
        line1.text = "Dhaa";
        line2.text = "Ayn";
        line3.text = "Ghayn";
        line4.text = "Faa";
        getImage(line1);
    }

    public void sixTab()
    {
        line1.text = "Qaaf";
        line2.text = "Kaaf";
        line3.text = "Laam";
        line4.text = "Meem";
        getImage(line1);
    }

    public void sevenTab()
    {
        line1.text = "Noon";
        line2.text = "Haa";
        line3.text = "Waaw";
        line4.text = "Yaa";
        getImage(line1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

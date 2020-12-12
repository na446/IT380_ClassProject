using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dictionaryprogress : MonoBehaviour
{
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

    public string checkedLetter;
    public GameObject newLetter;

    public void DicUpdate(string letter)
    {
        if (!checkLetter(letter, ""))
        {
            progressDic[letter] = "Unlocked";
            checkedLetter = letter;
            newLetter.SetActive(true);
        }
    }

    public bool checkLetter(string line, string currState)
    {
        if (progressDic.TryGetValue(line, out currState) && currState == "Unlocked")
        {
            return true;
        }
        else
            return false;
    }

    public void letterChecked()
    {
        checkedLetter = "none";
        newLetter.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "03Level01Scene")
            checkedLetter = "none";
        newLetter = GameObject.Find("New Letter");
        newLetter.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "00TitleScene")
            Destroy(gameObject);

        if (newLetter == null)
        {
            newLetter = GameObject.Find("New Letter");
            if (checkedLetter == "none")
                newLetter.SetActive(false);
        }
    }
}

using System.Collections;
using UnityEngine;
using TMPro;

//This script keeps track of the players progress in the tutorial.
public class TutorialProgress : MonoBehaviour
{
    public GameObject dir_1;
    public GameObject dir_2;
    public GameObject dir_3;
    public GameObject dir_4;
    public GameObject items;

    [HideInInspector]public bool audioPlayed = false;//Boolean for other scripts to use.

    public GameObject dTab; //Place tab that opens dictionary in inspector.
    public GameObject backTab; //Place tab that opens back button in inspector.

    public GameObject pen;//Place pen that allows user to write in inspector.

    public Rigidbody2D m_Rigidbody2D; //Place rigidbody of character in inspector. 
    public GameObject task; //Place writing task game object in inspector. 
    public GameObject taskUI; //Place writing task game object (UI) in inspector.
    public GameObject dictUI; //Place dictionary UI in inspector.

    private PlayerMovement player;//References to PlayerMovement script.

    public HoverAndClick hac;//Reference to HoverAndClick script from Item 1; put in inspector
    public HoverAndClick hac1;//Reference to HoverAndClick script from Item 2; put in inspector
    public HoverAndClick hac2;//Reference to HoverAndClick script from Item 3; put in inspector
    public HoverAndClick hac3;//Reference to HoverAndClick script from Desk; put in inspector

    public WritingTask wTask;//Reference to WritingTask script from scene; put in inspector
    public DictionaryFunctions dFun;//References to DictionaryFunctions script; put in inspector

    public GameObject settinPanel;
    public GameObject UIText;
    private TMP_Text instructText;//Reference to instructions in writing task.

    private bool okPressed = false;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        instructText = UIText.GetComponentInChildren<TMP_Text>();
    }

    public void OKButtonPressed()
    {
        okPressed = true;
    }

    public void showSettings()
    {
        if (settinPanel.activeInHierarchy)
            settinPanel.SetActive(false);
        else
            settinPanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        dir_1.SetActive(true);
        dir_2.SetActive(false);
        dir_3.SetActive(false);
        dir_4.SetActive(false);
        hac3.enabled = false;
        dTab.SetActive(false);
        backTab.SetActive(false);
        task.SetActive(false);
        taskUI.SetActive(false);
        dictUI.SetActive(false);
        pen.SetActive(false);
        instructText.text = "";
        StartCoroutine(basicInstructions());
        UIText.SetActive(false);
    }

    IEnumerator basicInstructions()
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => (m_Rigidbody2D.velocity.magnitude > 0));//Wait until player is moving.
        dir_1.SetActive(false);
        dir_2.SetActive(true);
        yield return new WaitUntil(() => (hac.clicked || hac1.clicked || hac2.clicked));//Wait until player clicks on an interactable
        dir_2.SetActive(false);
        dir_3.SetActive(true);
        hac3.enabled = true;
        yield return new WaitUntil(() => (hac3.clicked));//Wait until player clicks on desk
        dir_3.SetActive(false);
        StartCoroutine(writingTutorial());
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player finsihed writing tutorial
        yield return new WaitForSeconds(2f);
        dir_4.SetActive(true);
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        TMP_Text dir4Txt = dir_4.GetComponentInChildren<TMP_Text>();
        dir4Txt.text = "Writing tasks appear when you interact with the envirnoment.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        dir4Txt.text = "There are 2 types of writing tasks: one were it asks you to trace, and ...";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        dir4Txt.text = "another where it'll ask you to write a letter/position from memory.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        dir4Txt.text = "I'll ask you to the write Alef in it's final form in a bit.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        dir4Txt.text = "When you complete a writing task. The letter you just learned gets recorded in your Dictionary.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        Transform OKbutton = dir_4.transform.Find("Button");
        OKbutton.gameObject.SetActive(false);
        dir4Txt.text = "Click on the Dictionary tab to review  learned letters.";
        dTab.SetActive(true);
        yield return new WaitUntil(() => (dFun.clickedDic));//Wait for user to click on dictionary tab
        StartCoroutine(dicTutorial());
        yield return new WaitUntil(() => (dFun.clickedBack));//Wait for user to click on back tab
        dir_4.SetActive(true);
        OKbutton.gameObject.SetActive(true);
        dir4Txt.text = "As you progress, you'll unlock more letters in the dictionary!";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        dir4Txt.text = "Time for a pop quiz!";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        StartCoroutine(popQuiz());
        yield return new WaitUntil(() => (wTask.inOrder));
        yield return new WaitForSeconds(2f);
        dir_4.SetActive(true);
        dir4Txt.text = "Tutorial complete!";//Replace with decal
        yield return new WaitUntil(() => (okPressed));
        UnityEngine.SceneManagement.SceneManager.LoadScene("00TitleScene");
    }

    IEnumerator writingTutorial()
    {
        player.enabled = false;
        task.SetActive(true);
        taskUI.SetActive(true);
        items.SetActive(false);
        UIText.SetActive(true);
        Transform arw = UIText.transform.Find("Arrow");
        Transform OKbutton = UIText.transform.Find("Button");
        wTask.setTask("Trace", "Alef", "isolated");
        instructText.text = "Welcome to the writing task! Press OK to continue!";//Press the sound button to hear the letter.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        arw.gameObject.SetActive(true);
        instructText.text = "Unlike English, Arabic is written from right to left. So always start on the right-hand side of the letter.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        arw.gameObject.SetActive(false);
        instructText.text = "Arabic letters connect to form words like cursive.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        instructText.text = "Because of this the same letter has different forms depending on where it's placed in the word.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        instructText.text = "Most letters have four different forms: isolated, initial, medial, and final.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        arw = UIText.transform.Find("Arrow1");
        arw.gameObject.SetActive(true);
        instructText.text = "Here is Alef, the first letter of the Arabic Alphabet in it's isolated form.";
        yield return new WaitUntil(() => (okPressed));
        arw.gameObject.SetActive(false);
        okPressed = false;
        instructText.text = "You use the isolated form of a letter when its written by itself and not in a word.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        instructText.text = "There are 28 letters in the Arabic alphabet & some don't have an equivalent sound in English.";
        yield return new WaitUntil(() => (okPressed));
        OKbutton.gameObject.SetActive(false);//remove OK button to force user to click on sound button against their will
        okPressed = false;
        instructText.text = "But in this case, Alef sounds like our letter A! Click on the sound button to hear it!";
        yield return new WaitUntil(() => (audioPlayed));//Wait until player presses button
        instructText.text = "Time to trace Alef! Remember you can always click on the trash button to clear the canvas again.";
        OKbutton.gameObject.SetActive(true);
        yield return new WaitUntil(() => (okPressed));//Wait until player presses button
        OKbutton.gameObject.SetActive(false);
        okPressed = false;
        pen.SetActive(true);
        arw = UIText.transform.Find("Arrow");
        arw.gameObject.SetActive(true);
        instructText.text = "Trace the letter by holding down the left mouse button across the canvas.";
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player traces letter completetly.
        yield return new WaitForSeconds(2f);
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        UIText.SetActive(false);
        wTask.DeleteLines();
        player.enabled = true;
        task.SetActive(false);
        taskUI.SetActive(false);
        items.SetActive(true);
    }

    IEnumerator dicTutorial()
    {
        player.enabled = false;
        items.SetActive(false);
        dir_4.SetActive(false);
        instructText.text = "Letters have different forms depending on where it's placed in the word (1/3)...";
        yield return new WaitForSeconds(5f);
        instructText.text = "Most letters have 4 different forms: isolated, initial, medial, & final (2/3)...";
        yield return new WaitForSeconds(5f);
        instructText.text = "Use this dicitonary to review positions & assoicate them to words. (3/3)";
        yield return new WaitUntil(() => (dFun.clickedBack));//Wait for user to click on back tab
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        player.enabled = true;
        items.SetActive(true);
    }

    IEnumerator popQuiz()
    {
        dir_4.SetActive(false);
        player.enabled = false;
        task.SetActive(true);
        taskUI.SetActive(true);
        items.SetActive(false);
        //test user on writing the letter in the final form.
        wTask.setTask("Write", "Alef", "final");
        yield return new WaitUntil(() => (wTask.inOrder));
        yield return new WaitForSeconds(2f);
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        wTask.DeleteLines();
        player.enabled = true;
        task.SetActive(false);
        taskUI.SetActive(false);
        items.SetActive(true);
    }
}

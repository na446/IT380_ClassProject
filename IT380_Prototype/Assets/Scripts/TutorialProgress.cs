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
    public TMP_Text instructText;//Reference to instructions in writing task.

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
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
        instructText.text = "";
        dir_4.SetActive(true);
        dTab.SetActive(true);
        yield return new WaitUntil(() => (dFun.clickedDic));//Wait for user to click on dictionary tab
        StartCoroutine(dicTutorial());
        yield return new WaitUntil(() => (dFun.clickedBack));//Wait for user to click on back tab
        instructText.text = "Tutorial complete!";//Replace with decal
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("00TitleScene");
    }

    IEnumerator writingTutorial()
    {
        player.enabled = false;
        task.SetActive(true);
        taskUI.SetActive(true);
        items.SetActive(false);
        instructText.text = "Welcome to the writing task! Press the sound button to hear the letter.";
        yield return new WaitUntil(() => (audioPlayed));//Wait until player presses button
        pen.SetActive(true);
        instructText.text = "Trace the letter by holding down the left mouse button across the canvas.";
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player traces letter completetly.
        yield return new WaitForSeconds(2f);
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        wTask.DeleteLines();
        wTask.DeleteLines();
        player.enabled = true;
        task.SetActive(false);
        taskUI.SetActive(false);
        items.SetActive(true);
    }

    IEnumerator dicTutorial()
    {
        player.enabled = false;
        dictUI.SetActive(true);
        items.SetActive(false);
        dTab.SetActive(false);
        backTab.SetActive(true);
        dir_4.SetActive(false);
        instructText.text = "Letters have different forms depending on where it's placed in the word (1/3)...";
        yield return new WaitForSeconds(5f);
        instructText.text = "Most letters have 4 different forms: isolated, initial, medial, & final (2/3)...";
        yield return new WaitForSeconds(5f);
        instructText.text = "Use this dicitonary to review positions & assoicate them to words. (3/3)";
        yield return new WaitUntil(() => (dFun.clickedBack));//Wait for user to click on back tab
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        player.enabled = true;
        dictUI.SetActive(false);
        items.SetActive(true);
        dTab.SetActive(true);
        backTab.SetActive(false);
        dir_4.SetActive(false);
    }
}

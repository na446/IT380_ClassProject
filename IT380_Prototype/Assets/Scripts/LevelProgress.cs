using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    private PlayerMovement player;//References to PlayerMovement script.

    public GameObject items; //Place all game items/sprites in the inspector.
    public GameObject task; //Place writing task game object in inspector. 
    public GameObject taskUI; //Place writing task game object (UI) in inspector.

    public GameObject levelDone;

    public GameObject UIText;
    private TMP_Text storyTxt;

    public HoverAndClick taskObj;

    private bool okPressed = false;
    public WritingTask wTask;//Reference to WritingTask script from scene; put in inspector

    private GameObject dicButton;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        storyTxt = UIText.GetComponentInChildren<TMP_Text>();
        dicButton = GameObject.Find("Dictionary Button");
    }

    public void OKButtonPressed()
    {
        okPressed = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "KitchenScene")
            StartCoroutine(level1());
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ForestScene")
            StartCoroutine(level1p2());
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OutsideCaveScene")
            StartCoroutine(level1p3());
    }

    IEnumerator level1()
    {
        storyTxt.text = "MOM\n\nGo gather sticks for the fire. We need more for dinner tonight.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        storyTxt.text = "ALIBABA\n\nYes, Mother. [Click on the door to leave the house.]";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        UIText.SetActive(false);

        yield return new WaitUntil(() => (taskObj.clicked));//Wait until player clicks on desk
        StartCoroutine(writingBaa());
        dicButton.SetActive(false);
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player finsihed writing task
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrontYardScene");
    }

    IEnumerator level1p2()
    {
        storyTxt.text = "ALIBABA\n\nA rabbit! If I catch this, we will have meat for dinner!";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        UIText.SetActive(false);

        yield return new WaitUntil(() => (taskObj.clicked));//Wait until player clicks on desk
        dicButton.SetActive(false);
        StartCoroutine(writingAlef());
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player finsihed writing task
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("OutsideCaveScene");
    }

    IEnumerator level1p3()
    {
        storyTxt.text = "ALIBABA\n\nCaught you! Mother will be so happy.";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        storyTxt.text = "ALIBABA\n\nHuh? What’s this? Are those theives?";
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        UIText.SetActive(false);
        //Show level complete picture and then load main menu screen.
        levelDone.SetActive(true);
        yield return new WaitUntil(() => (okPressed));
        okPressed = false;
        //wait until player presses next level button to load scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("00TitleScene");
    }

    IEnumerator writingAlef()
    {
        player.enabled = false;
        task.SetActive(true);
        taskUI.SetActive(true);
        items.SetActive(false);
        wTask.setTask("Write", "Alef", "isolated");
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player traces letter completetly.
        dicButton.SetActive(true);
        yield return new WaitForSeconds(2f);
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        UIText.SetActive(false);
        wTask.DeleteLines();
        player.enabled = true;
        task.SetActive(false);
        taskUI.SetActive(false);
        items.SetActive(true);
    }

    IEnumerator writingBaa()
    {
        player.enabled = false;
        task.SetActive(true);
        taskUI.SetActive(true);
        items.SetActive(false);
        wTask.setTask("Trace", "Baa", "isolated");
        yield return new WaitUntil(() => (wTask.inOrder));//Wait until player traces letter completetly.
        dicButton.SetActive(true);
        yield return new WaitForSeconds(2f);
        //turns writing task objects off and ingame items on; quits and continues basicInstructions
        UIText.SetActive(false);
        wTask.DeleteLines();
        player.enabled = true;
        task.SetActive(false);
        taskUI.SetActive(false);
        items.SetActive(true);
    }
}

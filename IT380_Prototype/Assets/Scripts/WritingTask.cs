using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is responisble for creating lines, deleting lines, and checking if the lines match with the given letter/word.
public class WritingTask : MonoBehaviour
{
    public GameObject ink;//a linerender prefab (w/ an edge collider) that will instantiate everytime user creates dots or lines
    public GameObject taskWrong;//references a gameobj that displays an error when task is completed incorrectly
    [HideInInspector] public GameObject currLine;
    public GameObject taskDone; //references a gameobj that displays "Good job!" when task is complete

    public GameObject dir; //references a gameobject that displays directions like "trace" or "write" to the user
    public Image pic; //references a gameobject that displays the letter when the user needs to trace
    public GameObject tracePts; //references a gameobject that displays trace points
    //public GameObject tracePts; //references image that displays tracing picture that user can use a as guideline

    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public EdgeCollider2D edgeCollider;
    [HideInInspector] public List<Vector2> fingerPos;

    private List<int> pointsPressed = new List<int>();
    //private List<int> answerKey = new List<int>() {0,1,2,3,4,5,6,7,8,9,10,11};//this should just be changed to an int and count how many points/children there under the trace gameobject.

    [HideInInspector] public bool inOrder;
    private bool startPressed = false;
    private int isSuffering = 0;//keeps track of how many errors the user made during the writing task
    private string currLetter; //keeps track of what letter is assigned this time
    private string currDirection; //stores if the writing task is tracing or a memory task
    private string currPos; //stores position that is chosen for the current writing task

    public DictionaryFunctions dicFunc;

    public void Awake()
    {
        inOrder = false;
        startPressed = false;
        taskDone.SetActive(false);
        taskWrong.SetActive(false);
        isSuffering = 0;
    }

    void OnDisable()
    {
        taskDone.SetActive(false);
        taskWrong.SetActive(false);
        inOrder = false;
        startPressed = false;
        isSuffering = 0;
    }

    IEnumerator displayDone()
    {
        DeleteLines();
        taskDone.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        taskDone.SetActive(false);
    }
    
    //Displays what error the user has made when writing
    IEnumerator displayErr(string message)
    {
        isSuffering++;
        DeleteLines();
        taskWrong.SetActive(true);
        TMPro.TMP_Text writeError = taskWrong.GetComponent<TMPro.TMP_Text>();
        writeError.text = message;
        if (isSuffering >= 10 && currDirection == "Write")
            showTracing();
        yield return new WaitForSeconds(2.5f);
        taskWrong.SetActive(false);
    }

    /// <summary>
    /// A public method that can be called from other scripts to assign writing tasks to user. None of the paramaeters can be null.
    /// </summary>
    /// <param name="direction">Decides if the writing task should display a tracing guide or not AND changes the writing tasks instructions accordingly.</param>
    /// <param name="letter">Displays what letter the user will be tracing.</param>
    /// <param name="position">Displays what position the letter will be in: isolated, inital, medial, or final.</param>
    public void setTask(string direction, string letter, string position)
    {
        Text writtenDir = dir.GetComponent<Text>();
        SpriteRenderer picTrace = tracePts.GetComponent<SpriteRenderer>();
        currDirection = direction;
        currLetter = letter;
        currPos = position;
        switch (direction)
        {
            case "Trace":
                writtenDir.text = "Trace:";
                pic.sprite = Resources.Load<Sprite>("Letters/" + letter);

                picTrace.sprite = Resources.Load<Sprite>("Letters/Traced/" + letter +"_"+position+"_"+"TRACE");
                picTrace.color = new Color(1, 1, 1, 0.8f); 
                break;
            case "Write":
                pic.sprite = Resources.Load<Sprite>("Letters/none");
                writtenDir.text = "Write " + letter + " in it's " + position + " form";

                picTrace.sprite = Resources.Load<Sprite>("Letters/Traced/noneTRACE");
                if (currLetter == "Alef" && currPos == "isolated")
                    picTrace.sprite = Resources.Load<Sprite>("Letters/Traced/noneALEF");
                break;

            default:
                break;
        }
        PlaySound playSound = GameObject.Find("Sound Button").GetComponent<PlaySound>();
        playSound.changeLetter(letter);

        foreach (Transform child in tracePts.transform)
        {
            if (child.name == letter + "_" + position)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }
    
    //Displays tracing image after 3 or so more failed attempts
    void showTracing()
    {
        SpriteRenderer picTrace = tracePts.GetComponent<SpriteRenderer>();
        picTrace.sprite = Resources.Load<Sprite>("Letters/Traced/" + currLetter + "_" + currPos + "_" + "TRACE");
        picTrace.color = new Color(1, 1, 1, 0.8f);
        isSuffering = 0;
    }

    //Adds the tracing points the user presses. 
    public void TracingPointPressed(int ptNum)
    {
        bool alreadyExist = pointsPressed.Contains(ptNum);
        if (!alreadyExist)
            pointsPressed.Add(ptNum);
    }

    //Call this when you want to validate the order of the points
    public bool ValidatePressedOrder()
    {
        //check that we have
        bool pointsInOrder = true;

        //check that we have pressed each Tracing point, otherwise it can't possibly be in order
        //if (pointsPressed.Count == answerKey.Count)
        //{
            
            for (int i = 0; i < pointsPressed.Count; ++i)
            {
                //Debug.Log(pointsPressed[i]);
                if (pointsPressed[i] != i)
                {
                    pointsInOrder = false;
                    break;
                }
            }
        //}
        //else
        //{
            //pointsInOrder = false;
        //}

        //Debug.Log("Valid order: " + pointsInOrder);
        if (pointsInOrder && startPressed)
        {
            StartCoroutine(displayDone());
            isSuffering = 0;
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "02TutorialScene")
                dicFunc.learnedLetter(currLetter);
        }

        return pointsInOrder;
    }
    
    //Find all created lines in scene and delete them
    public void DeleteLines()
    {
        foreach (GameObject lines in GameObject.FindGameObjectsWithTag("Line"))
            Destroy(lines);
    }

    void CreateDot()
    {
        currLine = Instantiate(ink, Vector3.zero, Quaternion.identity);
        lineRenderer = currLine.GetComponent<LineRenderer>();
        edgeCollider = currLine.GetComponent<EdgeCollider2D>();
        fingerPos.Clear();
        //Even if the player taps the screen, that dot has 2 poistions so we must set our 2 first values to that current finger position
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);
        try { edgeCollider.points = fingerPos.ToArray(); }
        catch (System.ArgumentOutOfRangeException) { }
    }

    //If more than a dot, keep conituning drawing line based on position
    void CreateLine(Vector2 newFingerPos)
    {
        fingerPos.Add(newFingerPos);
        try {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        }
        catch (MissingReferenceException) { }
    }

    // Update is called once per frame
    void Update()
    {
        //Draw line
        try
        {
            if (Input.GetMouseButtonDown(0))//If user clicks left button
            {
                //Checks if user is drawing on canvas, then create line
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                try
                {
                    //Debug.Log(hit.collider.transform.name);
                    if (hit.collider.CompareTag("Writeable") || hit.collider.CompareTag("Point"))
                    {
                        CreateDot();
                        //Debug.Log(hit.collider.name);
                        //If user hits tracing point, add to list
                        if (hit.collider.CompareTag("Point"))
                        {
                            string ptStr = hit.collider.transform.name;
                            if (ptStr == "Start00")
                                startPressed = true;
                            int ptNum = int.Parse(ptStr.Substring(ptStr.Length - 2));
                            TracingPointPressed(ptNum);

                            //If tracing point is last, check if user traced correctly
                            string isLast = ptStr.Substring(0, ptStr.Length - 2);
                            if (isLast == "Last")
                            {
                                if (startPressed == false)
                                    StartCoroutine(displayErr("Wrong starting point. Remember, write right to left."));
                                else
                                    inOrder = ValidatePressedOrder();

                                //Delete lines regardless if in correct order or not; but display different messages depending on incorrect order or not
                                DeleteLines();

                                //if false empty the list for retry
                                if (!inOrder && startPressed){
                                    pointsPressed.Clear();
                                    StartCoroutine(displayErr("Not neat enough.Try again!"));
                                }
                                //if last was the first thing pressed; empty list and tell user "wrong starting point" 
                            }
                        }
                    }

                }
                catch (System.NullReferenceException) { }
            }

            if (Input.GetMouseButton(0)) //If user is holding left mouse button down
            {
                //Checks if user is drawing on canvas, then update line
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                try
                {
                    //Debug.Log(hit.collider.transform.name);
                    if (hit.collider.CompareTag("Writeable") || hit.collider.CompareTag("Point"))
                    {
                        //Checks if current finger position is greatly different from the previous finger position
                        Vector2 currFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        if (Vector2.Distance(currFingerPos, fingerPos[fingerPos.Count - 1]) > .1f)
                        {
                            CreateLine(currFingerPos);
                            //Debug.Log(hit.collider.name);
                            //If user hits tracing point, add to list
                            if (hit.collider.CompareTag("Point"))
                            {
                                string ptStr = hit.collider.transform.name;
                                int ptNum = int.Parse(ptStr.Substring(ptStr.Length - 2));
                                TracingPointPressed(ptNum);
                                if (ptStr == "Start00")
                                    startPressed = true;
                                //If tracing point is last, check if user traced correctly
                                string isLast = ptStr.Substring(0, ptStr.Length - 2);
                                if (isLast == "Last")
                                {
                                    if (startPressed == false)
                                        StartCoroutine(displayErr("Wrong starting point. Remember, write right to left."));
                                    else
                                        inOrder = ValidatePressedOrder();

                                    DeleteLines();
                                    //if false empty the list for retry
                                    if (!inOrder && startPressed)
                                    {
                                        pointsPressed.Clear();
                                        StartCoroutine(displayErr("Not neat enough. Try again!"));
                                    }
                                    //if last was the first thing pressed; empty list and tell user "wrong starting point" 
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException) { }

            }
        }
        catch (System.ArgumentOutOfRangeException) { }

        //Follow mouse and act as the pen's cursour 
        Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//This script is responsible for moving between scenes and slides in the Introduction scene.
public class UIFunctions : MonoBehaviour
{
    [Header("Title Scene Properties", order = 0)]
    [Space(2f)]
    [Header("Intro Scene Properties", order = 1)]
    //References the image used in the canvas of the Intro scene
    public GameObject introImage;
    //References the back button in the scene; Used to toggle if the back button is being used or not.
    public GameObject backButton;
    //References the next button in the scene; Used to toggle if the next button is being used or not.
    public GameObject nextButton;
    //Changes text in the Intro scene.
    public TMP_Text introText;
    [Header("Tutorial Scene Properties", order = 2)]
    [Space(2f)]
    [Header("Level01 Scene Properties", order = 3)]

    //Private int; Counts what slide the user is currently in the canvas of intro game.
    int introSequence;
    //Keeps track of what scene the user is curently in. 
    Scene currentScene;

    /// <summary>
    /// Method for buttons used in the Title, Intro and Tutorial 
    /// scene that changes which scene the player should switch to.
    /// </summary>
    /// <param name="sceneName"> What you write down so the scene name the button 
    /// would switch to </param>
    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        //Switch statement that determines what the canvas will look like in different scenes.
        switch (currentScene.name)
        {
            case "00TitleScene":
                break;
            case "01IntroScene":
                IntializeIntro();
                break;
            case "02TutorialScene":
                break;
            case "03Level01Scene":
                break;
        }

    }

    public void dicButton(GameObject thisButton)
    {
        thisButton.SetActive(false);
    }

    public void dicButton2(GameObject otherButton)
    {
        otherButton.SetActive(true);
    }

    /// <summary>
    /// Method is only called to being contents for the first slide of the intro sequence. 
    /// </summary>
    void IntializeIntro()
    {
        introText.text = "Welcome to Alefwabaa! \n\n" +
                "Learn to write Arabic letters and words by helping Alibaba through his \n" +
                "magical fairy tale!";
        introImage.SetActive(true);
        backButton.SetActive(false);
    }

    /// <summary>
    /// Determines what slide the user is currently in during the intro sequence.
    /// </summary>
    void SetIntroSequence()
    {
        switch (introSequence)
        {
            case 0:
                introText.alignment = TextAlignmentOptions.Center;
                IntializeIntro();
                break;
            case 1:
                introImage.SetActive(false);
                backButton.SetActive(true);
                introText.alignment = TextAlignmentOptions.Left;
                introText.text = "OVERVIEW\n\n" +
                    "Title: Alefwabaa!\n" +
                    "Platform: WebGL Build (optional: Mobile App)\n" +
                    "Subject: World Languages\n" +
                    "Sub Subject: Arabic\n" +
                    "Main Focus: Arabic 101 - Elementary Writing.\n" +
                    "Learning Level: 8 - 12 year olds";
                break;
            case 2:
                introText.text = "PROPOSED EDUCATIONAL SOFTWARE SOLUTION TO A LEARNING PROBLEM \n\n" +
                    "Arab children who move to English Speaking countries depend " +
                    "on their parents to learn the mother tongue. Although, many only " +
                    "learn how to speak it fluently without knowing how to read and " +
                    "write since the language’s grammar is notoriously known to be one" +
                    " of the most difficult. There is also a lack of Arabic learning " +
                    "applications specifically targeted to the youth, so creating this " +
                    "software would help populate and increase the options for the " +
                    "targeted audience. This educational software (disguised as a game) " +
                    "will be an introduction for these children and a fun additional " +
                    "resource in their Arabic-lacking environment.";
                break;
            case 3:
                introText.text = "COMPETING SOFTWARE REVIEW \n\n" +
                   "· Alef: Learn Arabic For Kids - A mobile app available on the App Store and " +
                   "Google Play store for children to learn spoken Arabic with dialect. The easy interface " +
                   "it provides can be used by children from 1 to 8 years old. Children use the app by tapping " +
                   "on the drawing/Arabic alphabet/number on the screen and repeat the word they hear in the selected Arabic dialect. \n" +
                   "· Arabic Academy - A website for Kindergartners to Fifth graders to learn Arabic. " +
                   "With their animated illustrations, stories, and puzzles, children learn the alphabet " +
                   "and then learn to speak Arabic through situations that relate to their age. " +
                   "The program is self-paced and offers trial classes with personal tutors. For unlimited " +
                   "access to the program, it’s a minimum of $10/month.\n" +
                   "· DuoLingo - DuoLingo is a mobile and website language learning program that offers other " +
                   "languages. Their game-like lessons only take 5 minutes a day, and you receive rewards and unlock " +
                   "new levels as you continue. Other features include immediate grading and feedback after " +
                   "completing a lesson and personalized learning when it comes to reviewing vocabulary. Note: " +
                   "DuoLingo is targeted to adults, while DuoLingo Kids is targeted to kids, but does not offer Arabic.";

                break;
            case 4:
                introText.text = "COMPETING SOFTWARE SUGGESTED IMPROVEMENTS  \n\n" +
                    "· Personalization Features - Since not everyone learns the same way, providing a way " +
                    "to change the speed of pronunciation (only Duolingo does this), choosing the dialect " +
                    "(only Alef: Learn Arabic For Kids does this), and if they’d want the romanization subtitles along with Arabic ones.\n\n" +
                    "· Engagement Factors​ - The current competing software needs more interactive ways to engage with the" +
                    " user. There is a lack of a variety of minigames, background music, or other factors to keep the users" +
                    " interested. We would need more than rewards, pretty colors, and flashcards to hold children’s interest." +
                    " Things like adding emojis raining down when you complete a lesson or having a main story to follow " +
                    "along with can also make a gigantic difference in the long run. (For additional points, the story can be" +
                    " surrounded by Arabic culture or fairy tales). To take it even a step further, letting them create an " +
                    "account to save their progress makes their experience more personalized and encourages them to come back" +
                    " the next day to pick up where they left off (like DuoLingo).\n";
                break;
            case 5:
                introText.text = "COMPETING SOFTWARE CONTINUED...  \n\n" +
                    "· Writing Features - All of these apps lack the opportunity to write/type Arabic letters. If they are " +
                    "unfamiliar with writing the letters in the beginning, they would only be able to recognize them and not " +
                    "write them on their own. Users could write the letters with their stylus/fingertip on a smartphones' " +
                    "touch screen or with a mouse in a WebGL build. Leaving an option to type on the Arabic keyboard is also " +
                    "good, but should be used later on. It's not beneficial to start with typing on a keyboard because it " +
                    "connects the letters for you, and the users will not learn how to do it on their own. Difficulty for writing " +
                    "can begin from tracing letters to writing vocabulary on their own. ";
                break;
            case 6:
                introText.text = "STAKEHOLDERS & USERS\n" +
                    "Teachers\nParents\nStudents: ​6 - 8th Grade Middle School Students.\n" +
                    "PERSONA \n" +
                    "Name:​ Yasseen\nAge:​ 9 years old\nGender:​ Male\nLocation:​ USA\nPersonal Notes:\n" + 
                    "· Plays soccer\n· Enjoys being outside and being active\n· Primarily likes to play FPS shooter games or trendy mobile apps\n" +
                    "STUDENT NOTES:\n" +
                    "· Struggles to retain information in his Islamic Sunday School Program\n· Primarily finds difficulty with recitation / memorization Quran verses\n" +
                    "· Often forgets how to recognize words/ letter if not reviewed often\n· Prefers a visual examples and concepts\n· Doesn’t like to sit and listen if the lesson doesn’t demand direct interaction from him";
                break;
            case 7:
                introText.text = "JUSTIFICATION OF PERSONA DECISIONS:\n\n" +
                    "After debating and revising my ideas, I  settled on a persona named Yasseen. He represents " +
                    "elementary school students who need more engaging ways to learn Arabic in an English-speaking " +
                    "country. I chose this persona because elementary and middle school is when Arabic speaking " +
                    "students begin to lose motivation to learn Arabic due to their environment. The loss of motivation" +
                    " also stems from losing a sense of belonging to the Arab world and their home country after being " +
                    "exposed to a predominantly English-speaking one for a long time.";
                break;
            case 8:
                introText.text = "PROBLEM SCENARIO\n\n" +
                    "Yasseen is a 4th grader attending an American school. Yasseen spends his weekends " +
                    "practicing soccer on Saturdays and playing video games on Sundays. Later on, in the school year," +
                    " his parents signed him up for Islamic Sunday School. He'd spend time reciting the alphabet " +
                    "and Quran verses at school and then go home and play video games as he usually does on Sundays. " +
                    "Then when Saturday night rolls around, he would spend time trying to cram for the upcoming Arabic " +
                    "quiz. He'd struggle to grasp the basics of Arabic grammar as it's very different from English. " +
                    "Things like root-and-pattern morphology,  interspersing multiple morphemes, and semi-regular or " +
                    "irregular plural forms are foreign to him since he doesn't see this around his environment at all, or " +
                    "it's the complete opposite of what he is learning in English. Because of this, his Arabic lessons " +
                    "consisted more of recitation than active participation, which is not his preferred way of learning " +
                    "(he prefers visual and kinetic learning methods). After a year, his parents pulled him out of the " +
                    "program as they saw no significant improvement. Feeling isolated from the Arab world, Yasseen gives up learning how to write and read Arabic. ";
                break;
            case 9:
                nextButton.SetActive(true);
                introText.text = "ACTIVITY SCENARIO\n\n" +
                    "  One day his parents sit him at the computer and introduce him to an Arabic learning application they shared in a Facebook group. They tell him to try it out for a couple of minutes.  Reluctantly, he sits through the intro and tutorial and realizes this wasn't just a flashcard simulator. He was able to relate to levels and characters in the game as they talked in a similar dialect as his parents would talk to him. This application was different as it demanded constant input and rewarded him for doing so, just like the video games he plays at home. His parents go up and check on him to see how he's doing, and they're relieved to find him beaming with excitement." + 
                    "\n  After a week, you would often find Yasseen talking to his parents about the new words or phrases he learned to defeat the latest level.He would also engage with Arabic conversation more with his parents instead of replying in English.Due to his newfound interest in the game and ever so often learning some new words and phrases, it was easier to respond in Arabic.He even texts his oversea family on Facebook Messenger with a few Arabic words(like Hi, Bye, or Good). Yasseen can now write his name with ease and recite the alphabet. He becomes motivated again to learn Arabic and asks his parents to find him a private tutor.";
                break;
            case 10:
                nextButton.SetActive(false);
                introText.text = "PROBLEM STATEMENT\n\n " +
                    "Yasseen feels isolated and lost when it comes to learning Arabic. He likes to keep himself busy and prefers visual examples and concepts integrated into his lessons. He needs a way to apply the new rules he learns in his Arabic lectures, so the recited information wouldn't leave him right away. Through gamified sessions of learning to write simple letters and words, he unconsciously picks up that he needs to apply genders when forming verbs; he understands the letters will look different if connected at the start, middle, or the end of the words. And after being exposed to patterns of three-letter root words, he recognized the root-and-pattern grammar rule, where vowel-consonant patterns of words have similar grammatical functions. Additionally, he learns to appreciate his non-American identity more and embraces it as he unlocks the Arab world.";
                break;
        }
    }

    /// <summary>
    /// Goes to back to the previous slide when the "back" button is pressed.
    /// </summary>
    public void IntroSequenceBack()
    {
        introSequence--;
        SetIntroSequence();
    }

    /// <summary>
    /// Goes to the next slide when the "next" button is pressed.
    /// </summary>
    public void IntroSequenceNext()
    {
        introSequence++;
        SetIntroSequence();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

//This script is responsible for changing to a specfic scene.
public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    //When the Player collides with the box collider trigger, it will change scene
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            SceneManager.LoadScene(sceneName);
    }
}

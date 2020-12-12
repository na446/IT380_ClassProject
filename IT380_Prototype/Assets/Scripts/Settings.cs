using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI function that determines when settings panel will show.
public class Settings : MonoBehaviour
{
    public GameObject settinPanel;

    public void showSettings()
    {
        if (settinPanel.activeInHierarchy)
            settinPanel.SetActive(false);
        else
            settinPanel.SetActive(true);
    }
}

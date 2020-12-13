using UnityEngine;
using UnityEngine.UI;
using TMPro;

//UI function that determines when settings panel will show & stores values selected by user.
public class Settings : MonoBehaviour
{
    public GameObject settinPanel;
    public TMP_Dropdown dd;
    [HideInInspector]public float speedP = 1f;

    public void showSettings()
    {
        if (settinPanel.activeInHierarchy)
            settinPanel.SetActive(false);
        else
            settinPanel.SetActive(true);
    }

    private void Start()
    {
        //asigned on value changed event
        dd.onValueChanged.AddListener(delegate
        {
            setSpeed(dd);
        });

        DontDestroyOnLoad(gameObject);
        //DontDestroyChildOnLoad(gameObject);
    }

    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        // If this object doesn't have a parent then its the root transform.
        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }
        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
    }

    public void setSpeed(TMP_Dropdown sender)
    {
        switch (sender.value)
        {
            case 0:
                speedP = 1f;//speed is normal
                break;
            case 1:
                speedP = .5f;//speed is x.5 -slower
                break;
            case 2:
                speedP = 1.5f;//speed is x1.5 -faster
                break;
            default:
                speedP = 1f;
                break;
        }
    }
}

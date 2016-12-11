using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }

    public void HideMe()
    {
        gameObject.SetActive(false);
    }
}

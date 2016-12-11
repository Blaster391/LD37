using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{

    public bool ShowHelp;

	// Use this for initialization
	void Start ()
	{
        if(ShowHelp)
	        StartCoroutine("Reveal");

	   
	}

    IEnumerator Reveal()
    {
        yield return new WaitForSeconds(10);
        gameObject.GetComponent<Text>().text = "Press F1 For Help";
    }
	

}

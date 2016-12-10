using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour
{

    public float CreditsSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 gameObject.transform.Translate(Vector3.up * CreditsSpeed * Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class CameraScript : MonoBehaviour
{
    public int CameraSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(KeyCode.W))
	    {
	        MoveCamera(gameObject.transform.position + gameObject.transform.up * CameraSpeed * Time.deltaTime);
	    }
        if (Input.GetKey(KeyCode.S))
        {
            MoveCamera(gameObject.transform.position + -gameObject.transform.up* CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveCamera(gameObject.transform.position + gameObject.transform.right * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveCamera(gameObject.transform.position + -gameObject.transform.right * CameraSpeed * Time.deltaTime);
        }

    }

    private void MoveCamera(Vector3 newPos)
    {
        if (CheckCanMoveTheCamera(newPos))
        {
            gameObject.transform.position = newPos;
        }
    }

    private bool CheckCanMoveTheCamera(Vector3 newPos)
    {
        Ray ray = new Ray(newPos, gameObject.transform.forward);
        return Physics.SphereCast(ray, 7);
    }
}

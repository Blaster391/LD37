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
	        MoveCamera(gameObject.transform.position + Vector3.forward * CameraSpeed * Time.deltaTime);
	    }
        if (Input.GetKey(KeyCode.S))
        {
            MoveCamera(gameObject.transform.position + Vector3.back * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveCamera(gameObject.transform.position + Vector3.right * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveCamera(gameObject.transform.position + Vector3.left * CameraSpeed * Time.deltaTime);
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
        Ray ray = new Ray(newPos + Vector3.up*10, Vector3.down);
        return Physics.SphereCast(ray, 7);
    }
}

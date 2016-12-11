using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class CameraScript : MonoBehaviour
{
    public int CameraSpeed;
    public bool isCinematic;
    public Vector3 ViewNormal;

    public Vector3 ViewCinematic;
    public Vector3 RotationCinematic;
	
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
        if (Input.GetKey(KeyCode.Q))
        {
            MoveCamera(gameObject.transform.position + gameObject.transform.forward * CameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            MoveCamera(gameObject.transform.position + -gameObject.transform.forward * CameraSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeView();
        }
    }

    void Start()
    {
        if (!isCinematic)
        {
            gameObject.transform.position = ViewNormal;
            gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
        }
        else
        {
            gameObject.transform.position = ViewCinematic;
            gameObject.transform.eulerAngles = RotationCinematic;
        }
    }

    private void ChangeView()
    {
        if (isCinematic)
        {
            gameObject.transform.position = ViewNormal;
            gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
        }
        else
        {
            gameObject.transform.position =  ViewCinematic;
            gameObject.transform.eulerAngles = RotationCinematic;
        }

        isCinematic = !isCinematic;
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

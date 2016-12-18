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
        if (Input.GetKey(KeyCode.Q) && gameObject.transform.position.y > 5)
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

        var recognizer = new TKPanRecognizer();

        // when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            recognizer.minimumNumberOfTouches = 2;

        recognizer.gestureRecognizedEvent += (r) =>
        {
            Camera.main.transform.position -= new Vector3(recognizer.deltaTranslation.x, recognizer.deltaTranslation.y) / 100;
            Debug.Log("pan recognizer fired: " + r);
        };

        // continuous gestures have a complete event so that we know when they are done recognizing
        recognizer.gestureCompleteEvent += r =>
        {
            Debug.Log("pan gesture complete");
        };
        TouchKit.addGestureRecognizer(recognizer);
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

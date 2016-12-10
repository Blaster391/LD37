using UnityEngine;
using System.Collections;

public class RoomSpaceScript : MonoBehaviour
{

    private Room _room;

	// Use this for initialization
	void Start ()
	{
	    _room = gameObject.GetComponentInParent<Room>();

	}

    public Room GetConnectedRoom()
    {
        return _room;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Person"))
        {
            Debug.Log(_room.gameObject.name + "entered");
            _room.PersonEntersRoom(other.GetComponent<Person>());

            if (_room.Scary)
            {
                _room.Flee();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Person"))
        {
            Debug.Log(_room.gameObject.name + "left");
            _room.PersonLeavesRoom(other.GetComponent<Person>());
        }
    }

    public void TriggerScary()
    {
        _room.MakeRoomScary();
    }
}

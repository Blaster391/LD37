using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour
{

    private NavMeshAgent _agent;
    private GameManager _gm;

    public PersonColor PersonColor;
    public bool RandomWander;
    public Room CurrentRoom;

    // Use this for initialization
    void Start ()
	{
       
        _gm = GameObject.Find("Master").GetComponent<GameManager>();
        _gm.AddPerson(this);
        _agent = gameObject.GetComponent<NavMeshAgent>();

	    if (RandomWander)
	    {
            StartCoroutine("RandomWanderCoroutine");
	    }
    }

    public void MoveToRoom(Room room)
    {
        _agent.SetDestination(room.GetSpaceInRoom());
    }

    IEnumerator RandomWanderCoroutine()
    {
        yield return new WaitForSeconds(Random.value * 2);
        while (true)
        {
            Debug.Log("Setting destination");
            _agent.SetDestination(
                CurrentRoom.AdjecentRooms[RandomInteger.Get(0, CurrentRoom.AdjecentRooms.Count)].GetSpaceInRoom());
            yield return new WaitForSeconds(RandomInteger.Get(2,8));
        }
    }
}

public enum PersonColor
{
    Green,
    Blue
}

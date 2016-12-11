using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Person : MonoBehaviour
{

    private NavMeshAgent _agent;
    private GameManager _gm;

    public PersonColor PersonColor;
    public bool RandomWander;
    public Room CurrentRoom;

    public List<AudioClip> EnterAudio;

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
            var rooms = CurrentRoom.AdjecentRooms.Where(x => !x.Scary).ToList();
            if (rooms.Count != 0)
            {
                _agent.SetDestination(
                    rooms[RandomInteger.Get(0, rooms.Count)].GetSpaceInRoom());
               
            }
            yield return new WaitForSeconds(RandomInteger.Get(2, 8));
        }
    }

    public void PlayRoomAudio()
    {
        
        gameObject.GetComponent<AudioSource>().PlayOneShot(EnterAudio[0]);
    }
}

public enum PersonColor
{
    Green,
    Blue
}

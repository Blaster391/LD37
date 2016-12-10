using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Room : MonoBehaviour
{
    public GameObject RoomSpace;
    public List<Person> People;
    public List<Room> AdjecentRooms;
    private GameManager _gm;

    public bool Scary;

	// Use this for initialization
	void Start ()
	{
        _gm = GameObject.Find("Master").GetComponent<GameManager>();
        _gm.AddRoom(this);
        RoomSpace = gameObject.transform.GetChild(0).gameObject;

        var material = gameObject.GetComponent<Renderer>();
        material.material.color = Color.grey;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 GetSpaceInRoom()
    {
        return RoomSpace.transform.position;
    }

    public void PersonEntersRoom(Person p)
    {
        if (People.Contains(p))
            return;

        p.CurrentRoom = this;
        People.Add(p);
        CheckComplete();
        SetRoomColor();
    }
    public void PersonLeavesRoom(Person p)
    {
        if (!People.Contains(p))
            return;

        People.Remove(p);
        CheckComplete();
        SetRoomColor();
    }

    public void Flee()
    {
        var filteredRooms = AdjecentRooms.Where(x => !x.Scary).ToList();
        if (filteredRooms.Count == 0)
            return;
        foreach (var person in People)
        {
            var fleeIndex = RandomInteger.Get(0, filteredRooms.Count);
            Debug.Log(fleeIndex);
            person.MoveToRoom(filteredRooms[fleeIndex]);
        }
    }

    public void MakeRoomScary()
    {
        Scary = true;
        SetRoomColor();
        Flee();
    }

    public void CheckComplete()
    {
        _gm.CheckComplete();
    }

    public void SetRoomColor()
    {
        float green = (float)GetPeopleOfColourCount(PersonColor.Green)/_gm.NumberOfPeopleOfGivenColor(PersonColor.Green);
        float blue = (float)GetPeopleOfColourCount(PersonColor.Blue) / _gm.NumberOfPeopleOfGivenColor(PersonColor.Blue);



        var material = gameObject.GetComponent<Renderer>();

        if (Scary)
        {
            material.material.color = Color.red;
            return;
        }
        
        if (Math.Abs(green) < 0.01f)
        {
            material.material.color = Color.grey;
            return;
        }

        material.material.color = new Color(0, green, blue);
        
        
    }

    private int GetPeopleOfColourCount(PersonColor color)
    {
        return People.Count(x => x.PersonColor == color);
    }
}

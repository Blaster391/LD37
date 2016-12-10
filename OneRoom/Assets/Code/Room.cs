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

    public GameObject NorthWall;
    public GameObject SouthWall;
    public GameObject EastWall;
    public GameObject WestWall;

    public bool Scary;
    public bool ColorExclusive;
    public PersonColor ExclusiveColor;

	// Use this for initialization
	void Start ()
	{
        _gm = GameObject.Find("Master").GetComponent<GameManager>();
        _gm.AddRoom(this);
        RoomSpace = gameObject.transform.GetChild(0).gameObject;
        EastWall = gameObject.transform.GetChild(1).gameObject;
        WestWall = gameObject.transform.GetChild(2).gameObject;
        SouthWall = gameObject.transform.GetChild(3).gameObject;
        NorthWall = gameObject.transform.GetChild(4).gameObject;

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

        var nextRoomIndex = 0;
        foreach (var person in People)
        {
            if (nextRoomIndex >= filteredRooms.Count)
            {
                nextRoomIndex = 0;
            }

            person.MoveToRoom(filteredRooms[nextRoomIndex]);

            nextRoomIndex++;
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

        if (Scary)
        {
            SetColors(Color.red);
            return;
        }
        
        if (Math.Abs(green) < 0.01f)
        {
            SetColors(Color.grey);
            return;
        }

        SetColors(new Color(0, green, blue));
        
    }

    private void SetColors(Color color)
    {
        var material = gameObject.GetComponent<Renderer>();
        material.material.color = color;

        var wallNMaterial = NorthWall.gameObject.GetComponent<Renderer>();
        wallNMaterial.material.color = color;
        var wallSMaterial = SouthWall.gameObject.GetComponent<Renderer>();
        wallSMaterial.material.color = color;
        var wallEMaterial = EastWall.gameObject.GetComponent<Renderer>();
        wallEMaterial.material.color = color;
        var wallWMaterial = WestWall.gameObject.GetComponent<Renderer>();
        wallWMaterial.material.color = color;
    }

    private int GetPeopleOfColourCount(PersonColor color)
    {
        return People.Count(x => x.PersonColor == color);
    }
}

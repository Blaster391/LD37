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

    public GameObject NorthWalkThrough;
    public GameObject SouthWalkThrough;
    public GameObject EastWalkThrough;
    public GameObject WestWalkThrough;


    public bool Scary;
    public bool ColorExclusive;
    public PersonColor ExclusiveColor;

    public List<AudioClip> ClickedSounds;

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

        NorthWalkThrough = gameObject.transform.GetChild(9).gameObject;
        SouthWalkThrough = gameObject.transform.GetChild(10).gameObject;
        EastWalkThrough = gameObject.transform.GetChild(11).gameObject;
        WestWalkThrough = gameObject.transform.GetChild(12).gameObject;

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

        if (Time.timeSinceLevelLoad > 0.5f)
        {
            p.PlayRoomAudio();
        }
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

        var filteredRoomsBlue = filteredRooms.Where(x => (!x.ColorExclusive) || (x.ExclusiveColor == PersonColor.Blue)).ToList();
        var filteredRoomsGreen = filteredRooms.Where(x => (!x.ColorExclusive) || (x.ExclusiveColor == PersonColor.Green)).ToList();

        var nextRoomIndex = 0;
        foreach (var person in People.Where(x => x.PersonColor == PersonColor.Green))
        {
            if (filteredRoomsGreen.Count == 0)
                break;
            if (nextRoomIndex >= filteredRoomsGreen.Count)
            {
                nextRoomIndex = 0;
            }

            person.MoveToRoom(filteredRoomsGreen[nextRoomIndex]);

            nextRoomIndex++;
        }

        foreach (var person in People.Where(x => x.PersonColor == PersonColor.Blue))
        {
            if (filteredRoomsBlue.Count == 0)
                break;
            if (nextRoomIndex >= filteredRoomsBlue.Count)
            {
                nextRoomIndex = 0;
            }

            person.MoveToRoom(filteredRoomsBlue[nextRoomIndex]);

            nextRoomIndex++;
        }
    }

    public void MakeRoomScary()
    {
        Scary = true;
        SetRoomColor();
        Flee();

        var audioIndex = RandomInteger.Get(0, ClickedSounds.Count);
        gameObject.GetComponent<AudioSource>().PlayOneShot(ClickedSounds[audioIndex]);
    }

    public void CheckComplete()
    {
        _gm.CheckComplete();
    }

    public void SetRoomColor()
    {
        float green = (float)GetPeopleOfColourCount(PersonColor.Green)/_gm.NumberOfPeopleOfGivenColor(PersonColor.Green);
        float blue = (float)GetPeopleOfColourCount(PersonColor.Blue) / _gm.NumberOfPeopleOfGivenColor(PersonColor.Blue);
        float red = 0;
        if (Scary)
        {
            red = 1;
        }
        
        if (GetPeopleOfColourCount(PersonColor.Green) == 0 && GetPeopleOfColourCount(PersonColor.Blue) == 0 && !Scary)
        {
            SetColors(Color.grey);
            return;
        }

        SetColors(new Color(red, green, blue));
        
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<Person> People;
    public List<Room> Rooms;
    public GameObject NextLevelPanel;



    void Start()
    {
        NextLevelPanel = GameObject.Find("NextLevelPnl");
        if (NextLevelPanel != null)
        {
            NextLevelPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.CompareTag("RoomSpace"))
                {
                    hit.collider.GetComponent<RoomSpaceScript>().TriggerScary();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public int NumberOfPeopleOfGivenColor(PersonColor color)
    {
        return People.Count(x => x.PersonColor == color);
    }

    public void AddPerson(Person person)
    {
        if (People.Contains(person))
            return;

        People.Add(person);
    }

    public void AddRoom(Room room)
    {
        if (Rooms.Contains(room))
            return;

        Rooms.Add(room);
    }

    public void CheckComplete()
    {
        var greenRoom = new List<Room>();
        var blueRoom = new List<Room>();

        if (NumberOfPeopleOfGivenColor(PersonColor.Green) != 0)
        {
            greenRoom =
                Rooms.Where(x => (x.People.Count(y => y.PersonColor == PersonColor.Green) == NumberOfPeopleOfGivenColor(PersonColor.Green))).ToList();

            if (greenRoom.Count != 1)
                return;
        }

        if (NumberOfPeopleOfGivenColor(PersonColor.Blue) != 0)
        {
            blueRoom =
                Rooms.Where(x => (x.People.Count(y => y.PersonColor == PersonColor.Blue) == NumberOfPeopleOfGivenColor(PersonColor.Blue))).ToList();

            if (blueRoom.Count != 1)
                return;
        }

        if (blueRoom.FirstOrDefault() != greenRoom.FirstOrDefault())
        {
            if (NextLevelPanel != null)
                NextLevelPanel.SetActive(true);
        }
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

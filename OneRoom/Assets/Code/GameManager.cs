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
    public GameObject HelpPanel;
    public GameObject QuitPanel;
    public AudioSource AudioPlayer;

    public List<AudioClip> CompletionSounds;

    void Start()
    {
        NextLevelPanel = GameObject.Find("NextLevelPnl");
        if (NextLevelPanel != null)
        {
            NextLevelPanel.SetActive(false);
        }
        
        HelpPanel = GameObject.Find("HelpPanel");
        if (HelpPanel != null)
        {
            HelpPanel.SetActive(false);
        }

        QuitPanel = GameObject.Find("QuitPanel");
        if (QuitPanel != null)
        {
            QuitPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask lm = LayerMask.GetMask("Room");
            if (Physics.Raycast(ray, out hit, 100.0f, lm))
            {
                if (hit.collider.CompareTag("Room"))
                {
                    hit.collider.GetComponent<Room>().MakeRoomScary();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Replay();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            HelpPanel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.F1))
        {
            HelpPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (QuitPanel != null)
            {
                QuitPanel.SetActive(true);
            }
            else
            {
                Application.Quit();
            }
           
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

            var audioIndex = RandomInteger.Get(0, CompletionSounds.Count);
            AudioPlayer.PlayOneShot(CompletionSounds[audioIndex]);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

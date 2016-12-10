using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;
using Object = UnityEngine.Object;


public class RoomLinker : EditorWindow
{

    [MenuItem("Window/Room Linker")]
    static void ShowEditor()
    {
        RoomLinker editor = EditorWindow.GetWindow<RoomLinker>();

    }


    void OnGUI()
    {

		if (GUILayout.Button("Link"))
		{
            Link();
		    
		    AssetDatabase.SaveAssets();

		}

        if (GUILayout.Button("Clear"))
        {
            Clear();
        }

    }

    public void Link()
    {
        var roomsArray = Object.FindObjectsOfType(typeof(Room)) as Room[];
        if (roomsArray == null)
            return;

        foreach (var room in roomsArray)
        {
            room.RoomSpace = room.gameObject.transform.GetChild(0).gameObject;
            room.EastWall = room.gameObject.transform.GetChild(1).gameObject;
            room.WestWall = room.gameObject.transform.GetChild(2).gameObject;
            room.SouthWall = room.gameObject.transform.GetChild(3).gameObject;
            room.NorthWall = room.gameObject.transform.GetChild(4).gameObject;

            var northRoom = TryGetRoom(room.gameObject.transform.position + new Vector3(0, 0, 10));
            if (northRoom != null && !room.AdjecentRooms.Contains(northRoom))
            {

                room.NorthWall.SetActive(false);
                room.AdjecentRooms.Add(northRoom);
            }else if (northRoom == null)
            {
                room.NorthWall.SetActive(true);
            }

            var southRoom = TryGetRoom(room.gameObject.transform.position + new Vector3(0, 0, -10));
            if (southRoom != null && !room.AdjecentRooms.Contains(southRoom))
            {
                room.SouthWall.SetActive(false);
                room.AdjecentRooms.Add(southRoom);
            }
            else if (southRoom == null)
            {
                room.SouthWall.SetActive(true);
            }

            var eastRoom = TryGetRoom(room.gameObject.transform.position + new Vector3(10, 0, 0));
            if (eastRoom != null && !room.AdjecentRooms.Contains(eastRoom))
            {
                room.EastWall.SetActive(false);
                room.AdjecentRooms.Add(eastRoom);
            }
            else if (eastRoom == null)
            {
                room.EastWall.SetActive(true);
            }

            var westRoom = TryGetRoom(room.gameObject.transform.position + new Vector3(-10, 0, 0));
            if (westRoom != null && !room.AdjecentRooms.Contains(westRoom))
            {
                room.WestWall.SetActive(false);
                room.AdjecentRooms.Add(westRoom);
            }
            else if (westRoom == null)
            {
                room.WestWall.SetActive(true);
            }

            EditorUtility.SetDirty(room);
            EditorUtility.SetDirty(room.NorthWall);
            EditorUtility.SetDirty(room.SouthWall);
            EditorUtility.SetDirty(room.EastWall);
            EditorUtility.SetDirty(room.WestWall);

        }
    }

    private Room TryGetRoom(Vector3 target)
    {
        Ray ray = new Ray(target + Vector3.up * 10, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("There is something in front of the object!" + hit.collider.tag + " - " + hit.collider.name);
            if (hit.collider.CompareTag("RoomSpace"))
            {
                Debug.Log("Bleh" + hit.collider.gameObject.GetComponent<RoomSpaceScript>().gameObject.transform.parent);
                return hit.collider.gameObject.GetComponent<RoomSpaceScript>().gameObject.transform.parent.GetComponent<Room>();
            }
            
        }

        return null;
    }

    private void Clear()
    {
        var roomsArray = Object.FindObjectsOfType(typeof(Room)) as Room[];
        if (roomsArray == null)
            return;

        foreach (var room in roomsArray)
        {
            room.AdjecentRooms.Clear();
            room.EastWall.SetActive(true);
            room.SouthWall.SetActive(true);
            room.NorthWall.SetActive(true);
            room.WestWall.SetActive(true);
        }
    }

}
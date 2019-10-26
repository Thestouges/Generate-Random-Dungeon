using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    struct Room
    {
        bool up;
        bool down;
        bool left;
        bool right;
    }

    List<List<bool>> dungeon;

    public int GridSize = 1;
    public int TotalRooms = 1;
    public float RoomSize = 1;
    public GameObject RoomObject;
    public GameObject CorridorObject;
    public float CorridorLength = 1;
    public float CorridorWidth = 1;
    int roomCounter = 1;
    // Start is called before the first frame update
    void Start()
    {
        initializeDungeonLayout();


        //createFloor();
        /*
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 1.5f, 0);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 1, 0);

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    void initializeDungeonLayout()
    {
        dungeon = new List<List<bool>>();
        for (int i = 0; i < GridSize; i++)
        {
            List<bool> row = new List<bool>();
            for (int j = 0; j < GridSize; j++)
            {
                bool room = false;
                row.Add(room);
            }
            dungeon.Add(row);
        }

        //random starting room
        int startx = Random.Range(0, GridSize);
        int starty = Random.Range(0, GridSize);
        Debug.Log(startx + " " +starty);
        dungeon[startx][starty] = true;

        while (roomCounter < TotalRooms)
        {
            generateRoom(startx, starty);
        }

        bool first = true;
        for (int i = 0; i < dungeon.Count; i++)
        {
            for (int j = 0; j < dungeon.Count; j++)
            {
                if (dungeon[i][j] == true)
                {
                    float posx = i * RoomSize + CorridorLength * i;
                    float posy = j * RoomSize + CorridorLength * j;
                    createFloor(posx, posy);
                    try
                    {
                        if (dungeon[i + 1][j] == true)
                        {
                            generateCorridor((((i+1) * RoomSize + CorridorLength * (i+1)) + posx)/2, posy, 0);
                            if (first)
                            {
                                first = false;
                                //Debug.Log((((i + 1) * RoomSize + CorridorLength * (i + 1)) * posx) / 2);
                            }
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        if (dungeon[i][j+1] == true)
                        {
                            generateCorridor(posx, (((j + 1) * RoomSize + CorridorLength * (j + 1)) + posy) / 2, 90);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
    }

    void generateCorridor(float posx, float posy, float rotation)
    {
        GameObject corridor = Instantiate(CorridorObject);
        corridor.transform.position = new Vector3(posx, posy, 0);
        corridor.transform.localScale = new Vector3(CorridorLength, CorridorWidth, 1);
        corridor.transform.eulerAngles = new Vector3(0,0,rotation);
    }

    void generateRoom(int posx, int posy)
    {
        if (roomCounter >= TotalRooms)
            return;
        
        //north room
        if(Random.Range(0,4) != 0 && posx > 0)
        {
            setRoomTrue(posx, posy);
            generateRoom(posx - 1, posy);
        }
        if (roomCounter >= TotalRooms)
            return;

        //east room
        if (Random.Range(0, 4) != 0 && posy > 0)
        {
            setRoomTrue(posx, posy);
            generateRoom(posx, posy-1);
        }
        if (roomCounter >= TotalRooms)
            return;

        //south room
        if (Random.Range(0, 4) != 0 && posx < GridSize-2)
        {
            setRoomTrue(posx, posy);
            generateRoom(posx + 1, posy);
        }
        if (roomCounter >= TotalRooms)
            return;

        //west room
        if (Random.Range(0, 4) != 0 && posy < GridSize - 2)
        {
            setRoomTrue(posx, posy);
            generateRoom(posx , posy + 1);
        }
        if (roomCounter >= TotalRooms)
            return;
    }

    void setRoomTrue(int posx, int posy)
    {
        if (dungeon[posx][posy] == false)
        {
            dungeon[posx][posy] = true;
            roomCounter++;
        }
    }

    void createFloor(float posx, float posy)
    {
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(posx, posy, 0);
        //cube.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        GameObject room = Instantiate(RoomObject);
        room.transform.position = new Vector3(posx, posy, 0);
        room.transform.localScale = new Vector3(RoomSize, RoomSize, 1);
    }
}

using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LoadLevel : MonoBehaviour
{
    static string activePath = "C:\\Users\\Jeremy\\Desktop\\level00.lvl";
    public bool loaded = false;

    public void Start()
    {
        activePath = GameObject.Find("PathHolder").GetComponent<PathHolderScript>().path;
        Load();
    }

    public void Load()
    {
        UnityEngine.Object[] objects = GameObject.FindObjectsOfType(gameObject.GetType());

        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name != "Main Camera" && objects[i].name != "PathHolder")
                GameObject.Destroy(objects[i]);
        }

        string[] lvl = File.ReadAllLines(activePath);
        string[] firstLine = lvl[0].Split();
        int width = Convert.ToInt32(firstLine[0]);
        int height = Convert.ToInt32(firstLine[1]);
        GameObject[,] objectsInLevel = new GameObject[height, width];
        int numConnections = Convert.ToInt32(firstLine[2]);
        for(int i = 1; i < height+1; i++)
        {
            int heightOffset = height - i;
            string[] line = lvl[i].Split();
            for(int j = 0; j < width; j++)
            {
                switch(line[j])
                {
                    case "00":
                        break;
                    case "01":
                        GameObject tile1x1 = Instantiate(Resources.Load("tile1x1"), new Vector3(j, heightOffset, -2), Quaternion.identity) as GameObject;
                        objectsInLevel[i-1, j] = tile1x1;
                        break;
                    case "02":
                        GameObject player = Instantiate(Resources.Load("player"), new Vector3((float)j + 0.5f, (float)heightOffset + 1.5f, 0), Quaternion.identity) as GameObject;
                        objectsInLevel[i-1, j] = player;
                        break;
                    case "03":
                        GameObject wallCrab = Instantiate(Resources.Load("wallCrab"), new Vector3(j, heightOffset, 0), Quaternion.identity) as GameObject;
                        objectsInLevel[i-1, j] = wallCrab;
                        break;
                    case "04":
                        GameObject hardPressurePlate = Instantiate(Resources.Load("hardPressurePlate"), new Vector3(j, heightOffset - 0.5f, -1), Quaternion.identity) as GameObject;
                        objectsInLevel[i-1, j] = hardPressurePlate;
                        break;
                    case "05":
                        GameObject softPressurePlate = Instantiate(Resources.Load("softPressurePlate"), new Vector3(j, heightOffset - 0.5f, -1), Quaternion.identity) as GameObject;
                        objectsInLevel[i - 1, j] = softPressurePlate;
                        break;
                    case "06":
                        GameObject door = Instantiate(Resources.Load("door"), new Vector3(j, heightOffset + 1.5f, 0), Quaternion.identity) as GameObject;
                        objectsInLevel[i-1, j] = door;
                        break;
                    case "07":
                        GameObject button = Instantiate(Resources.Load("button"), new Vector3(j + 0.5f, heightOffset, -1), Quaternion.identity) as GameObject;
                        objectsInLevel[i - 1, j] = button;
                        break;
                    case "08":
                        GameObject finish = Instantiate(Resources.Load("finish"), new Vector3(j, heightOffset, -1), Quaternion.identity) as GameObject;
                        objectsInLevel[i - 1, j] = finish;
                        break;

                }
            }
        }
        for(int i = height+1; i < height+1+numConnections; i++)
        {
            string[] line = lvl[i].Split();
            int num = Convert.ToInt32(line[0]);
            GameObject obj = objectsInLevel[Convert.ToInt32(line[1]), Convert.ToInt32(line[2])];
            for (int j = 0; j < num; j++)
            {
                int h = Convert.ToInt32(line[3 + 2 * j]);
                int w = Convert.ToInt32(line[4 + 2 * j]);
                obj.GetComponent<ObjectActivator>().objects.Add(objectsInLevel[h, w]);
                GameObject wire = Instantiate(Resources.Load("wire"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                LineRenderer lr = wire.GetComponent<LineRenderer>();
                lr.SetPosition(0, new Vector3(obj.transform.position.x, obj.transform.position.y, 3));
                lr.SetPosition(1, new Vector3(objectsInLevel[h, w].transform.position.x, objectsInLevel[h, w].transform.position.y, 3));
            }
        }
        loaded = true;
    }
}

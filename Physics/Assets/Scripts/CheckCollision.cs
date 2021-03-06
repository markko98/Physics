﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CheckCollision : MonoBehaviour
{
     public GameObject[] collidedObjects;
     public GameObject[] waters;
     public bool wasInWater = false;
     public float Vdisplaced;
    private void Awake()
    {
        collidedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        waters = GameObject.FindGameObjectsWithTag("Water");
    }

    public bool CheckForCollisionYDown(MeshGenerator objectToCheckCollisionsWith)
    {
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if ((objectToCheckCollisionsWith.transform.position.y - objectToCheckCollisionsWith.height - 0.05f <= collidedObjects[i].transform.position.y + collidedObjects[i].GetComponent<MeshGenerator>().height)
                && (objectToCheckCollisionsWith.transform.position.y > collidedObjects[i].transform.position.y)
                && (objectToCheckCollisionsWith.transform.position.x >= collidedObjects[i].transform.position.x - collidedObjects[i].GetComponent<MeshGenerator>().width)
                && (objectToCheckCollisionsWith.transform.position.x <= collidedObjects[i].transform.position.x + collidedObjects[i].GetComponent<MeshGenerator>().width))
            {
                objectToCheckCollisionsWith.transform.position =new Vector2(transform.position.x, collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<MeshGenerator>().height + objectToCheckCollisionsWith.height + 0.02f);

                wasInWater = false;
                return true;
            }

        }
        return false;
    }
    public bool CheckForCollisionYUp(MeshGenerator objectToCheckCollisionsWith)
    {
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if ((objectToCheckCollisionsWith.transform.position.y + objectToCheckCollisionsWith.height + 0.05f >= collidedObjects[i].transform.position.y - collidedObjects[i].GetComponent<MeshGenerator>().height)
                && (objectToCheckCollisionsWith.transform.position.y < collidedObjects[i].transform.position.y)
                && (objectToCheckCollisionsWith.transform.position.x >= collidedObjects[i].transform.position.x - collidedObjects[i].GetComponent<MeshGenerator>().width)
                && (objectToCheckCollisionsWith.transform.position.x <= collidedObjects[i].transform.position.x + collidedObjects[i].GetComponent<MeshGenerator>().width))
            {
                objectToCheckCollisionsWith.transform.position = new Vector2(transform.position.x, collidedObjects[i].transform.position.y - collidedObjects[i].transform.GetComponent<MeshGenerator>().height - objectToCheckCollisionsWith.height - 0.02f);
                return true;
            }
        }
        return false;
    }

    public bool CheckForCollisionXRight(MeshGenerator objectToCheckCollisionsWith)
    {
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if ((objectToCheckCollisionsWith.transform.position.x + objectToCheckCollisionsWith.width + 0.05f >= collidedObjects[i].transform.position.x - collidedObjects[i].GetComponent<MeshGenerator>().width)
                && (objectToCheckCollisionsWith.transform.position.x < collidedObjects[i].transform.position.x)
                && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.position.y - collidedObjects[i].GetComponent<MeshGenerator>().height)
                && (objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.position.y + collidedObjects[i].GetComponent<MeshGenerator>().height))
            {
                objectToCheckCollisionsWith.transform.position = new Vector2(
                    collidedObjects[i].transform.position.x - collidedObjects[i].transform.GetComponent<MeshGenerator>().width - objectToCheckCollisionsWith.width - 0.02f,
                    transform.position.y);
                return true;
            }
        }
        return false;
    }
    public bool CheckForCollisionXLeft(MeshGenerator objectToCheckCollisionsWith)
    {
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if ((objectToCheckCollisionsWith.transform.position.x - objectToCheckCollisionsWith.width - 0.05f <= collidedObjects[i].transform.position.x + collidedObjects[i].GetComponent<MeshGenerator>().width)
                && (objectToCheckCollisionsWith.transform.position.x > collidedObjects[i].transform.position.x)
                && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.position.y - collidedObjects[i].GetComponent<MeshGenerator>().height)
                && (objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.position.y + collidedObjects[i].GetComponent<MeshGenerator>().height))
            {
                objectToCheckCollisionsWith.transform.position = new Vector2(collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<MeshGenerator>().width + objectToCheckCollisionsWith.width + 0.02f, transform.position.y);
                return true;
            }
        }
        return false;
    }

    public bool CheckCollisionWithWaterDown(MeshGenerator object1)
    {
        for (int i = 0; i < waters.Length; i++)
        {
            if ((object1.transform.position.y - object1.GetComponent<MeshGenerator>().height <= waters[i].transform.position.y + waters[i].GetComponent<MeshGenerator>().height)
                && (object1.transform.position.x >= waters[i].transform.position.x - waters[i].GetComponent<MeshGenerator>().width)
                && (object1.transform.position.x <= waters[i].transform.position.x + waters[i].GetComponent<MeshGenerator>().width))
            {
                if (object1.transform.name == "Player")
                {
                    Vdisplaced = Mathf.Abs((object1.transform.position.y - object1.GetComponent<MeshGenerator>().height) - (waters[i].transform.position.y + waters[i].GetComponent<MeshGenerator>().height));
                    if (Vdisplaced > object1.GetComponent<PlayerController>().volumeOfObject)
                    {
                        Vdisplaced = object1.GetComponent<PlayerController>().volumeOfObject;
                    }
                }
                
                wasInWater = true;
                return true;
            }

        }
        return false;
    }
    public bool CheckCollisionWithWaterUp(MeshGenerator object1)
    {
        for (int i = 0; i < waters.Length; i++)
        {
            if ((object1.transform.position.y >= waters[i].transform.position.y + waters[i].GetComponent<MeshGenerator>().height)
                && (object1.transform.position.x >= waters[i].transform.position.x - waters[i].GetComponent<MeshGenerator>().width)
                && (object1.transform.position.x <= waters[i].transform.position.x + waters[i].GetComponent<MeshGenerator>().width) 
                && wasInWater)
            {
                return true;
            }

        }
        return false;
    }
}

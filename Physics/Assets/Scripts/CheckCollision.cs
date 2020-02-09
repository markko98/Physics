using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CheckCollision : MonoBehaviour
{
     public GameObject[] collidedObjects;
     public GameObject[] waters;
    [HideInInspector] public bool wasInWater = false;
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
                return true;
            }
        }
        return false;
    }

    public bool CheckCollisionWithWaterDown(MeshGenerator object1)
    {
        for (int i = 0; i < waters.Length; i++)
        {
            if ((object1.transform.position.y <= waters[i].transform.position.y + waters[i].GetComponent<MeshGenerator>().height)
                && (object1.transform.position.x >= waters[i].transform.position.x - waters[i].GetComponent<MeshGenerator>().width)
                && (object1.transform.position.x <= waters[i].transform.position.x + waters[i].GetComponent<MeshGenerator>().width))
            {
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

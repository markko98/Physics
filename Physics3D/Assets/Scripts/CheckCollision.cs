using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CheckCollision : MonoBehaviour
{
    public GameObject[] collidedObjects;
    public GameObject[] waters;
    private Collider collider;
    public bool wasInWater = false;
    private void Awake()
    {
        collidedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        waters = GameObject.FindGameObjectsWithTag("Water");
    }

    public bool CheckForCollisionYDown(GameObject objectToCheckCollisionsWith)
    {
        collider = objectToCheckCollisionsWith.GetComponent<Collider>();
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (objectToCheckCollisionsWith.transform.position.y - collider.bounds.extents.y <= collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
            {
                if ((objectToCheckCollisionsWith.transform.position.y + collider.bounds.extents.y >= collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.x - collider.bounds.extents.x <= collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (objectToCheckCollisionsWith.transform.position.x + collider.bounds.extents.x >= collidedObjects[i].transform.position.x - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (objectToCheckCollisionsWith.transform.position.z - collider.bounds.extents.z <= collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (objectToCheckCollisionsWith.transform.position.z + collider.bounds.extents.z >= collidedObjects[i].transform.position.z - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)) 
                {
                    objectToCheckCollisionsWith.transform.position = new Vector3(transform.position.x, collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y + collider.bounds.extents.y+0.05f, transform.position.z);
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckForCollisionYUp(GameObject objectToCheckCollisionsWith)
    {
        collider = objectToCheckCollisionsWith.GetComponent<Collider>();
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (collider.bounds.max.y >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.y)
            {
                if ((objectToCheckCollisionsWith.transform.position.x <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.x)
                    && (objectToCheckCollisionsWith.transform.position.x >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.x)
                    && (objectToCheckCollisionsWith.transform.position.z <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.z)
                    && (objectToCheckCollisionsWith.transform.position.z >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.z))
                {
                    objectToCheckCollisionsWith.transform.position = new Vector3(transform.position.x, collidedObjects[i].transform.position.y - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y - collider.bounds.extents.y - 0.05f, transform.position.z);
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckForCollisionXRight(GameObject objectToCheckCollisionsWith)
    {
        collider = objectToCheckCollisionsWith.GetComponent<Collider>();
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (objectToCheckCollisionsWith.transform.position.x + collider.bounds.extents.x >= collidedObjects[i].transform.position.x - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
            {
                if ((objectToCheckCollisionsWith.transform.position.x - collider.bounds.extents.x <= collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (objectToCheckCollisionsWith.transform.position.y - collider.bounds.extents.y <= collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.y + collider.bounds.extents.y >= collidedObjects[i].transform.position.y - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.z - collider.bounds.extents.z <= collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (objectToCheckCollisionsWith.transform.position.z + collider.bounds.extents.z >= collidedObjects[i].transform.position.z - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z))
                {
                    objectToCheckCollisionsWith.transform.position = new Vector3(collidedObjects[i].transform.position.x - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x - collider.bounds.extents.x - 0.05f, transform.position.y, transform.position.z);
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckForCollisionXLeft(GameObject objectToCheckCollisionsWith)
    {
        collider = objectToCheckCollisionsWith.GetComponent<Collider>();
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (objectToCheckCollisionsWith.transform.position.x - collider.bounds.extents.x <= collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
            {
                if ((objectToCheckCollisionsWith.transform.position.x + collider.bounds.extents.x >= collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (objectToCheckCollisionsWith.transform.position.y - collider.bounds.extents.y <= collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.y + collider.bounds.extents.y >= collidedObjects[i].transform.position.y - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.z - collider.bounds.extents.z <= collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (objectToCheckCollisionsWith.transform.position.z + collider.bounds.extents.z >= collidedObjects[i].transform.position.z - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z))
                {
                    objectToCheckCollisionsWith.transform.position = new Vector3(collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x + collider.bounds.extents.x + 0.05f, transform.position.y, transform.position.z);
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckForCollisionZTop(GameObject objectToCheckCollisionsWith)
    {
        collider = objectToCheckCollisionsWith.GetComponent<Collider>();
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (objectToCheckCollisionsWith.transform.position.z + collider.bounds.extents.z >= collidedObjects[i].transform.position.z - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
            {
                if ((objectToCheckCollisionsWith.transform.position.z - collider.bounds.extents.z <= collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (objectToCheckCollisionsWith.transform.position.y - collider.bounds.extents.y <= collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.y + collider.bounds.extents.y >= collidedObjects[i].transform.position.y - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.x - collider.bounds.extents.x <= collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (objectToCheckCollisionsWith.transform.position.x + collider.bounds.extents.x >= collidedObjects[i].transform.position.x - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x))
                {
                    objectToCheckCollisionsWith.transform.position = new Vector3(transform.position.x, transform.position.y, collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z + collider.bounds.extents.z + 0.05f);
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckForCollisionZDown(GameObject objectToCheckCollisionsWith)
    {
        collider = objectToCheckCollisionsWith.GetComponent<Collider>();
        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (objectToCheckCollisionsWith.transform.position.z - collider.bounds.extents.z <= collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
            {
                if ((objectToCheckCollisionsWith.transform.position.z + collider.bounds.extents.z >= collidedObjects[i].transform.position.z + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (objectToCheckCollisionsWith.transform.position.y - collider.bounds.extents.y <= collidedObjects[i].transform.position.y + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.y + collider.bounds.extents.y >= collidedObjects[i].transform.position.y - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (objectToCheckCollisionsWith.transform.position.x - collider.bounds.extents.x <= collidedObjects[i].transform.position.x + collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (objectToCheckCollisionsWith.transform.position.x + collider.bounds.extents.x >= collidedObjects[i].transform.position.x - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.x))
                {
                    objectToCheckCollisionsWith.transform.position = new Vector3(transform.position.x, transform.position.y, collidedObjects[i].transform.position.z - collidedObjects[i].transform.GetComponent<Collider>().bounds.extents.z - collider.bounds.extents.z - 0.05f);
                    return true;
                }
            }
        }
        return false;
    }
}

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
            if (objectToCheckCollisionsWith.transform.position.y - collider.bounds.extents.y <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.y)
            {
                if ((objectToCheckCollisionsWith.transform.position.y + collider.bounds.extents.y >= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.y)
                    && (objectToCheckCollisionsWith.transform.position.x - collider.bounds.extents.x <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.x)
                    && (objectToCheckCollisionsWith.transform.position.x + collider.bounds.extents.x >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.x))
                    //&& (objectToCheckCollisionsWith.transform.position.z - collider.bounds.extents.z <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.z)
                    //&& (objectToCheckCollisionsWith.transform.position.z + collider.bounds.extents.z >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.z))
                {
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
            if (collider.bounds.max.x >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.x)
            {
                if ((objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.y)
                    && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.y)
                    && (objectToCheckCollisionsWith.transform.position.z <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.z)
                    && (objectToCheckCollisionsWith.transform.position.z >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.z))
                {
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
            if (collider.bounds.min.x <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.x)
            {
                if ((objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.y)
                    && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.y)
                    && (objectToCheckCollisionsWith.transform.position.z <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.z)
                    && (objectToCheckCollisionsWith.transform.position.z >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.z))
                {
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
            if (collider.bounds.max.z >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.z)
            {
                if ((objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.y)
                    && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.y)
                    && (objectToCheckCollisionsWith.transform.position.x <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.x)
                    && (objectToCheckCollisionsWith.transform.position.x >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.x))
                {
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
            if (collider.bounds.min.z <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.z)
            {
                if ((objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.y)
                    && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.y)
                    && (objectToCheckCollisionsWith.transform.position.x <= collidedObjects[i].transform.GetComponent<Collider>().bounds.max.x)
                    && (objectToCheckCollisionsWith.transform.position.x >= collidedObjects[i].transform.GetComponent<Collider>().bounds.min.x))
                {
                    return true;
                }
            }
        }
        return false;
    }






    //public bool CheckForCollisionYUp(GameObject objectToCheckCollisionsWith)
    //{
    //    for (int i = 0; i < collidedObjects.Length; i++)
    //    {
    //        if ((objectToCheckCollisionsWith.transform.position.y + objectToCheckCollisionsWith.height + 0.05f >= collidedObjects[i].transform.position.y - collidedObjects[i].GetComponent<MeshGenerator>().height)
    //            && (objectToCheckCollisionsWith.transform.position.y < collidedObjects[i].transform.position.y)
    //            && (objectToCheckCollisionsWith.transform.position.x >= collidedObjects[i].transform.position.x - collidedObjects[i].GetComponent<MeshGenerator>().width)
    //            && (objectToCheckCollisionsWith.transform.position.x <= collidedObjects[i].transform.position.x + collidedObjects[i].GetComponent<MeshGenerator>().width))
    //        {

    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //public bool CheckForCollisionXRight(GameObject objectToCheckCollisionsWith)
    //{
    //    for (int i = 0; i < collidedObjects.Length; i++)
    //    {
    //        if ((objectToCheckCollisionsWith.transform.position.x + objectToCheckCollisionsWith.width + 0.05f >= collidedObjects[i].transform.position.x - collidedObjects[i].GetComponent<MeshGenerator>().width)
    //            && (objectToCheckCollisionsWith.transform.position.x < collidedObjects[i].transform.position.x)
    //            && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.position.y - collidedObjects[i].GetComponent<MeshGenerator>().height)
    //            && (objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.position.y + collidedObjects[i].GetComponent<MeshGenerator>().height))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    //public bool CheckForCollisionXLeft(GameObject objectToCheckCollisionsWith)
    //{
    //    for (int i = 0; i < collidedObjects.Length; i++)
    //    {
    //        if ((objectToCheckCollisionsWith.transform.position.x - objectToCheckCollisionsWith.width - 0.05f <= collidedObjects[i].transform.position.x + collidedObjects[i].GetComponent<MeshGenerator>().width)
    //            && (objectToCheckCollisionsWith.transform.position.x > collidedObjects[i].transform.position.x)
    //            && (objectToCheckCollisionsWith.transform.position.y >= collidedObjects[i].transform.position.y - collidedObjects[i].GetComponent<MeshGenerator>().height)
    //            && (objectToCheckCollisionsWith.transform.position.y <= collidedObjects[i].transform.position.y + collidedObjects[i].GetComponent<MeshGenerator>().height))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //public bool CheckCollisionWithWaterDown(GameObject object1)
    //{
    //    for (int i = 0; i < waters.Length; i++)
    //    {
    //        if ((object1.transform.position.y <= waters[i].transform.position.y + waters[i].GetComponent<MeshGenerator>().height)
    //            && (object1.transform.position.x >= waters[i].transform.position.x - waters[i].GetComponent<MeshGenerator>().width)
    //            && (object1.transform.position.x <= waters[i].transform.position.x + waters[i].GetComponent<MeshGenerator>().width))
    //        {
    //            wasInWater = true;
    //            return true;
    //        }

    //    }
    //    return false;
    //}
    //public bool CheckCollisionWithWaterUp(GameObject object1)
    //{
    //    for (int i = 0; i < waters.Length; i++)
    //    {
    //        if ((object1.transform.position.y >= waters[i].transform.position.y + waters[i].GetComponent<MeshGenerator>().height)
    //            && (object1.transform.position.x >= waters[i].transform.position.x - waters[i].GetComponent<MeshGenerator>().width)
    //            && (object1.transform.position.x <= waters[i].transform.position.x + waters[i].GetComponent<MeshGenerator>().width) 
    //            && wasInWater)
    //        {
    //            return true;
    //        }

    //    }
    //    return false;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CheckCollision
{
    private const float gravity = 9.81f;
    private float mass;
    PlayerController playerController;
    Position position, position0;
    Velocity bulletVelocity0, bulletVelocity;
    float bulletShootAngleX, bulletShootAngleY;
    bool isGrounded = false;
    [Range(0, 180)]
    public float projectileShootAngleX;
    [Range(0, 180)]
    public float projectileShootAngleY;
    private void Awake()
    {
        mass = 1;
        collidedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        waters = GameObject.FindGameObjectsWithTag("Water");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        position0 = new Position(transform.position.x, transform.position.y, transform.position.z);
        position = position0;
        bulletVelocity0 = new Velocity(0, 0);
        bulletVelocity = bulletVelocity0;

        // DEFINING THE FORCE FOR SHOOTING THE BULLET + CURRENT PLAYER VELOCITY
        bulletVelocity0.Vx = 15f;
        bulletVelocity0.Vy = 15f;
        bulletVelocity0.Vz = 15f;

        // ANGLE ON WHICH THE BULLET IS SHOOTED
        bulletShootAngleX = projectileShootAngleX * Mathf.PI / 180;
        bulletShootAngleY = projectileShootAngleY * Mathf.PI / 180;

        bulletVelocity.Vx = bulletVelocity0.Vx * Mathf.Cos(bulletShootAngleX) * Mathf.Cos(bulletShootAngleY);
        bulletVelocity.Vy = bulletVelocity0.Vy * Mathf.Sin(bulletShootAngleX);
        bulletVelocity.Vz = bulletVelocity0.Vz * Mathf.Cos(bulletShootAngleX) * Mathf.Sin(bulletShootAngleY);
    }

    private void Update()
    {
        //COLLISIONS
        if (CheckForCollisionYDown(gameObject))
        {
            isGrounded = true;
            bulletVelocity.Vy = 0;
        }
        else
        {
            bulletVelocity.Vy = bulletVelocity0.Vy - gravity * mass * Time.deltaTime;
            position.Y = position0.Y + bulletVelocity.Vy * Time.deltaTime;
        }
        if (CheckForCollisionYUp(gameObject))
        {
            bulletVelocity.Vy = 0;
        }
        if (CheckForCollisionXLeft(gameObject))
        {
            bulletVelocity.Vx = 0;
        }
        if (CheckForCollisionXRight(gameObject))
        {
            bulletVelocity.Vx = 0;
        }
        if (CheckForCollisionZDown(gameObject))
        {
            bulletVelocity.Vz = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 0.05f);
        }
        if (CheckForCollisionZTop(gameObject))
        {
            bulletVelocity.Vz = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.05f);
        }

        


        position.X = position0.X + bulletVelocity0.Vx * Time.deltaTime;

        position.Z = position0.Z + bulletVelocity.Vz * Time.deltaTime;

        transform.position = new Vector3(position.X, position.Y, position.Z);

        //FRICTION
        if (isGrounded)
        {
            if (bulletVelocity.Vx > 0.2)
            {
                bulletVelocity.Vx -= 0.2f;
            }
            if (bulletVelocity.Vx < -0.2)
            {
                bulletVelocity.Vx += 0.2f;
            }
            if (bulletVelocity.Vx >= -0.2 && bulletVelocity.Vx <= 0.2)
            {
                bulletVelocity.Vx = 0;
            }
            if (bulletVelocity.Vz > 0.2)
            {
                bulletVelocity.Vz -= 0.2f;
            }
            if (bulletVelocity.Vz < -0.2)
            {
                bulletVelocity.Vz += 0.2f;
            }
            if (bulletVelocity.Vz >= -0.2 && bulletVelocity.Vz <= 0.2)
            {
                bulletVelocity.Vz = 0;
            }
        }


        // saving values
        position0 = position;
        bulletVelocity0 = bulletVelocity;

    }
}

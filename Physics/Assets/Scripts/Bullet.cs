using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CheckCollision
{
    private const float gravity = 9.81f;
    private MeshGenerator bulletMesh;
    private float mass;
    PlayerController playerController;
    Position bulletPosition, bulletPosition0;
   //float bx, by, bx0, by0;
    Velocity bulletVelocity0, bulletVelocity;

    [Range(0,180)]
    public float projectileShootAngle;

    float bulletShootAngle;
    bool isGrounded = false;

    private void Awake()
    {
        bulletMesh = GetComponent<MeshGenerator>();
        mass = GetComponent<MeshGenerator>().mass;
        collidedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        waters = GameObject.FindGameObjectsWithTag("Water");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        bulletPosition0 = new Position(transform.position.x, transform.position.y);
        bulletPosition = bulletPosition0;
        bulletVelocity0 = new Velocity(0, 0);
        bulletVelocity = bulletVelocity0;

        // DEFINING THE FORCE FOR SHOOTING THE BULLET + CURRENT PLAYER VELOCITY
        bulletVelocity0.Vx = 15f + playerController.velocity.Vx;
        bulletVelocity0.Vy = 15f + playerController.velocity.Vy;

        // ANGLE ON WHICH THE BULLET IS SHOOTED
        bulletShootAngle = projectileShootAngle * Mathf.PI/180;

        bulletVelocity.Vx = bulletVelocity0.Vx * Mathf.Cos(bulletShootAngle);
        bulletVelocity.Vy = bulletVelocity0.Vy * Mathf.Sin(bulletShootAngle);
    }

    private void Update()
    {


        
        // CHECKING COLLISION WITH THE FLOOR
        if (CheckForCollisionYDown(bulletMesh))
        {
            bulletVelocity.Vy = 0;
            isGrounded = true;
        }
        else
        {
            bulletVelocity.Vy = bulletVelocity0.Vy - gravity * mass * Time.deltaTime;
            bulletPosition.Y = bulletPosition0.Y + bulletVelocity.Vy * Time.deltaTime;
        }
        

        // FRICTION
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
        }

        //CHECKING SIDE COLLISIONS
        if (CheckForCollisionXLeft(bulletMesh))
        {
            bulletPosition.X = bulletPosition0.X + 0.1f;
            bulletVelocity.Vx = -bulletVelocity.Vx * 0.5f;
        }
        if (CheckForCollisionXRight(bulletMesh))
        {
            bulletPosition.X = bulletPosition0.X - 0.1f;
            bulletVelocity.Vx = -bulletVelocity.Vx * 0.5f;
        }
        if (!CheckForCollisionXLeft(bulletMesh) && !CheckForCollisionXRight(bulletMesh))
        {
            bulletPosition.X = bulletPosition0.X + bulletVelocity0.Vx * Time.deltaTime;
        }
        
        if (CheckForCollisionYUp(bulletMesh))
        {
            bulletPosition.Y = bulletPosition0.Y - 0.1f;
            bulletVelocity.Vy = 0;
        }

        if (CheckCollisionWithWaterDown(bulletMesh))
        {
            wasInWater = true;
            bulletVelocity.Vy += 0.2f;

        }

        if (CheckCollisionWithWaterUp(bulletMesh) && wasInWater)
        {
            bulletVelocity.Vy = bulletVelocity.Vy * 0.6f;
            if (bulletVelocity.Vy < 0.5f)
            {
                bulletVelocity.Vy = 0;
            }
            wasInWater = false;
        }


        transform.position = new Vector2(bulletPosition.X, bulletPosition.Y);

        bulletPosition0 = bulletPosition;
        bulletVelocity0.Vx = bulletVelocity.Vx;
        bulletVelocity0.Vy = bulletVelocity.Vy;

    }
}

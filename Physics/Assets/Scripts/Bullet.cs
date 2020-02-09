using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CheckCollision
{
    private const float gravity = 9.81f;
    private MeshGenerator bulletMesh;
    PlayerController playerController;
    float bx, by, bx0, by0;
    Velocity bulletVelocity0, bulletVelocity;
    float bulletShootAngle;
    bool isGrounded = false;

    private void Awake()
    {
        bulletMesh = GetComponent<MeshGenerator>();
        collidedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        waters = GameObject.FindGameObjectsWithTag("Water");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        bx0 = transform.position.x;
        by0 = transform.position.y;
        bx = bx0;
        by = by0;
        bulletVelocity0 = new Velocity(0, 0);
        bulletVelocity = bulletVelocity0;

        // DEFINING THE FORCE FOR SHOOTING THE BULLET + CURRENT PLAYER VELOCITY
        bulletVelocity0.Vx = 15f + playerController.velocity.Vx;
        bulletVelocity0.Vy = 15f + playerController.velocity.Vy;

        // ANGLE ON WHICH THE BULLET IS SHOOTED
        bulletShootAngle = 45f;

        bulletVelocity.Vx = bulletVelocity0.Vx * Mathf.Cos(bulletShootAngle);
        bulletVelocity.Vy = bulletVelocity0.Vy * Mathf.Sin(bulletShootAngle);
    }

    private void Update()
    {

        if (CheckCollisionWithWaterDown(bulletMesh))
        {
            bulletVelocity.Vy = bulletVelocity.Vy * -0.2f;
        }
        if (CheckCollisionWithWaterUp(bulletMesh))
        {
            bulletVelocity.Vy = bulletVelocity.Vy * -0.2f;
        }

        // CHECKING COLLISION WITH THE FLOOR
        if (CheckForCollisionYDown(bulletMesh))
        {
            bulletVelocity.Vy = 0;
            isGrounded = true;
        }
        else
        {
            bulletVelocity.Vy = bulletVelocity0.Vy - gravity * Time.deltaTime;
            by = by0 + bulletVelocity.Vy * Time.deltaTime * Mathf.Sin(bulletShootAngle) - (gravity / 2) * (Mathf.Pow(Time.deltaTime, 2));
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
            bx = bx0 + 0.1f;
            bulletVelocity.Vx = -bulletVelocity.Vx * 0.5f;
        }
        if (CheckForCollisionXRight(bulletMesh))
        {
            bx = bx0 - 0.1f;
            bulletVelocity.Vx = -bulletVelocity.Vx * 0.5f;
        }
        if (!CheckForCollisionXLeft(bulletMesh) && !CheckForCollisionXRight(bulletMesh))
        {
            bx = bx0 + bulletVelocity0.Vx * Time.deltaTime * Mathf.Cos(bulletShootAngle);
        }

        transform.position = new Vector2(bx, by);

        bx0 = bx;
        by0 = by;
        bulletVelocity0.Vx = bulletVelocity.Vx;
        bulletVelocity0.Vy = bulletVelocity.Vy;

    }
}

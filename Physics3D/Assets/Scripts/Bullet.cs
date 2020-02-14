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
    float bulletShootAngle, bulletShootAngle2;
    bool isGrounded = false;
    [Range(0, 180)]
    public float projectileShootAngle;
    [Range(0, 180)]
    public float projectileShootAngle2;
    private void Awake()
    {
        mass = 1;
        //collidedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        //waters = GameObject.FindGameObjectsWithTag("Water");
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
        bulletShootAngle = projectileShootAngle * Mathf.PI / 180;
        bulletShootAngle2 = projectileShootAngle2 * Mathf.PI / 180;

        bulletVelocity.Vx = bulletVelocity0.Vx * Mathf.Cos(bulletShootAngle) * Mathf.Cos(bulletShootAngle2);
        bulletVelocity.Vy = bulletVelocity0.Vy * Mathf.Sin(bulletShootAngle);
        bulletVelocity.Vz = bulletVelocity0.Vz * Mathf.Cos(bulletShootAngle) * Mathf.Sin(bulletShootAngle2);
    }

    private void Update()
    {
        position.X = position0.X + bulletVelocity0.Vx * Time.deltaTime;

        bulletVelocity.Vy = bulletVelocity0.Vy - gravity * mass * Time.deltaTime;
        position.Y = position0.Y + bulletVelocity.Vy * Time.deltaTime;

        position.Z = position0.Z + bulletVelocity.Vz * Time.deltaTime;


        transform.position = new Vector3(position.X, position.Y, position.Z);

        // saving values
        position0 = position;
        bulletVelocity0 = bulletVelocity;

    }
}

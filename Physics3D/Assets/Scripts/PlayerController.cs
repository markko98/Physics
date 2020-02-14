using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : CheckCollision
{
    public enum TypeOfFluid { Water, Propane, Mercury}
    private const float gravity = 9.81f;
    public float mass;
    float speed = 50f;
    float trenjePoda = 0.2f;
    public Position position, position0;
    public Velocity velocity0, velocity;
    bool canMoveRight = true, canMoveLeft = true, canMoveUp = true, canMoveDown = true;
    bool isGrounded = false, isJump = false;
    public Action onFire;
    private GameObject player;

    // FORCES
    Vector2 Fg;
    Vector2 Fb;
    
    // BUOYANCY
    public TypeOfFluid typeOfFluid;
    float density = 1000f;
    float liquidDisplaced;

    [Range(0,5)]
    public float buoyancyModifier;

    private void Start()
    {
        player = this.gameObject;
        position0 = new Position(transform.position.x, transform.position.y, transform.position.z);
        position = position0;
        velocity0 = new Velocity(0, 0, 0);
        velocity = velocity0;
    }


    private void Update()
    {
        // MOVING ON X
        if (Input.GetKey(KeyCode.A) && canMoveLeft)
        {
            velocity.Vx -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
            velocity.Vx += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) && canMoveUp)
        {
            velocity.Vz += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && canMoveDown)
        {
            velocity.Vz -= speed * Time.deltaTime;
        }

        // MAX VELOCITY
        if (velocity.Vx > 10)
        {
            velocity.Vx = 10;
        }
        if (velocity.Vx < -10)
        {
            velocity.Vx = -10;
        }
        if (velocity.Vz > 10)
        {
            velocity.Vz = 10;
        }
        if (velocity.Vz < -10)
        {
            velocity.Vz = -10;
        }


        position.X = position0.X + velocity0.Vx * Time.deltaTime;
        position.Z = position0.Z + velocity0.Vz * Time.deltaTime;
        transform.position = new Vector3(position0.X, position0.Y, position0.Z);


        if (CheckForCollisionYDown(player))
        {
            velocity.Vy = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            velocity.Vy = velocity0.Vy - gravity * mass * Time.deltaTime;
            position.Y = position0.Y + velocity0.Vy * Time.deltaTime - (gravity / 2) * (Mathf.Pow(Time.deltaTime, 2));
        }

        if (CheckForCollisionXLeft(player))
        {
            velocity.Vx = 0;
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }
        if (CheckForCollisionXRight(player))
        {
            velocity.Vx = 0;
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }
        if (CheckForCollisionZTop(player))
        {
            velocity.Vz = 0;
            canMoveUp = false;
        }
        else
        {
            canMoveUp = true;
        }
        if (CheckForCollisionZDown(player))
        {
            velocity.Vz = 0;
            canMoveDown = false;
        }
        else
        {
            canMoveDown = true;
        }


        // JUMPING
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJump = true;
            isGrounded = false;
            velocity.Vy = 10f;

        }

        if (isJump)
        {
            position.Y = position0.Y + velocity.Vy * Time.deltaTime - gravity / 2 * Mathf.Pow(Time.deltaTime, 2);
        }

        // GROUND FRICTION
        if (isGrounded)
        {
            isJump = false;
            if (velocity.Vx > 0.2)
            {
                velocity.Vx -= trenjePoda;
            }
            if (velocity.Vx < -0.2)
            {
                velocity.Vx += trenjePoda;
            }
            if (velocity.Vx >= -0.2 && velocity.Vx <= 0.2)
            {
                velocity.Vx = 0;
            }
            if (velocity.Vz > 0.2)
            {
                velocity.Vz -= trenjePoda;
            }
            if (velocity.Vz < -0.2)
            {
                velocity.Vz += trenjePoda;
            }
            if (velocity.Vz >= -0.2 && velocity.Vz <= 0.2)
            {
                velocity.Vz = 0;
            }
        }



        // SAVING VALUES
        position0 = position;
        velocity0 = velocity;

    }
}

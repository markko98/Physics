using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : CheckCollision
{
    private const float gravity = 9.81f;
    float x, x0, y0, y;
    float speed = 50f;
    float trenjePoda = 0.2f;
    [HideInInspector] public Velocity velocity0, velocity;
    bool canMoveRight = true, canMoveLeft = true;
    bool isGrounded = false, isJump = false;
    private MeshGenerator player;
    public Action onFire;
    private MeshGenerator water;

    private void Start()
    {
        player = GetComponent<MeshGenerator>();
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<MeshGenerator>();
        x0 = transform.position.x;
        y0 = transform.position.y;
        velocity0 = new Velocity(0, 0);
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

        // MAX VELOCITY
        if (velocity.Vx > 10)
        {
            velocity.Vx = 10;
        }
        if (velocity.Vx < -10)
        {
            velocity.Vx = -10;
        }


        x = x0 + velocity0.Vx * Time.deltaTime;

        transform.position = new Vector2(x0, y0);


        


        // CHECKING COLLISION WITH THE FLOOR
        if (CheckForCollisionYDown(player))
        {
            velocity.Vy = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            velocity.Vy = velocity0.Vy - gravity * Time.deltaTime;
            y = y0 + velocity0.Vy * Time.deltaTime - (gravity / 2) * (Mathf.Pow(Time.deltaTime, 2));
        }

        // CHECKING COLLISION WITH CEILING
        if (CheckForCollisionYUp(player))
        {
            y = y0 - 0.1f;
            velocity.Vy = velocity.Vy * (-0.25f);
        }


        //CHECKING SIDE COLLISIONS
        if (CheckForCollisionXLeft(player))
        {
            x = x0 + 0.1f;
            velocity.Vx = velocity.Vx * (-0.2f);
            canMoveLeft = false;
            canMoveRight = true;
        }
        if (CheckForCollisionXRight(player))
        {
            x = x0 - 0.1f;
            velocity.Vx = velocity.Vx * (-0.2f);
            canMoveRight = false;
            canMoveLeft = true;
        }
        if (!CheckForCollisionXRight(player) && !CheckForCollisionXLeft(player))
        {
            canMoveRight = true;
            canMoveLeft = true;
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
            y = y0 + velocity.Vy * Time.deltaTime - gravity / 2 * Mathf.Pow(Time.deltaTime, 2);
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
        }

        // FIRING BULLETS
        if (Input.GetMouseButtonDown(0))
        {
            if (onFire != null)
            {
                onFire();
            }
        }



        // BUOYANCY
        if (CheckCollisionWithWaterDown(player))
        {
            velocity.Vy += 0.2f;
        }
        //if (CheckCollisionWithWaterUp(player))
        //{
        //    velocity.Vy -= 0.2f;
        //}
        //if((velocity.Vy<= 0.2f || velocity.Vy >= 0.2f) && wasInWater)
        //{
        //    velocity.Vy = 0f;
        //}



        // SAVING VALUES
        x0 = x;
        y0 = y;
        velocity0 = velocity;
                     
    }
}

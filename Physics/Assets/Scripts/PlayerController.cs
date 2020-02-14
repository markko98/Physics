using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : CheckCollision
{
    public enum TypeOfFluid { Water, Propane, Mercury}
    private const float gravity = 9.81f;
    private float mass;
    Position position, position0;
    //float x, x0, y0, y;
    float speed = 50f;
    float trenjePoda = 0.2f;
    public Velocity velocity0, velocity;
    bool canMoveRight = true, canMoveLeft = true;
    bool isGrounded = false, isJump = false;
    private MeshGenerator player;
    public Action onFire;
    private MeshGenerator water;

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

        player = GetComponent<MeshGenerator>();
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<MeshGenerator>();
        mass = GetComponent<MeshGenerator>().mass;
        position0 = new Position(transform.position.x, transform.position.y);
        position = position0;
        velocity0 = new Velocity(0, 0);
        velocity = velocity0;
        // converting from liters to m3
        liquidDisplaced = (player.width*player.height) / 1000;

        if(typeOfFluid == TypeOfFluid.Water)
        {
            density = 1000f;
        }
        if (typeOfFluid == TypeOfFluid.Propane)
        {
            density = 493f;
        }
        if (typeOfFluid == TypeOfFluid.Mercury)
        {
            density = 13590f;
        }

        Fg.y = mass * gravity;
        Fb.y = density * gravity * liquidDisplaced;

        Debug.Log("Fg: " + Fg.y);
        Debug.Log("Fb: " + Fb.y);
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


        position.X = position0.X + velocity0.Vx * Time.deltaTime;

        transform.position = new Vector2(position0.X, position0.Y);


        


        // CHECKING COLLISION WITH THE FLOOR
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

        // CHECKING COLLISION WITH CEILING
        if (CheckForCollisionYUp(player))
        {
            velocity.Vy = velocity.Vy * (-0.25f);
        }


        //CHECKING SIDE COLLISIONS
        if (CheckForCollisionXLeft(player))
        {
            velocity.Vx = velocity.Vx * (-0.2f);
            canMoveLeft = false;
            canMoveRight = true;
        }
        if (CheckForCollisionXRight(player))
        {
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
            velocity.Vy += buoyancyModifier;

        }

        if (CheckCollisionWithWaterUp(player) && wasInWater)
        {
            velocity.Vy = velocity.Vy * 0.6f;
            if (velocity.Vy < 0.5f)
            {
                velocity.Vy = 0;
            }
            wasInWater = false;
        }





        // SAVING VALUES
        position0 = position;
        velocity0 = velocity;
                     
    }
}

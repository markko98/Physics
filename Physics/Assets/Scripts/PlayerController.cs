using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : CheckCollision
{
    public enum TypeOfFluid { Water, Propane, Mercury}
    public enum TypeOfObject { Wood, Stone, Plastic }
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

    
    // BUOYANCY
    public TypeOfFluid typeOfFluid;
    public TypeOfObject typeOfObject;
    float densityOfFluid = 1000f;
    float densityOfObject = 1000f;

    private float Fb;
    private float Fg;
    public float volumeOfObject;
    private void Start()
    {

        player = GetComponent<MeshGenerator>();
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<MeshGenerator>();
        mass = GetComponent<MeshGenerator>().mass;
        position0 = new Position(transform.position.x, transform.position.y);
        position = position0;
        velocity0 = new Velocity(0, 0);
        velocity = velocity0;

        switch (typeOfFluid)
        {
            case TypeOfFluid.Water:
                densityOfFluid = 0.1f;
                break;
            case TypeOfFluid.Mercury:
                densityOfFluid = 0.3590f;
                break;
            case TypeOfFluid.Propane:
                densityOfFluid = 0.493f;
                break;
            default:
                densityOfFluid = 0.1f;
                break;
        }
        switch (typeOfObject)
        {
            case TypeOfObject.Plastic:
                densityOfObject = 0.05f;
                break;
            case TypeOfObject.Stone:
                densityOfObject = 0.15f;
                break;
            case TypeOfObject.Wood:
                densityOfObject = 0.06f;
                break;
            default:
                densityOfObject = 0.06f;
                break;
        }


        volumeOfObject = (player.width * player.height);
        mass = volumeOfObject * densityOfObject;
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
            velocity.Vy = velocity0.Vy - gravity * Time.deltaTime;
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
            Fb = Vdisplaced * densityOfFluid * gravity;
            Fg = mass * (-gravity);
            //Fg = (1 - Vdisplaced) * densityOfObject * gravity;

            if (Mathf.Abs(Fb) > Mathf.Abs(Fg))
            {
                velocity.Vy = velocity0.Vy + Fb-Fg;
            }
            if (Mathf.Abs(Fg) > Mathf.Abs(Fb))
            {
                velocity.Vy = velocity0.Vy + Fg-Fb;
            }
        }

        if (CheckCollisionWithWaterUp(player))
        {
            velocity.Vy *= 0.6f;
        }

        
        // SAVING VALUES
        position0 = position;
        velocity0 = velocity;
                     
    }
}

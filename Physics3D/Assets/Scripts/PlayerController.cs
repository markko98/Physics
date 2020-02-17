using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : CheckCollision
{
    public enum TypeOfFluid { Water, FluidB, Mercury}
    public enum TypeOfObject { Wood, Stone, Plastic }
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
    float Fg;
    float Fb;
    
    // BUOYANCY
    public TypeOfFluid typeOfFluid;
    public TypeOfObject typeOfObject;
    float densityOfObject = 1000f;
    float densityOfFluid = 1000f;
    //[Range(0,5)]
    public float buoyancyForce;
    private float dragForce;
    private float dragCoefficientcyOfCube = 0.8f;
    private float a;
    [HideInInspector] public float volumeOfObject;

    private void Start()
    {
        player = this.gameObject;
        position0 = new Position(transform.position.x, transform.position.y, transform.position.z);
        position = position0;
        velocity0 = new Velocity(0, 0, 0);
        velocity = velocity0;
        if (typeOfFluid == TypeOfFluid.Water)
        {
            densityOfFluid = 0.1f;
        }
        if (typeOfFluid == TypeOfFluid.Mercury)
        {
            densityOfFluid = 0.2f;
        }
        if (typeOfFluid == TypeOfFluid.FluidB)
        {
            densityOfFluid = 0.05f;
        }
        if(typeOfObject == TypeOfObject.Wood)
        {
            densityOfObject = 0.06f;
        }
        if (typeOfObject == TypeOfObject.Stone)
        {
            densityOfObject = 0.15f;
        }
        if (typeOfObject == TypeOfObject.Plastic)
        {
            densityOfObject = 0.05f;
        }

        volumeOfObject = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        mass = volumeOfObject * densityOfObject;
        dragForce = (dragCoefficientcyOfCube * densityOfFluid * volumeOfObject * (10*10)) * 0.5f;
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

        //COLLISION
        if (CheckForCollisionYDown(player))
        {
            velocity.Vy = 0;
            isGrounded = true;
            isInWater = false;
        }
        else
        {
            isGrounded = false;

            velocity.Vy = velocity0.Vy - gravity * Time.deltaTime;
            position.Y = position0.Y + velocity0.Vy * Time.deltaTime - (gravity / 2) * (Mathf.Pow(Time.deltaTime, 2));
        }
        if (CheckForCollisionYUp(player))
        {
            velocity.Vy = 0;
        }

        if (CheckForCollisionXLeft(player))
        {
            velocity.Vx = 0;
            player.transform.position = new Vector3(player.transform.position.x + 0.1f, player.transform.position.y, player.transform.position.z);
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }
        if (CheckForCollisionXRight(player))
        {
            velocity.Vx = 0;
            player.transform.position = new Vector3(player.transform.position.x -0.1f, player.transform.position.y, player.transform.position.z);
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }
        if (CheckForCollisionZDown(player))
        {
            velocity.Vz = 0;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.1f);
            canMoveDown = false;
        }
        else
        {
            canMoveDown = true;
        }
        if (CheckForCollisionZTop(player))
        {
            velocity.Vz = 0;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 0.1f);
            canMoveUp = false;
        }
        else
        {
            canMoveUp = true;
        }


        //COLLISION WITH WATER
        
        if (CheckForCollisionYDownWater(player))
        {
            Fb = Vdisplaced * densityOfFluid * gravity;
            Fg = mass * (-gravity);
            
            //akceleracija buoyancy force-a
            //a = Fb / mass;

            // Akceleracija tijela nakon sto na njega djeluje buoyancy force i viskoznost
            //a = (1 / (mass)) * ((mass*gravity)-Fb-dragForce);
        }
       

        if (isInWater)
        {
            Debug.Log("Fb: " + Fb);
            Debug.Log("Fg: " + Fg);

            velocity.Vy = velocity0.Vy + Fg + Fb;
            
            //velocity.Vy = Fg + Fb;

            if (Mathf.Abs(Fg) == Mathf.Abs(Fb))
            {
                velocity.Vy = 0f;
            }

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


        // SHOOTING
        if (Input.GetMouseButtonDown(0))
        {
            if (onFire != null)
            {
                onFire();
            }
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

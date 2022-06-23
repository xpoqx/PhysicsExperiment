using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float AccelSpeed;
    public float Speed;
    public float GravityPresentOnly;
    public float gravitySpeed;

    public bool isLandingOnGround;
    private int switchDirection;
    private float horizontalMove;
    private bool isMovingHorizon;
    
    

    public RaycastHit hit;
    // Start is called before the first frame update

    public void Init()
    {
        AccelSpeed = 8f;
        Speed = Time.deltaTime*AccelSpeed;
        gravitySpeed = Speed;
    }
    void Start()
    {
        Init();
        // switchDirection = 0;
        // InvokeRepeating("PlusSwitch",2f,2f);

    }

    // Update is called once per frame
    void Update()
    {
        SetSpeed();
        SetGravity();
        MoveInput();
        MoveCalculate();
    }

    private void SetSpeed()
    {
        Speed = Time.deltaTime*AccelSpeed;
    }

    private void SetGravity()
    {
        if (gravitySpeed < Speed*10f)
        {
            gravitySpeed += Time.deltaTime/10f;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f))
        {
            if (gravitySpeed > 0)
            {
                gravitySpeed = 0;
                isLandingOnGround = true;
            }
        }

        GravityPresentOnly = gravitySpeed / Time.deltaTime;
    }

    private void Gravity()
    {
        transform.Translate(Vector3.down * gravitySpeed);
    }

    private void MoveInput()
    {
        isMovingHorizon = false;
        if (isLandingOnGround)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalMove += (-1-horizontalMove)/5f;
                isMovingHorizon = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontalMove += (1-horizontalMove)/5f;
                isMovingHorizon = true;
            }
            if (!isMovingHorizon)
            {
                if (horizontalMove > 0.01f)
                {
                    horizontalMove -= Speed;
                }
                else if (horizontalMove < -0.01f)
                {
                    horizontalMove += Speed;
                }
                else
                {
                    horizontalMove = 0;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalMove += (-1-horizontalMove)/150f;
                isMovingHorizon = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontalMove += (1-horizontalMove)/150f;
                isMovingHorizon = true;
            }
            if(!isMovingHorizon)
            {
                if (horizontalMove > 0.01f)
                {
                    horizontalMove -= Speed/10f;
                }
                else if (horizontalMove < -0.01f)
                {
                    horizontalMove += Speed/10f;
                }
                else
                {
                    horizontalMove = 0;
                }
            }
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isLandingOnGround)
            {
                gravitySpeed = -4/144f;
                isLandingOnGround = false;
            }
            else
            {
                
            }
        }
    }

    private void MoveCalculate()
    {
        float horSpeed = horizontalMove * Time.deltaTime * 4f;
        transform.Translate(Vector3.right * horSpeed);
        Gravity();
    }

    private void Jump()
    {
        
    }
    
}

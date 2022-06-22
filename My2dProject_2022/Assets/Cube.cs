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
    
    

    public RaycastHit hit;
    // Start is called before the first frame update

    public void Init()
    {
        AccelSpeed = 2f;
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
        horizontalMove = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMove += (-1-horizontalMove)/2f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMove += (1-horizontalMove)/2f;
        }
        if (Input.GetKey(KeyCode.UpArrow) && isLandingOnGround)
        {
            gravitySpeed = -4/144f;
            isLandingOnGround = false;
        }
    }

    private void MoveCalculate()
    {
        float horSpeed = horizontalMove * Time.deltaTime * 5f;
        transform.Translate(Vector3.right * horSpeed);
        Gravity();
    }

    private void Jump()
    {
        
    }
    
    private void Default()
    {
            float speed = Time.deltaTime * Speed;
            if (switchDirection == 0)
            {
                transform.Translate(-speed,0,0);
            }
            else if (switchDirection == 1)
            {
                transform.Translate(0,-speed,0);
            }
            else if (switchDirection == 2)
            {
                transform.Translate(-speed,0,0);
            }
    }

    private void PlusSwitch()
    {
        switchDirection++;
    }
}

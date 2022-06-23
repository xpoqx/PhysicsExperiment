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
    public bool canUseDoubleJump;
    private int switchDirection;
    private float horizontalMove;
    private bool isMovingHorizon;
    private bool lastMovedRight;
    
    

    public RaycastHit hit;
    // Start is called before the first frame update

    public void Init()
    {
        AccelSpeed = 8f;
        Speed = Time.deltaTime*AccelSpeed;
        gravitySpeed = Speed;
        lastMovedRight = true;
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
        { // 바닥에 착지한 상태
            if (gravitySpeed > 0)
            {
                gravitySpeed = 0;
                isLandingOnGround = true;
                canUseDoubleJump = false;
            }
        }
        else
        {
            isLandingOnGround = false;
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
        if (isLandingOnGround) // 바닥에 붙어있는 상태의 움직임
        {
            // 좌, 우 움직임 키 입력
            if (Input.GetKey(KeyCode.LeftArrow) && horizontalMove > -1f)
            {
                horizontalMove += (-1-horizontalMove)/5f;
                isMovingHorizon = true;
                lastMovedRight = false;
            }
            if (Input.GetKey(KeyCode.RightArrow) && horizontalMove < 1f) 
            {
                horizontalMove += (1-horizontalMove)/5f;
                isMovingHorizon = true;
                lastMovedRight = true;
            }
            
            // 바닥에서 좌, 우 키 입력이 없을 때 감속
            if (!isMovingHorizon)
            {
                if (horizontalMove > 0.05f)
                {
                    horizontalMove -= horizontalMove/100f+Speed;
                }
                else if (horizontalMove < -0.05f)
                {
                    horizontalMove += -horizontalMove/100f+Speed;
                }
                else
                {
                    horizontalMove = 0;
                }
            }
        }
        else // 공중에 떠 있는 상태의 움직임
        {
            // 공중에서도 좌,우로 조금씩 가속됨.
            if (Input.GetKey(KeyCode.LeftArrow)&& horizontalMove > -1f)
            {
                horizontalMove += (-1-horizontalMove)/250f;
                isMovingHorizon = true;
                lastMovedRight = false;
            }
            if (Input.GetKey(KeyCode.RightArrow)&& horizontalMove < 1f)
            {
                horizontalMove += (1-horizontalMove)/250f;
                isMovingHorizon = true;
                lastMovedRight = true;
            }
            // 공중에서는 느리게 감속됨. (속도를 어느정도 유지)
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
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.C))
        {
            if(canUseDoubleJump)
            {
                gravitySpeed = -2 / 144f;
                canUseDoubleJump = false;
                if (lastMovedRight)
                {
                    horizontalMove = 3f;
                }
                else
                {
                    horizontalMove = -3f;
                }
            }
        }
        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.C))
        {
            if (isLandingOnGround)
            {
                gravitySpeed = -4 / 144f;
                isLandingOnGround = false;
                canUseDoubleJump = true;
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

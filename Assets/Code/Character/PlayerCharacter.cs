using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 1;

    private static string PLAYER_START = "PlayerStart";
    private static string PLAYER_DEAD = "PlayerDead";
    private static string PLAYER_MOVE_RIGHT_UP = "PlayerMoveRightUp";
    private static string PLAYER_MOVE_RIGHT_DOWN = "PlayerMoveRightDown";
    private static string PLAYER_MOVE_LEFT_UP = "PlayerMoveLeftUp";
    private static string PLAYER_MOVE_LEFT_DOWN = "PlayerMoveLeftDown";
    private static string PLAYER_IDLE_RIGHT_UP = "PlayerIdleRightUp";
    private static string PLAYER_IDLE_RIGHT_DOWN = "PlayerIdleRightDown";
    private static string PLAYER_IDLE_LEFT_UP = "PlayerIdleLeftUp";
    private static string PLAYER_IDLE_LEFT_DOWN = "PlayerIdleLeftDown";

    private readonly float EPSILON = 0.00001f;
    private Vector3 lastMoveDir = Vector3.zero;
    private string lastState = PLAYER_START;
    private Animator animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        //Default state
        animator.Play(PLAYER_IDLE_RIGHT_DOWN);
        lastState = PLAYER_IDLE_RIGHT_DOWN;
    }

    private void Update()
    {
        if(!lastState.Equals(PLAYER_DEAD))
        {
            handleMovement();
        }
    }

    public void PlayerDead()
    {
        animator.Play(PLAYER_DEAD);
        lastState = PLAYER_DEAD;
    }

    private void handleMovement()
    {
        _rigidbody.velocity = Vector3.zero;
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");

        bool isIdle = Mathf.Abs(hMove) < EPSILON && Mathf.Abs(vMove) < EPSILON;
        Vector3 moveDir = new Vector3(hMove, 0, vMove).normalized;
        PlayAnimator(moveDir);
        Vector3 newPosition = transform.position + moveDir * speed * Time.deltaTime;
        _rigidbody.MovePosition(newPosition);
    }

    private void PlayAnimator(Vector3 animatorDir)
    {
        if(Mathf.Abs(animatorDir.x) < EPSILON &&
            Mathf.Abs(animatorDir.z) < EPSILON)
        {
            AnimateIdle();
        }else if (animatorDir.x > EPSILON && animatorDir.z > EPSILON)
        {
            lastMoveDir.x = animatorDir.x;
            lastMoveDir.z = animatorDir.z;
            AnimateRightUp();
        }else if (animatorDir.x > EPSILON && animatorDir.z < -EPSILON)
        {
            lastMoveDir.x = animatorDir.x;
            lastMoveDir.z = animatorDir.z;
            AnimateRightDown();
        }else if (animatorDir.x < -EPSILON && animatorDir.z > EPSILON)
        {
            lastMoveDir.x = animatorDir.x;
            lastMoveDir.z = animatorDir.z;
            AnimateLeftUp();
        }else if (animatorDir.x < -EPSILON && animatorDir.z < -EPSILON)
        {
            lastMoveDir.x = animatorDir.x;
            lastMoveDir.z = animatorDir.z;
            AnimateLeftDown();
        }
        else if (animatorDir.x > EPSILON && Mathf.Abs(animatorDir.z) < EPSILON)
        {
            lastMoveDir.x = animatorDir.x;
            AnimateRight();
        }
        else if (animatorDir.x < -EPSILON && Mathf.Abs(animatorDir.z) < EPSILON)
        {
            lastMoveDir.x = animatorDir.x;
            AnimateLeft();
        }
        else if (Mathf.Abs(animatorDir.x) < EPSILON && animatorDir.z > EPSILON)
        {
            lastMoveDir.z = animatorDir.z;
            AnimateUp();
        }
        else if (Mathf.Abs(animatorDir.x) < EPSILON && animatorDir.z < EPSILON)
        {
            lastMoveDir.z = animatorDir.z;
            AnimateDown();
        }
    }

    private void AnimateRightUp()
    {
        if (!lastState.Equals(PLAYER_MOVE_RIGHT_UP))
        {
            animator.Play(PLAYER_MOVE_RIGHT_UP);
            lastState = PLAYER_MOVE_RIGHT_UP;
        }
    }

    private void AnimateRightDown()
    {
        if (!lastState.Equals(PLAYER_MOVE_RIGHT_DOWN))
        {
            animator.Play(PLAYER_MOVE_RIGHT_DOWN);
            lastState = PLAYER_MOVE_RIGHT_DOWN;
        }
    }

    private void AnimateLeftUp()
    {
        if (!lastState.Equals(PLAYER_MOVE_LEFT_UP))
        {
            animator.Play(PLAYER_MOVE_LEFT_UP);
            lastState = PLAYER_MOVE_LEFT_UP;
        }
    }

    private void AnimateLeftDown()
    {
        if (!lastState.Equals(PLAYER_MOVE_LEFT_DOWN))
        {
            animator.Play(PLAYER_MOVE_LEFT_DOWN);
            lastState = PLAYER_MOVE_LEFT_DOWN;
        }
    }

    private void AnimateRight()
    {
        if (lastState.Equals(PLAYER_MOVE_LEFT_DOWN))
        {
            animator.Play(PLAYER_MOVE_RIGHT_DOWN);
            lastState = PLAYER_MOVE_RIGHT_DOWN;
        }else
        {
            animator.Play(PLAYER_MOVE_RIGHT_UP);
            lastState = PLAYER_MOVE_RIGHT_UP;
        }
    }

    private void AnimateLeft()
    {
        if (lastState.Equals(PLAYER_MOVE_RIGHT_DOWN))
        {
            animator.Play(PLAYER_MOVE_LEFT_DOWN);
            lastState = PLAYER_MOVE_LEFT_DOWN;
        }
        else
        {
            animator.Play(PLAYER_MOVE_LEFT_UP);
            lastState = PLAYER_MOVE_LEFT_UP;
        }
    }

    private void AnimateUp()
    {
        if (lastState.Equals(PLAYER_MOVE_RIGHT_DOWN))
        {
            animator.Play(PLAYER_MOVE_RIGHT_UP);
            lastState = PLAYER_MOVE_RIGHT_UP;
        }
        else
        {
            animator.Play(PLAYER_MOVE_LEFT_UP);
            lastState = PLAYER_MOVE_LEFT_UP;
        }
    }

    private void AnimateDown()
    {
        if (lastState.Equals(PLAYER_MOVE_RIGHT_UP))
        {
            animator.Play(PLAYER_MOVE_RIGHT_DOWN);
            lastState = PLAYER_MOVE_RIGHT_DOWN;
        }
        else
        {
            animator.Play(PLAYER_MOVE_LEFT_DOWN);
            lastState = PLAYER_MOVE_LEFT_DOWN;
        }
    }

    private void AnimateIdle()
    {
        if (!lastState.Equals(PLAYER_IDLE_LEFT_UP) &&
            !lastState.Equals(PLAYER_IDLE_LEFT_DOWN) &&
            !lastState.Equals(PLAYER_IDLE_RIGHT_UP) &&
            !lastState.Equals(PLAYER_IDLE_RIGHT_DOWN))
        {
            if (lastMoveDir.x < -EPSILON && lastMoveDir.z > EPSILON)
            {
            animator.Play(PLAYER_IDLE_LEFT_UP);
                lastState = PLAYER_IDLE_LEFT_UP;
            }
            else if (lastMoveDir.x < -EPSILON && lastMoveDir.z < -EPSILON)
            {
                animator.Play(PLAYER_IDLE_LEFT_DOWN);
                lastState = PLAYER_IDLE_LEFT_DOWN;
            }
            else if (lastMoveDir.x > EPSILON && lastMoveDir.z > EPSILON)
            {
                animator.Play(PLAYER_IDLE_RIGHT_UP);
                lastState = PLAYER_IDLE_RIGHT_UP;
            }
            else if (lastMoveDir.x > EPSILON && lastMoveDir.z < -EPSILON)
            {
                animator.Play(PLAYER_IDLE_RIGHT_DOWN);
                lastState = PLAYER_IDLE_RIGHT_DOWN;
            }else if(lastMoveDir.x < -EPSILON)
            {
                animator.Play(PLAYER_IDLE_LEFT_DOWN);
                lastState = PLAYER_IDLE_LEFT_DOWN;
            }else if (lastMoveDir.x > EPSILON)
            {
                animator.Play(PLAYER_IDLE_RIGHT_DOWN);
                lastState = PLAYER_IDLE_RIGHT_DOWN;
            }
            else if (lastMoveDir.z < -EPSILON)
            {
                animator.Play(PLAYER_IDLE_RIGHT_DOWN);
                lastState = PLAYER_IDLE_RIGHT_DOWN;
            }else if (lastMoveDir.z > EPSILON)
            {
                animator.Play(PLAYER_IDLE_RIGHT_UP);
                lastState = PLAYER_IDLE_RIGHT_UP;
            }
        }
        else
        {
            animator.Play(lastState);
        }
    }
}

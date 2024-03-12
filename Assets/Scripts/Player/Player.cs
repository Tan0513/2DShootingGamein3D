using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]PlayerInput input;

    [SerializeField]float movespeed = 10f;
    [SerializeField]float accelerationTime = 3f;
    [SerializeField]float decelerationTime = 3f;
    [SerializeField]float moveRotationAngle = 50f;
    [SerializeField]float paddingX = 0.2f;
    [SerializeField]float paddingY = 0.2f;

    new Rigidbody2D rigidbody;

    Coroutine moveCorotine;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

    void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }


    void Start()
    {
        rigidbody.gravityScale = 0f;
        input.EnableGameplayInput();
    }
    
    void Move(Vector2 moveinput)
    {
        if(moveCorotine != null)
        {
            StopCoroutine(moveCorotine);
        }
        //Quaternion moveRotation = Quaternion.AngleAxis(moveRotationAngle * moveinput.y, Vector3.right);
        //moveCorotine = StartCoroutine(MoveCorotine(accelerationTime, moveinput.normalized * movespeed, moveRotation));
        moveCorotine = StartCoroutine(MoveCorotine(accelerationTime, moveinput.normalized * movespeed, Quaternion.AngleAxis(moveRotationAngle * moveinput.y, Vector3.right)));
        StartCoroutine(MovePositionLimitCorotine());
    }

    void StopMove()
    {
        if(moveCorotine != null)
        {
            StopCoroutine(moveCorotine);
        }

        rigidbody.velocity = Vector2.zero;
        StartCoroutine(MoveCorotine(decelerationTime, Vector2.zero, Quaternion.identity));
        StopCoroutine(MovePositionLimitCorotine());
    }

    IEnumerator MoveCorotine(float time, Vector2 moveVelocity, Quaternion moveRotation)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.fixedDeltaTime / time;
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelocity, t / time);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t / time);

            yield return null;
        }
    }


    IEnumerator MovePositionLimitCorotine()
    {
        while(true)
        {
            transform.position = Viewport.Instance.PlayerMoveablePosion(transform.position, paddingX, paddingY);

            yield return null;
        }
    }
}

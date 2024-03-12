using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    float minY;
    float maxY;
    float minX;
    float maxX;

    void Start()
    {
        Camera maincamera = Camera.main;

        Vector2 bottomLeft = maincamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        Vector2 topRight = maincamera.ViewportToWorldPoint(new Vector3(1f, 1f));

        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;
    }

    public Vector3 PlayerMoveablePosion(Vector3 Playerposition, float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Clamp(Playerposition.x, minX + paddingX, maxX - paddingX);
        position.y = Mathf.Clamp(Playerposition.y, minY + paddingY, maxY - paddingY);


        return position;
    }

}

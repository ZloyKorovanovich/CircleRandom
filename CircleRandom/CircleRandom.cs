using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CircleRandom
{
    public static void GetRndomPointsInCircle(int count, Vector2 center, float radius, float randomStrength, float distance, out List<Vector2> randomPoints, int stages)
    {

        List<Vector2> points = new List<Vector2>();
        randomPoints = new List<Vector2>();

        Vector2 point;

        MakeGridInCircle(distance + randomStrength * 2, center, radius, out points);

        int randomIndex;

        if (count > points.Count - 1)
            count = points.Count - 1;

        for (int i = 0; i < count; i++)
        {
            randomIndex = Random.Range(0, points.Count - 1);
            point = points[randomIndex];

            points.RemoveAt(randomIndex);

            float k = Vector2.SqrMagnitude(point - center) / (radius * radius);
            for (int j = 0; j < stages; j++)
                k *= k;
            point += RandomDirection() * randomStrength * (1 - k);
            randomPoints.Add(point);
        }
    }

    public static void MakeGridInCircle(float distance, Vector2 center, float radius, out List<Vector2> positions)
    {
        positions = new List<Vector2>();

        float startx = center.x - radius;
        float endx = center.x + radius;

        float starty = center.y - radius;
        float endy = center.y + radius;

        Vector2 point;

        point.y = starty;

        float magnitude = radius * radius;
        float currentMagnitude;

        while (point.y <= endy)
        {
            point.x = startx;
            while (point.x <= endx)
            {
                point.x += distance;
                currentMagnitude = Vector2.SqrMagnitude(point - center);
                if (currentMagnitude <= magnitude)
                    positions.Add(point);
            }

            point.y += distance;
        }
    }

    public static Vector2 RandomDirection()
    {
        float angle = Random.Range(0.0f, 360.0f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}

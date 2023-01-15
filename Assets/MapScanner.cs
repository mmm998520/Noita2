using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScanner : MonoBehaviour
{
    Collider2D mapCollider;
    Vector2 scanPoint = Vector3.zero;
    public int scanCircleRadius = 5;

    public GameObject cube;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(scan(new Point(),scanCircleRadius));
        }
    }

    void signal()//掃描
    {
        /*
         * for (int i = -scanCircleRange; i < scanCircleRange; i++)
        {
            for (int j = -scanCircleRange; j < scanCircleRange; j++)
            {
                scanPoint.x = i;
                scanPoint.x = j;
            }
        }
        */
        if (mapCollider.bounds.Contains(scanPoint))
        {

        }
    }

    WaitForSeconds aScecond = new WaitForSeconds(1);
    IEnumerator scan(Point center, int radius)
    {
        HashSet<Point> lastPoints = new HashSet<Point>(new PointEqualityComparer());
        //HashSet<Point> allPoints = new HashSet<Point>(new PointEqualityComparer());

        for (int i = 1; i < radius; i++)
        {
            HashSet<Point> points;
            points = countCirclePoints(center, i);

            foreach (Point point in lastPoints)
            {
                if (point.x > center.x) 
                { 
                    points.Add(new Point(point.x + 1, point.y)); 
                }
                else if(point.x < center.x)
                {
                    points.Add(new Point(point.x - 1, point.y));
                }
                if (point.y > center.y)
                {
                    points.Add(new Point(point.x, point.y + 1));
                }
                else if (point.y < center.y)
                {
                    points.Add(new Point(point.x, point.y - 1));
                }
            }

            if(i == 2)
            {
                points.Add(new Point(center.x + 1, center.y + 1));
                points.Add(new Point(center.x + 1, center.y - 1));
                points.Add(new Point(center.x - 1, center.y + 1));
                points.Add(new Point(center.x - 1, center.y - 1));
            }

            //points.RemoveWhere(item => lastPoints.Contains(allPoints));
            points.ExceptWith(lastPoints);

            foreach (Point point in points)
            {
                scanPoint.x = point.x;
                scanPoint.y = point.y;
                Instantiate(cube, scanPoint, Quaternion.identity, transform);
            }

            lastPoints = points;
            //allPoints.UnionWith(points);
            yield return aScecond;
        }
    }


    HashSet<Point> countCirclePoints(Point center, int radius)
    {
        // 定義圓心座標和半徑
        int x = center.x;
        int y = center.y;

        // 用於存儲像素圓的座標的List
        HashSet<Point> circlePoints = new HashSet<Point>(new PointEqualityComparer());

        // 使用Bresenham算法獲取圓的邊界像素的座標
        int x1 = 0, y1 = radius, d = 3 - 2 * radius;

        while (x1 <= y1)
        {
            circlePoints.Add(new Point(x + x1, y + y1));
            circlePoints.Add(new Point(x + y1, y + x1));
            circlePoints.Add(new Point(x - y1, y + x1));
            circlePoints.Add(new Point(x - x1, y + y1));
            circlePoints.Add(new Point(x - x1, y - y1));
            circlePoints.Add(new Point(x - y1, y - x1));
            circlePoints.Add(new Point(x + y1, y - x1));
            circlePoints.Add(new Point(x + x1, y - y1));

            if (d < 0)
                d = d + 4 * x1 + 6;
            else
            {
                d = d + 4 * (x1 - y1) + 10;
                y1--;
            }
            x1++;
        }

        return circlePoints;
    }
}

public class Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Point()
    {
        this.x = 0;
        this.y = 0;
    }
}

class PointEqualityComparer : IEqualityComparer<Point>
{
    public bool Equals(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public int GetHashCode(Point obj)
    {
        return obj.x.GetHashCode() ^ obj.y.GetHashCode();
    }
}
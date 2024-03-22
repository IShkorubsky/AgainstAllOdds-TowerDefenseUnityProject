using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] WayPoints;

    private void Awake()
    {
        WayPoints = new Transform[transform.childCount];

        for (var i = 0; i < WayPoints.Length; i++)
        {
            WayPoints[i] = transform.GetChild(i);
        }
    }
}

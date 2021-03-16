using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float _changeSpeed;

    public float CurrentSpeed => _changeSpeed;
    public float Position => transform.position.x;
}

using System.Collections.Generic;
using UnityEngine;
using ww.Utilities.Singleton;

public class QueueManager : Singleton<QueueManager>
{

    [SerializeField]
    private int _maxQueSize = 30;
    public int MaxQueSize { get { return _maxQueSize; } set { _maxQueSize = value; } }

    [SerializeField]
    private float _waypointOffset = 1.0f;
    [SerializeField]
    private float _firstWaypointOffset = 1.0f;

    [SerializeField]
    private Transform _tableTransform;
    public Transform TableTransform => _tableTransform;

    private Vector3[] _initialWaypoints;
    private List<Vector3> _endWaypoints = new List<Vector3>();
    public List<Vector3> EndWaypoints => _endWaypoints;

    private Dictionary<Vector3, int> _initialWaypointsDictionary;
    void Start()
    {
        InitWaypoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitWaypoints()
    {
        _initialWaypoints = new Vector3[_maxQueSize];
        float m_offset;
        for (int i = 0; i < _maxQueSize; i++)
        {
            m_offset = i == 0 ? 0.0f : _firstWaypointOffset;
            _initialWaypoints[i] = _tableTransform.position + _tableTransform.forward * (_waypointOffset * (i + 1) + m_offset);
        }
        _initialWaypointsDictionary = WaypointsDictionary();
        InitEndWaypoints();
    }

    private void InitEndWaypoints()
    {
        Vector3 m_waypointCenter = _tableTransform.position + _tableTransform.right * CharacterHandler.Instance.GroupDistance;

        for (int i = 0; i < CharacterHandler.Instance.GroupSize; i++)
        {
            Vector3 m_waypointPosition = m_waypointCenter + Random.insideUnitSphere * CharacterHandler.Instance.SpawnRadius;
            m_waypointPosition.y = _tableTransform.position.y;
            _endWaypoints.Add(m_waypointPosition);
        }
    }


    // 0 if empty, 1 if occupied
    private Dictionary<Vector3, int> WaypointsDictionary()
    {
        Dictionary<Vector3, int> waypoints = new Dictionary<Vector3, int>();
        for (int i = 0; i < _maxQueSize; i++)
        {
            waypoints.Add(_initialWaypoints[i], 0);
        }

        return waypoints;
    }

    public Vector3 GetNextWaypoint()
    {
        foreach (var waypoint in _initialWaypointsDictionary)
        {
            if (waypoint.Value == 0)
            {
                _initialWaypointsDictionary[waypoint.Key] = 1;
                return waypoint.Key;
            }
        }
        return Vector3.zero;
    }
    public void ReleaseWaypoint(Vector3 waypoint)
    {
        _initialWaypointsDictionary[waypoint] = 0;
    }

    public void ReleaseAllWaypoints()
    {
        _initialWaypointsDictionary = WaypointsDictionary();
    }

}

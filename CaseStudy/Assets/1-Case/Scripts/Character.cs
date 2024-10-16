using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float _interactonDuration = 5.0f;

    private Vector3 _targetWaypoint;
    public Vector3 TargetWaypoint => _targetWaypoint;
    [SerializeField] 
    private Vector2 _speedMinMax = new Vector2(2f, 10f);

    private NavMeshAgent _navMeshAgent;
    private bool _isActivated = false;
    public bool IsActivated => _isActivated;
    public bool HasReachedTarget => Vector3.Distance(this.transform.position, _targetWaypoint) < .25f;
   // private Vector3 _targetWaypoint;
    private bool _isOnTheWay = false;
    public bool IsOnTheWay => CharacterHandler.Instance.CharactersOnTheWay.Contains(this);



    private void Awake()
    {
        _isActivated = false;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = UnityEngine.Random.Range(_speedMinMax.x, _speedMinMax.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActivated)
            MoveToTarget();
    }


    private void MoveToTarget()
    {
        if (!HasReachedTarget)
        {
            _navMeshAgent.SetDestination(_targetWaypoint);
            _navMeshAgent.isStopped = false;
        }
        else
        {
            if (IsOnTheWay)
                ReachedQueueTarget();
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _navMeshAgent.isStopped = true;
        }
    }

    public void SetTargetDestination(Vector3 target)
    {
        _targetWaypoint = target;
        if (CharacterHandler.Instance.IsFirstCharacter(this))
            StartCoroutine(StartCharacterInteraction());
    }

    private IEnumerator StartCharacterInteraction()
    {
        Image tableClock = QueueHandler.Instance.TableTransform.GetChild(1).GetChild(0).GetComponent<Image>();
        float m_timer = 0.0f;
        yield return new WaitUntil(() => HasReachedTarget);
        tableClock.fillAmount = 1;
        while (m_timer < _interactonDuration)
        {
            m_timer += Time.deltaTime;
            tableClock.fillAmount = 1 - m_timer / _interactonDuration;
            yield return null;
        }
        CharacterHandler.Instance.SendFirstCharactertoBack();
    }

    public void ActivatePlayer()
    {
        _isActivated = true;
    }

    private void ReachedQueueTarget()
    {
        CharacterHandler.Instance.CharacterReachedQueue(this);
        if (CharacterHandler.Instance.IsFirstCharacter(this))
            StartCoroutine(StartCharacterInteraction());
    }
}

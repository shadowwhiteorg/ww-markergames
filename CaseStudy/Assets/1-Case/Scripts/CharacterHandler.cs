using System.Collections.Generic;
using UnityEngine;
using ww.Utilities.Singleton;

public class CharacterHandler : Singleton<CharacterHandler>
{
    [SerializeField]
    private Character _characterPrefab;

    [SerializeField]
    private Transform _tableTransform;
    public Transform TableTransform => _tableTransform;

    [SerializeField]
    private float _groupDistance = 30.0f;
    public float GroupDistance => _groupDistance;

    [SerializeField]
    private int _groupSize = 30;
    public int GroupSize => _groupSize;

    [SerializeField]
    private float _spawnRadius = 10.0f;
    public float SpawnRadius => _spawnRadius;

    private Queue<Character> _charactersInQueue = new Queue<Character>();
    private List<Character> _characters = new List<Character>();
    private static int _currentCharacterIndex = 0;



    // Start is called before the first frame update
    void Start()
    {
        InitializeCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendNextCharaterToQueue();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SendFirstCharactertoBack();
        }
    }

    private void InitializeCharacters()
    {
        Vector3 m_groupCenter = _tableTransform.position + _tableTransform.forward * _groupDistance;
        for (int i = 0; i < _groupSize; i++)
        {
            Vector3 m_spawnPosition = m_groupCenter + Random.insideUnitSphere * _spawnRadius;
            m_spawnPosition.y = _tableTransform.position.y;
            Character character = Instantiate(_characterPrefab, m_spawnPosition, Quaternion.identity);
            character.transform.parent = this.transform;
            _characters.Add(character);
        }
    }

    private void SendNextCharaterToQueue()
    {
        // find next inactive character and send it to target
        foreach (Character character in _characters)
        {
            if (character != null && !character.IsActivated)
            {
                _charactersInQueue.Enqueue(character);
                character.SetTargetDestination(QueueHandler.Instance.GetNextWaypoint());
                character.ActivatePlayer();
                break;
            }
        }
    }

    public void SendFirstCharactertoBack()
    {
        if (_charactersInQueue.Count > 0)
        {
            Character character = _charactersInQueue.Peek();
            _charactersInQueue.Dequeue();
            _currentCharacterIndex++;
            if (_currentCharacterIndex > _characters.Count)
            {
                Debug.Log("All characters have completed interactions");
                return;

            }
            character.SetTargetDestination(QueueHandler.Instance.EndWaypoints[_currentCharacterIndex]);
            ResetQueuePositions();
        }
    }

    private void ResetQueuePositions()
    {
        QueueHandler.Instance.ReleaseAllWaypoints();
        foreach (Character character in _charactersInQueue)
        {
            character.SetTargetDestination(QueueHandler.Instance.GetNextWaypoint());
        }
    }

    private bool IsFirstCharacterInQueue(Character character) => _charactersInQueue.Peek() == character;

    public bool IsFirstCharacter(Character character)
    {
        if (_charactersInQueue.Count > 0)
            return character == _charactersInQueue.Peek();

        return false;
    }

}






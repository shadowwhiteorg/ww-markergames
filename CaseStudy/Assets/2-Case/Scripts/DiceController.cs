using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ww.Utilities.Singleton;
public class DiceController : Singleton<DiceController>
{

    [SerializeField]
    private int _maxThrowCount = 20;
    public int MaxThrowCount => _maxThrowCount;
    private int _throwCount = 0;
    public int ThrowCount
    {
        get
        {
            return _throwCount;
        }
        set
        {
            _throwCount = value;
            UIManager.Instance.UpdateThrowCountText(_maxThrowCount - _throwCount);
        }
    }
    private int _sumTotal = 0;
    public int SumTotal
    {
        get
        {
            return _sumTotal;
        }
        set
        {
            _sumTotal = value;
            UIManager.Instance.UpdateTotalText(_sumTotal);
        }
    }

    private int _total = 0;
    public int Total
    {
        get
        {
            return _total;
        }
        set
        {
            _total = value;
            UIManager.Instance.UpdateDiceTotalText(_total);
        }
    }

    [SerializeField]
    private List<int> _diceTargetValues = new List<int>();
    public List<int> DiceTargetValues => _diceTargetValues;
    [SerializeField]
    private List<int> _selectedNumbers = new List<int>();
    public List<int> SelectedNumbers => _selectedNumbers;
    [SerializeField]
    private int _currentTargetValue;

    private bool _canRollDice = true;
    public bool CanRollDice => _canRollDice;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetTargetDiceValues();
        }
    }

    public void SetTargetDiceValues()
    {
        _diceTargetValues = NumberHandler.Instance.DiceTargetList(_selectedNumbers);
    }

    public void AddToSelectedNumbers(int number)
    {
        _selectedNumbers.Add(number);
    }
    public void RollDices()
    {
        StartCoroutine(RollDiceWithDelay(NumberHandler.Instance.RollDice(_diceTargetValues[_throwCount]), 1.5f));
        _currentTargetValue = _diceTargetValues[_throwCount];
        ThrowCount++;
    }

    private IEnumerator RollDiceWithDelay(int[] dices, float delay)
    {
        int m_total = 0;
        _canRollDice = false;
        for (int i = 0; i < dices.Length; i++)
        {
            m_total += dices[i];
            UIManager.Instance.SetDiceTexts(i, dices[i]);
            yield return new WaitForSeconds(delay);
            Total = m_total;
            SumTotal += dices[i];
        }
        // Check if target is reached
        CheckTargetFound(m_total);
        _canRollDice = true;
    }

    private void CheckTargetFound(int subTotal)
    {
        for (int i = 0; i < _selectedNumbers.Count; i++)
        {
            if (_selectedNumbers[i] == subTotal)
                UIManager.Instance.ActivateSelectedNumber(i);
        }
    }

    private bool CheckIfTargetReached()
    {
        if (_total == _diceTargetValues[_throwCount])
        {
            return true;
        }
        return false;
    }
}

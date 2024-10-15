using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ww.Utilities.Singleton;
public class NumberHandler : Singleton<NumberHandler>
{
    private int _targetValue = 17;
    [SerializeField] int _sumTarget = 200;
    [SerializeField] int _numberOfSelectedNumbers = 3;
    public int NumberOfSelectedNumbers => _numberOfSelectedNumbers;

    [SerializeField]
    private int[] _diceValues = new int[3];
    [SerializeField]
    private List<int> _numberList = new List<int>();

    private List<int> _selectedNumbers = new List<int>();
    public List<int> SelectedNumbers => _selectedNumbers;


    public void AddToSelectedNumbers(int number)
    {
        _selectedNumbers.Add(number);
    }

    private int[] TargetPositionsInList()
    {
        int[] targetPositions = new int[3];
        int pos1, pos2, pos3;

        do
        {
            pos1 = UnityEngine.Random.Range(0, 10);
            pos2 = UnityEngine.Random.Range(5, 16);
            pos3 = UnityEngine.Random.Range(10, 19);
        } while (pos1 == pos2 || pos1 == pos3 || pos2 == pos3);

        targetPositions[0] = pos1;
        targetPositions[1] = pos2;
        targetPositions[2] = pos3;

        return targetPositions;
    }

    private int SumUpSelectedNumbers(List<int> selectedNumbers)
    {
        int sum = 0;
        for (int i = 0; i < selectedNumbers.Count; i++)
        {
            sum += selectedNumbers[i];
        }
        return sum;
    }


    public List<int> DiceTargetList(List<int> selectedNumbers)
    {
        List<int> m_diceTargetList = new List<int>();
        int m_sum = 200;
        int m_count = selectedNumbers.Count;
        m_sum = _sumTarget - SumUpSelectedNumbers(selectedNumbers);
        m_diceTargetList = GenerateNumberList(m_sum, 17, 3, 18, selectedNumbers);
        int[] m_targetPositions = TargetPositionsInList();
        m_diceTargetList = PlaceNumbersInTargetIndex(m_diceTargetList, selectedNumbers, m_targetPositions);
        return m_diceTargetList;
    }

    private List<int> PlaceNumbersInTargetIndex(List<int> targetList, List<int> selectedNumbers, int[] targetPositions)
    {
        for (int i = 0; i < targetPositions.Length; i++)
        {
            if (targetList.Count <= targetPositions[i] || targetPositions[i] < 0)
            {
                Debug.Log("Index out of range: " + targetPositions[i]);
                break;
            }
            int temp = targetList[targetPositions[i]];
            targetList[targetPositions[i]] = selectedNumbers[i];
            targetList.Add(temp);
        }

        return targetList;
    }


    List<int> GenerateNumberList(int totalSum, int count, int minValue, int maxValue, List<int> excludedNumbers)
    {
        System.Random random = new System.Random();
        List<int> numberList = Enumerable.Repeat(minValue, count).ToList(); // Start with all values as minValue
        int remainingSum = totalSum - (minValue * count); // Remaining sum after initializing with minimum values

        while (remainingSum > 0)
        {
            for (int i = 0; i < count && remainingSum > 0; i++)
            {
                int maxAdd = Math.Min(maxValue - numberList[i], remainingSum); // Max we can add without exceeding maxValue or remainingSum
                if (maxAdd > 0)
                {
                    int addValue;
                    do
                    {
                        addValue = random.Next(1, maxAdd + 1); // Random value to add
                    } while (excludedNumbers.Contains(numberList[i] + addValue)); // Retry if the sum is in the excluded list

                    numberList[i] += addValue;
                    remainingSum -= addValue;
                }
            }
        }

        // Check to make sure no values in the list are in the excluded numbers
        for (int i = 0; i < numberList.Count; i++)
        {
            while (excludedNumbers.Contains(numberList[i]))
            {
                // Adjust the value within the allowed range if it falls into excluded numbers
                numberList[i] = random.Next(minValue, maxValue + 1);
            }
        }

        return numberList;
    }



    #region Dice Mechanic
    public int[] RollDice(int targetValue)
    {
        int[] m_diceValues = new int[3];
        int sum = 0;

        do
        {
            sum = 0;

            for (int i = 0; i < 3; i++)
            {
                m_diceValues[i] = UnityEngine.Random.Range(1, 7);
                sum += m_diceValues[i];
            }

        } while (sum != targetValue);

        m_diceValues = RandomizeArrayOrder(m_diceValues);

        return m_diceValues;
    }

    private int[] RandomizeArrayOrder(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int randomIndex = UnityEngine.Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        return array;
    }
    #endregion

}
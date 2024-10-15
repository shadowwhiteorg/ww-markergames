using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ww.Utilities.Singleton;
public class UIManager : Singleton<UIManager>
{
    #region Number Selection Fields
    [SerializeField]
    GameObject _numberSelectionPanel;
    [SerializeField]
    int numberOfButtons;
    [SerializeField]
    int numberOfSelectedNumbers;
    [SerializeField]
    Button _buttonPrefab;
    [SerializeField]
    Image _buttonGrid;
    [SerializeField]
    Image _numbeGrid;
    [SerializeField]
    Image _selectedNumberPrefab;
    #endregion

    #region Roll Dice Fields
    [SerializeField]
    GameObject _rollDicePanel;
    [SerializeField]
    TextMeshProUGUI _throwCountText;
    [SerializeField]
    TextMeshProUGUI _totalText;
    [SerializeField]
    GameObject _dicesParent;
    [SerializeField]
    TextMeshProUGUI _diceTotalText;
    [SerializeField]
    GameObject _selectedNumbersParent;
    [SerializeField]
    Button _rollButton;

    [SerializeField]
    Sprite[] _diceSides;
    #endregion

    private List<TextMeshProUGUI> _selectedNumberTexts = new List<TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        InitButtons();
        InitSelectedNumberGrid();
    }

    private void InitButtons()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            int m_numberToSet;
            m_numberToSet = i + 3;
            Button button = Instantiate(_buttonPrefab, _buttonGrid.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = (m_numberToSet).ToString();
            button.onClick.AddListener(() => Selectumber(m_numberToSet));
            NumberHandler.Instance.AddToSelectedNumbers(m_numberToSet);
        }

        _rollButton.onClick.AddListener(() =>
        {
            if (DiceController.Instance.CanRollDice)
                DiceController.Instance.RollDices();
        });

    }

    private void InitSelectedNumberGrid()
    {
        for (int i = 0; i < numberOfSelectedNumbers; i++)
        {
            Image selectedNumber = Instantiate(_selectedNumberPrefab, _numbeGrid.transform);
            _selectedNumberTexts.Add(selectedNumber.GetComponentInChildren<TextMeshProUGUI>());
            _selectedNumberTexts[i].gameObject.SetActive(false);
        }
        _diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void Selectumber(int number)
    {
        for (int i = 0; i < _selectedNumberTexts.Count; i++)
        {
            if (!_selectedNumberTexts[i].gameObject.activeInHierarchy)
            {
                _selectedNumberTexts[i].text = number.ToString();
                DiceController.Instance.AddToSelectedNumbers(number);
                _selectedNumberTexts[i].gameObject.SetActive(true);
                break;
            }
            if (i == _selectedNumberTexts.Count - 2)
            {
                StartCoroutine(ActivateRollPanelWithDelay(1));
            }
        }
    }

    private IEnumerator ActivateRollPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _numberSelectionPanel.SetActive(false);
        _rollDicePanel.SetActive(true);
        SetRollDicePanel();
        DiceController.Instance.SetTargetDiceValues();

    }

    private void SetRollDicePanel()
    {
        for (int i = 0; i < _selectedNumbersParent.transform.childCount; i++)
        {
            _selectedNumbersParent.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = DiceController.Instance.SelectedNumbers[i].ToString();
        }

        _throwCountText.text = DiceController.Instance.MaxThrowCount.ToString();
        _totalText.text = DiceController.Instance.SumTotal.ToString();
    }

    public void UpdateTotalText(int total)
    {
        _totalText.text = total.ToString();
    }

    public void UpdateThrowCountText(int throwCount)
    {
        _throwCountText.text = throwCount.ToString();
    }

    public void SetDiceTexts(int count, int value)
    {
        if (_dicesParent.transform.GetChild(count))
        {
            if (!_dicesParent.transform.GetChild(count).gameObject.activeInHierarchy)
                _dicesParent.transform.GetChild(count).gameObject.SetActive(true);
            _dicesParent.transform.GetChild(count).GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
            StartCoroutine(RollDiceSides(count, value - 1));
        }
    }

    private IEnumerator RollDiceSides(int index, int targetFace)
    {
        int m_randomFace;
        for (int i = 0; i < 10; i++)
        {
            m_randomFace = Random.Range(0, 6);
            _dicesParent.transform.GetChild(index).GetChild(1).GetComponent<Image>().sprite = _diceSides[m_randomFace];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.25f);
        _dicesParent.transform.GetChild(index).GetChild(1).GetComponent<Image>().sprite = _diceSides[targetFace];
    }

    public void ActivateSelectedNumber(int index)
    {
        _selectedNumbersParent.transform.GetChild(index).GetChild(1).gameObject.SetActive(true);
    }

    public void UpdateDiceTotalText(int value)
    {
        if (!_diceTotalText.gameObject.activeInHierarchy)
            _diceTotalText.gameObject.SetActive(true);
        _diceTotalText.text = value.ToString();
    }

}

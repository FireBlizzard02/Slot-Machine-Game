using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public SlotMachineController slotMachine;
    public HandleController handleController;

    [Header("Texts (TMP)")]
    public TMP_Text balanceText;

    [Header("Bet Buttons Group")]
    public GameObject betButtonsParent;
    public Button bet10Button;
    public Button bet50Button;
    public Button bet100Button;

    private void OnEnable()
    {
        SlotMachineController.OnSpinComplete += OnSpinFinished; 
    }

    private void OnDisable()
    {
        SlotMachineController.OnSpinComplete -= OnSpinFinished;
    }

    private void Start()
    {
        if (bet10Button) bet10Button.onClick.AddListener(() => SetBetAndSpin(10));
        if (bet50Button) bet50Button.onClick.AddListener(() => SetBetAndSpin(50));
        if (bet100Button) bet100Button.onClick.AddListener(() => SetBetAndSpin(100));

        UpdateUI();
    }

    private void SetBetAndSpin(int amount)              // start the spin and set betting amount
    {
        if (BetAndCurrencyManager.Instance.currentBalance >= amount)
        {
            BetAndCurrencyManager.Instance.currentBet = amount;

            SetBetButtonsActive(false);

            handleController.PullHandle();
        }

        UpdateUI();
    }

    private void OnSpinFinished()                      // button enabled
    {
        SetBetButtonsActive(true);
        UpdateUI();
    }

    private void UpdateUI()                          // update ui elements
    {
        if (balanceText)
            balanceText.text = $"Balance: {BetAndCurrencyManager.Instance.currentBalance}";

        // disable buttons if balance < bettingAmount
        if (bet10Button) bet10Button.interactable = BetAndCurrencyManager.Instance.currentBalance >= 10;
        if (bet50Button) bet50Button.interactable = BetAndCurrencyManager.Instance.currentBalance >= 50;
        if (bet100Button) bet100Button.interactable = BetAndCurrencyManager.Instance.currentBalance >= 100;
    }

    private void SetBetButtonsActive(bool active)                 // 
    {
        if (betButtonsParent != null)
            betButtonsParent.SetActive(active);
    }
}

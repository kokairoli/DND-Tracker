using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private GameObject turnCounterObject;
    [SerializeField] private GameObject actionTextObject;
    [SerializeField] private GameObject initialActionButtonObject;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject addUnitButton;
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject bonusActionPanel;
    [SerializeField] private GameObject actionPrefab;
    [SerializeField] private GameObject bonusActionPrefab;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject darkBackground;
    [SerializeField] private GameObject availableActionsPanel;



    private void Start()
    {
        Debug.Log(initialActionButtonObject.GetComponent<Button>());
        initialActionButtonObject.GetComponent<Button>().Select();
    }

    public void CreateFloatingText(Vector3 position, string text)
    {
        if (floatingTextPrefab)
        {
            GameObject floatingText = Instantiate(floatingTextPrefab, position, Quaternion.identity);
            floatingText.GetComponentInChildren<TextMeshPro>().SetText(text);
        }
        
    }

    public void UpdateResourcesPanel(Resources resources)
    {
        foreach (Transform child in actionPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in bonusActionPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < resources.CurrentActionPoints; i++)
        {
            GameObject actionImage = Instantiate(actionPrefab);
            actionImage.transform.SetParent(actionPanel.transform);
        }
        for (int i = 0; i < resources.CurrentBonusActionPoints; i++)
        {
            GameObject actionImage = Instantiate(bonusActionPrefab);
            actionImage.transform.SetParent(bonusActionPanel.transform);
        }
    }

    

    public void OpenSpellPanel()
    {
        spellPanel.SetActive(true);
    }

    public void CloseSpellPanel()
    {
        spellPanel.SetActive(false);
    }

    public void CloseAvailableActionPanel()
    {
        availableActionsPanel.SetActive(false);
    }

    public void OpenAvailableActionPanel()
    {
        availableActionsPanel.SetActive(true);
    }

    public void OpenInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        darkBackground.SetActive(true);
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        darkBackground.SetActive(false);
    }

    public void SetTurnCounter(int turn)
    {
        turnCounterObject.GetComponent<TextMeshProUGUI>().SetText(turn.ToString());
    }

    public void SetActionText(Action action)
    {
        actionTextObject.GetComponent<TextMeshProUGUI>().SetText(action.ToString());
    }

    public void DisableStartBattleAndAddUnitButton()
    {
        startButton.SetActive(false);
        addUnitButton.SetActive(false);
    }
}

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

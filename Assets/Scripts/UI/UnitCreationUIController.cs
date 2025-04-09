using UnityEngine;

public class UnitCreationUIController : MonoBehaviour
{
    [SerializeField] private GameObject playableCharacterPrefab;
    [SerializeField] private GameObject enemyCharacterPrefab;
    [SerializeField] private GameObject unitCreationCanvas;
    private GameObject selectedPrefab;

    public void PlayerPrefabSelected()
    {
        selectedPrefab = playableCharacterPrefab;
        DisableUnitCreationWindow();
    }

    public void EnemyPrefabSelected()
    {
        selectedPrefab = enemyCharacterPrefab;
        DisableUnitCreationWindow();
    }

    public bool IsPrefabSelected()
    {
        return selectedPrefab != null;
    }


    public GameObject GetSelectedPrefab()
    {
        return selectedPrefab;
    }

    public void DisableUnitCreationWindow()
    {
        unitCreationCanvas.SetActive(false);
    }

    public void EnableUnitCreationWindow()
    {
        unitCreationCanvas.SetActive(true);
    }




}

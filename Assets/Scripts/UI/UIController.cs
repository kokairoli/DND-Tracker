using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;


    public void CreateFloatingText(Vector3 position, string text)
    {
        if (floatingTextPrefab)
        {
            GameObject floatingText = Instantiate(floatingTextPrefab, position, Quaternion.identity);
            floatingText.GetComponentInChildren<TextMeshPro>().SetText(text);
            
        }
        
    }
}

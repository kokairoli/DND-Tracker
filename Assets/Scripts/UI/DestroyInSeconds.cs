using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 2f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}

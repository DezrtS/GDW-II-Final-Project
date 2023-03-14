using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float destroyAfter;

    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }
}

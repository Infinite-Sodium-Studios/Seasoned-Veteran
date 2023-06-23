using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    private GameObject baseObject;

    public void Init(GameObject _baseObject) {
        baseObject = _baseObject;
    }

    public bool IsSameTypeAs(GameObject other)
    {
        return baseObject == other;
    }
}

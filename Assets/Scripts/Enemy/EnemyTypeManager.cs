using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    public GameObject instantiationRoot;

    public void Init(GameObject baseEnemyObject)
    {
        var baseEnemyTypeManager = baseEnemyObject.GetComponent<EnemyTypeManager>();
        Debug.Assert(baseEnemyTypeManager != null, "Can only instantiate enemy off of object with EnemyTypeManager component");
        instantiationRoot = baseEnemyTypeManager.GetInstantiationRoot();
    }

    GameObject GetInstantiationRoot()
    {
        return instantiationRoot != null ? instantiationRoot : gameObject;
    }

    public bool IsSameEnemyType(GameObject other)
    {
        var otherEnemyTypeManager = other.GetComponent<EnemyTypeManager>();
        Debug.Assert(otherEnemyTypeManager != null, "Enemy object must have EnemyTypeManager component");
        return GetInstantiationRoot() == otherEnemyTypeManager.GetInstantiationRoot();
    }
}

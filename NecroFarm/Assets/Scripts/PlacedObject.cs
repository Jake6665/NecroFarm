using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{


    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, ScriptableObjects.Dir dir, ScriptableObjects scriptableObject)
    {
        Transform placedObjectTransform = Instantiate(scriptableObject.prefab, worldPosition, Quaternion.Euler(0, scriptableObject.GetRotationAngle(dir), 0));
        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.scriptableObject = scriptableObject;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }
    private ScriptableObjects scriptableObject;
    private Vector2Int origin;
    private ScriptableObjects.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return scriptableObject.GetGridPositionList(origin, dir);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}

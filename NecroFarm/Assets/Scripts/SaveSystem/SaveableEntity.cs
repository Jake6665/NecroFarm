using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//Place this script on objects we want to save data on (crops, units, gameManager, ect)
//
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string id = string.Empty;

    public string Id => id;

    [ContextMenu("Generate Id")]
    private void GenerateId() => id = Guid.NewGuid().ToString();


    //Capture state of each var saved on the game object marked as ISavable
    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }

        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if(stateDictionary.TryGetValue(typeName, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }

    public void createGUID()
    {
        if(id != null)
        {
            //GenerateId();
        }
    }
}

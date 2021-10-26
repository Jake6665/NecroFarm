using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileSave : MonoBehaviour, ISaveable
{
    [SerializeField] private int money = 100;
    [SerializeField] private int parts = 0;

    public object CaptureState()
    {
        return new SaveData
        {
            money = money,
            parts = parts
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        money = saveData.money;
        parts = saveData.parts;
    }

    [Serializable]
    private struct SaveData
    {
        public int money;
        public int parts;
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CropType
{
    Carrot,
    Corn,
    Pig
}
[System.Serializable]
public class Crops
{
    public string id;

    public CropType cropType;

    public DateTime buildTime;

    public Vector3 position;

    public Quaternion rortaion;
}

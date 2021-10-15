using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Character_Collision : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider characterBlockerCollider;


    void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }
}

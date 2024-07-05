using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Draggable : MonoBehaviour
{
    private const string TagOfDraggableItems = "Draggable";
    private Rigidbody _rigidbody;
    private const int DefaultLayerValue = 0;
    private const int DraggableLayerValue = 6;
    void Start()
    {
        gameObject.tag = TagOfDraggableItems;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PrepareForDrag()
    {
        gameObject.layer = DraggableLayerValue;
        _rigidbody.useGravity = false;
        
    }
    
    public void PrepareForDrop()
    {
        gameObject.layer = DefaultLayerValue;
        _rigidbody.useGravity = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    public void DropWithForce(Vector3 dropDirection, float dropForce)
    {
        gameObject.layer = DefaultLayerValue;
        _rigidbody.useGravity = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _rigidbody.AddForce(dropDirection*dropForce,ForceMode.Impulse);
    }
}

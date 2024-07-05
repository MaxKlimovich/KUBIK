using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DragNDrop : NetworkBehaviour
{
    private const string TagOfDraggableItems = "Draggable";
    [SerializeField] private int _dropForce = 1000;
    [SerializeField] private int _speedOfDrag = 5; 
    [SerializeField] private KeyCode _dragKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _forceDragKey = KeyCode.Mouse1;
    [SerializeField] private int MaxRayDistance = 3;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _pickUpSocket;
    [SerializeField] private LayerMask _defaultLayerMask;
    private GameObject _draggableObject;
    private Rigidbody _rbOfdraggableObject;
    
    
    void Start()
    {
        if (!isLocalPlayer) return;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position,_camera.forward,out hit,MaxRayDistance,_defaultLayerMask))
        {
            if (hit.transform.CompareTag(TagOfDraggableItems))
            {
                Debug.Log("See Draggable");
                if (Input.GetKeyDown(_dragKey))
                {
                    PrepareForDrag(hit); 
                }
            }
        }
        if (_draggableObject != null)
        {
            CheckDropButton();
            CheckDropWithForceButton();
        }
    }

    
    private void CheckDropWithForceButton()
    {
        if (Input.GetKeyDown(_forceDragKey))
        {
            DropWithForce();
        }
    }

    
    private void CheckDropButton()
    {
        if (Input.GetKeyUp(_dragKey))
        {
            Drop();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(_dragKey) && _draggableObject != null)
        {
            Drag();
        }
    }

    private void Drag()
    {
        Vector3 dragDirection = _pickUpSocket.position - _draggableObject.transform.position;
        _rbOfdraggableObject.velocity = dragDirection * _speedOfDrag;
        
    }

    private void Drop()
    {
        _draggableObject.GetComponent<Draggable>().PrepareForDrop();
        _draggableObject = null;
        _rbOfdraggableObject = null;
    }
    
    private void DropWithForce()
    {
        _draggableObject.GetComponent<Draggable>().DropWithForce(_camera.forward, _dropForce);
        _draggableObject = null;
        _rbOfdraggableObject = null;
    }

    
    private void PrepareForDrag(RaycastHit hit)
    {
        _draggableObject = hit.transform.gameObject;
        _rbOfdraggableObject = _draggableObject.GetComponent<Rigidbody>();
        _draggableObject.GetComponent<Draggable>().PrepareForDrag();
    }
}

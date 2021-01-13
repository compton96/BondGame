//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ProcGenInputHandling : MonoBehaviour
{
    public NodeInfo currInfo;

    void OnUp(InputValue value){

    }

    void OnLeftClick() 
    {
        Debug.Log("LeftCLick");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit something");
            if(hit.transform.tag == "PH") 
            {
                Debug.Log("Hit node");
                hit.transform.GetComponent<NodePH>().onClicked(currInfo);
            }
        } 
    }

   
}

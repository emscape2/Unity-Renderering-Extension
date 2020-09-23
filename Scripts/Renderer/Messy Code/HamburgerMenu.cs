using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Networking.UnityWebRequest;

public class HamburgerMenu : MonoBehaviour
{
    bool engaged;
    RectTransform rektTransform;
    

    public ButtonRowPivotScript Response;
    //todo, settable acties definieren hiervoor 
    // Start is called before the first frame update
    void Start()
    {
        if (engaged)
        {
            engaged = false;
        }
    }

    private void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.LogError("enter werkt");
    }

    private void OnMouseUpAsButton()
    {
        Debug.LogError("Button Werkt");

    }



    // Update is called once per frame
    void OnGUI()
    {
    }
}

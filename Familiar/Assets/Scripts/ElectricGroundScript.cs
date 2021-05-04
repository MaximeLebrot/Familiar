using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Simon Canbäck
public class ElectricGroundScript : MonoBehaviour, IZappable
{
    private List<GameObject> listOfContacts;

    public bool IsZapped
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Start()
    {
        listOfContacts = new List<GameObject>();
    }

    void LateUpdate()
    {
        ValidateContacts();

        IsZapped = false;
    }

    private void ValidateContacts()
    {
        List<GameObject> tempList = new List<GameObject>();

        foreach (GameObject go in listOfContacts)
        {
            if (go == null)
                tempList.Add(go);
        }

        foreach (GameObject go in tempList)
            listOfContacts.Remove(go);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collider added");

        if (collision.gameObject.GetComponent<IZappable>() != null)
            listOfContacts.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("collider removed");

        listOfContacts.Remove(collision.gameObject);
    }

    public void OnZap()
    {
        foreach (GameObject go in listOfContacts)
        {
            if (go == null)
                continue;

            foreach (IZappable iz in go.GetComponents<IZappable>())
            {
                if (iz.IsZapped)
                    continue;

                iz.OnZap();
            }
        }
    }
}

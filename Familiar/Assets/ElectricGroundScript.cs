using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGroundScript : MonoBehaviour, IZappable
{
    private List<GameObject> listOfContacts;

    // Start is called before the first frame update
    void Start()
    {
        listOfContacts = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collider added");
        if (collision.gameObject.GetComponent<IZappable>() != null)
            listOfContacts.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collider removed");
        listOfContacts.Remove(collision.gameObject);
    }

    public void OnZap()
    {
        foreach (GameObject go in listOfContacts)
        {
            foreach (IZappable iz in go.GetComponents<IZappable>())
                iz.OnZap();
        }
    }
}

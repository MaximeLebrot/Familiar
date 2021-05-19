using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsScript : MonoBehaviour
{
    private GrabObjectScript gos;
    [SerializeField] private Transform resetPoint;

    private void Awake()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (go.TryGetComponent(out gos))
            {
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
            return;

        if (collision.gameObject.CompareTag("Moveable"))
            gos.DropObject();

        collision.gameObject.transform.position = resetPoint.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Simon Canbäck
public class ArachnophobeModeScript : MonoBehaviour
{
    [SerializeField]
    private Mesh spider;
    [SerializeField]
    private GameObject notSpider; 
    [SerializeField]
    private Material spiderMaterial;
    [SerializeField]
    private Material notSpiderMaterial;

    public static bool IsSpiderModeOn
    {
        get;
        set;
    }

    private SkinnedMeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<SkinnedMeshRenderer>();
        IsSpiderModeOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SpiderToggle"))//Input.GetKeyDown(KeyCode.P))
        {
            mesh.enabled = !mesh.enabled;
            notSpider.gameObject.SetActive(!notSpider.gameObject.activeSelf);
            IsSpiderModeOn = !IsSpiderModeOn;
            //mesh.sharedMesh = mesh.sharedMesh == spider ? notSpider : spider;
            //mesh.sharedMaterial = mesh.sharedMaterial == spiderMaterial ? notSpiderMaterial : spiderMaterial;
            //mesh.material = mesh.material == spiderMaterial ? notSpiderMaterial : spiderMaterial;
        }
    }
}

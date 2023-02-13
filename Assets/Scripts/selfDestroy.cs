using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DissableBoxCollider()
    {
        boxCollider.enabled = false;
    }

    private void Suicide()
    {
        Object.Destroy(this.gameObject);
    }
}

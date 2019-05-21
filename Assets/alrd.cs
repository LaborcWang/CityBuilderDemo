using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alrd : MonoBehaviour
{

    private bool _alreadybuild = false;
    // Start is called before the first frame update
 
    private void OnTriggerStay(Collider other)
    {
        _alreadybuild = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _alreadybuild = false;
    }

    public void OnMouseUp()
    {
        if (_alreadybuild == true)
        {
            Destroy(this.gameObject);
        }
    }
}

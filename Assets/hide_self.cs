using UnityEngine;
using System.Collections;

public class hide_self : MonoBehaviour {

	public void hide_parent()
    {
        
        transform.parent.gameObject.SetActive(false);
    }
}

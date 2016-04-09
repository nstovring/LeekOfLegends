using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Flashing : MonoBehaviour
{

    public Text text;
    private bool changeScene = false;


	// Use this for initialization
	void Start () {

	    

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Attack"))
        {
            SceneManager.LoadScene(1);
        }


        text.color = new Color(Mathf.Sin(Time.time)*1.5F,0,0);
	}

    
   
}

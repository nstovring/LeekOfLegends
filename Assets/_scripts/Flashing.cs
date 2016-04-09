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

        if(changeScene)
        SceneManager.LoadScene(1);

    }
	
	// Update is called once per frame
	void Update () {
	
        
        text.color = new Color(Mathf.Sin(Time.time)*1.5F,0,0);
	}

    public void sceneChanging()
    {
        changeScene = true;
    }
    
   
}

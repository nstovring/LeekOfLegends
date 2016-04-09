using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    //public delegate void PointAction();
    //public static event PointAction PointReceived;

    public Text playerPointsText;

    public static int playerPoints;
    // Use this for initialization
    void Start () {
	
	}

    public static void AddPoints(int points)
    {
        playerPoints += points;
    }

	// Update is called once per frame
	void Update ()
	{
	    playerPointsText.text = "Points :" + playerPoints;
	}
}

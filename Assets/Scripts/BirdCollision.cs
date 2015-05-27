using UnityEngine;
using System.Collections;

public class BirdCollision : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameSystem.Instance.currentGameState = GameSystem.GameState.GameOver;
    }
}

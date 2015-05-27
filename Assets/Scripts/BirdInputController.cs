using UnityEngine;
using System.Collections;

public class BirdInputController : MonoBehaviour
{
    public float liftForce = 0;

    private Rigidbody2D m_rigidBody;

	// Use this for initialization
	void Start ()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0) && m_rigidBody.velocity.y < 0)
        {
            m_rigidBody.AddRelativeForce(new Vector2(0, liftForce));
        }
	}
}

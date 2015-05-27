using UnityEngine;
using System.Collections;

public class PipeMoving : MonoBehaviour
{
    public float moveSpeed;
    public bool moving;
    private float m_fLeftMargin;

    // Use this for initialization
    void Start ()
    {
        m_fLeftMargin = Camera.main.orthographicSize * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!moving || GameSystem.Instance.currentGameState == GameSystem.GameState.GameOver)
        {
            return;
        }

        Vector3 currentPos = transform.position;
        currentPos.x -= moveSpeed * Time.deltaTime;
        transform.position = currentPos;

        if (currentPos.x < -m_fLeftMargin)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour
{
    public float scrollSpeed = 0;

    private MeshRenderer m_meshRenderer;
    private Material m_material;

	// Use this for initialization
	void Start ()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_material = m_meshRenderer.sharedMaterial;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameSystem.Instance.currentGameState == GameSystem.GameState.Running)
        {
            m_material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
        }
    }
}

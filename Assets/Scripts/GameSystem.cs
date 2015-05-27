using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    private const int               k_iNumActivePipes       = 10;
    private float                   k_fMinPipeScaleHeight   = 0.25f;
    private float                   k_fMaxPipeScaleHeight   = 0.6f;

    public enum GameState
    {
        Running,
        GameOver
    };

    public GameState                currentGameState;

    private static GameSystem       m_instance;
    private GameObject              m_pipe;
    private Vector3                 m_pipeRect;
    private float                   m_fGroundHeight;
    private Vector3                 m_birdStartPos;
    private GameObject[]            m_activePipes = new GameObject[k_iNumActivePipes];
    private GameObject              m_bird;
    private Rigidbody2D             m_birdRigidBody;

    public static GameSystem Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }

    void OnDestroy()
    {
        m_instance = null;
    }

    void Start ()
    {
	    m_pipe = GameObject.Find("pipe");
        SpriteRenderer pipeRenderer = m_pipe.GetComponent<SpriteRenderer>();
        m_pipeRect = pipeRenderer.sprite.bounds.size;

        GameObject ground = GameObject.Find("ground");
        m_fGroundHeight = ground.transform.localScale.y;

        m_bird = GameObject.Find("bird");
        m_birdStartPos = m_bird.transform.position;
        m_birdRigidBody = m_bird.GetComponent<Rigidbody2D>();
        currentGameState = GameState.Running;
    }

	void Update ()
    {
        if (currentGameState == GameState.Running)
        {
            GeneratePipes();
        }
        else if (currentGameState == GameState.GameOver && Input.GetMouseButtonDown(0))
        {
            // Reset bird's state
            m_birdRigidBody.velocity = Vector3.zero;
            m_birdRigidBody.angularVelocity = 0;
            m_bird.transform.rotation = Quaternion.identity;
            m_bird.transform.position = m_birdStartPos;

            // Reset pipe's state
            for (int i = 0; i < k_iNumActivePipes; i++)
            {
                Destroy(m_activePipes[i].gameObject);
            }

            // Switch game state
            currentGameState = GameState.Running;
        }
    }

    void GeneratePipes()
    {
        float startX = 2 * Camera.main.orthographicSize * Camera.main.aspect;

        for (int i = 0; i < k_iNumActivePipes; i++)
        {
            if (m_activePipes[i] != null && m_activePipes[i].transform.position.x > startX)
            {
                startX = m_activePipes[i].transform.position.x;
            }
        }

        for (int i = 0; i < k_iNumActivePipes; i++)
        {
            if (m_activePipes[i] == null)
            {
                m_activePipes[i] = GeneratePipe(startX);
                startX = m_activePipes[i].transform.position.x + m_pipeRect.x;
            }
        }
    }

    GameObject GeneratePipe(float startX)
    {
        // Clone a pipe
        GameObject pipe = Instantiate(m_pipe);

        // Random pipe type
        int frameId = Random.Range(0, 2);
        SpriteRenderer pipeRenderer = pipe.GetComponent<SpriteRenderer>();
        pipeRenderer.sprite = pipe.GetComponent<PipeAnimator>().sprFrames[frameId];

        // Random pipe length
        pipe.transform.localScale = new Vector3(1.0f, Random.Range(k_fMinPipeScaleHeight, k_fMaxPipeScaleHeight), 1.0f);

        // Random distance between two nearby pipes
        Vector3 pipeRect = m_pipeRect;
        pipeRect.y *= pipe.transform.localScale.y;
        float x = startX + Random.Range(k_iNumActivePipes >> 2, k_iNumActivePipes) * pipeRect.x;
        float y = (frameId == 0) ? (pipeRect.y / 2 + m_fGroundHeight) : (2 * Camera.main.orthographicSize - pipeRect.y / 2);
        pipe.transform.position = new Vector2(x, y);
        pipe.GetComponent<PipeMoving>().moving = true;

        return pipe;
    }
}

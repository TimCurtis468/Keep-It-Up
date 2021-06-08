using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton
    private static Paddle _instance;
    public static Paddle Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private Camera mainCamera;
    private float paddleInitialY;
    private float leftClamp = 0;
    private float rightClamp = 410;
    private float screenEdgeOffset = 0.1f;
    private SpriteRenderer sr;
    private BoxCollider2D boxCol;

    private Vector2 screenBounds;
    private float objectWidth;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleInitialY = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = sr.bounds.extents.x; //extents = size of width / 2

        leftClamp = -screenBounds.x + (objectWidth + screenEdgeOffset);
        rightClamp = screenBounds.x - (objectWidth + screenEdgeOffset);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        float mousePositionPixels = Input.mousePosition.x;
        float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        mousePositionWorldX = Mathf.Clamp(mousePositionWorldX, leftClamp, rightClamp);
        transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);
    }
}

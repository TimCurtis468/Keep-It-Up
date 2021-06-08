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
    private float LeftClamp = 0;
    private float RightClamp = 410;
    private float paddleOffset = 0.1f;
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

        LeftClamp = -screenBounds.x + (objectWidth + paddleOffset);
        RightClamp = screenBounds.x - (objectWidth + paddleOffset);

        Debug.Log("LeftClamp: " + LeftClamp.ToString());
        Debug.Log("RightClamp: " + RightClamp.ToString());

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
        mousePositionWorldX = Mathf.Clamp(mousePositionWorldX, LeftClamp, RightClamp);
        transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);

        //       Debug.Log("Input.mousePosition.x: " + Input.mousePosition.x.ToString() + "\r\n" + "mousePositionWorldX: " + mousePositionWorldX.ToString());

#if (PI)
        float paddleShift = (defaultPaddleWidthInPixels - ((defaultPaddleWidthInPixels / 2) * sr.size.x)) / 2;
        float leftClamp = objectWidth / 2;//defaultLeftClamp - paddleShift;
        float rightClamp = defaultRightClamp + paddleShift;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);
//     Debug.Log("Input.mousePosition.x: " + Input.mousePosition.x.ToString());
#endif

#if (PI)
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        transform.position = viewPos;
#endif
    }
}

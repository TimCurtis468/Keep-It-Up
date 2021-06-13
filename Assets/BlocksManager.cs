using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    #region Singleton
    private static BlocksManager _instance;
    public static BlocksManager Instance => _instance;

    public static event Action OnLevelLoaded;

    public Block blockPrefab;

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
    private SpriteRenderer sr;
    private BoxCollider2D boxCol;
    private Vector2 screenBounds;

    private float xOffset = 0.1f;
    private float yOffset = 0.1f;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
//        xOffset = screenBounds.x / 50.0f;
//        yOffset = screenBounds.y / 50.0f;
        this.GenerateBlocks();
    }

    private void GenerateBlocks()
    {
        float width = UnityEngine.Random.Range(screenBounds.x / 20.0f, screenBounds.x / 2.0f);
        float height= UnityEngine.Random.Range(screenBounds.y / 5.0f, screenBounds.y / 1.5f);
        float currentSpawnX = UnityEngine.Random.Range(-screenBounds.x + xOffset + (width * 2), screenBounds.x - (width * 2) - xOffset);
        float currentSpawnY = UnityEngine.Random.Range(-screenBounds.y / 5.0f, screenBounds.y - yOffset - (screenBounds.y / 2.5f));

        Debug.Log("screenBounds.y: " + screenBounds.y.ToString() + ", height: " + height.ToString() + ", currentSpawnY:" + currentSpawnY.ToString());



        Block newBlock = Instantiate(blockPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f), Quaternion.identity) as Block;
        newBlock.transform.localScale = new Vector3(width, height, 0.0f);

    }
}

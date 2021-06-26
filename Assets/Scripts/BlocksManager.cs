﻿using System;
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

    private uint level = 1;

    private const uint MAX_BLOCKS = 10;
    private const float MAX_WIDTH = 4.0f;    // equals 1/4 of the screen width
    private const float MAX_HEIGHT = 3.0f;   // equals 1/4 of the screen height
    private float width_factor;
    private float height_factor;

    public List<Block> blockList { get; set; }


    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        width_factor = screenBounds.x / 8.0f;
        height_factor = screenBounds.y / 4.0f;

        this.blockList = new List<Block>();

        this.NewLevel();
        level = 1;
    }

    public void GenerateBlocks()
    {
        //        float width = UnityEngine.Random.Range(screenBounds.x / 20.0f, screenBounds.x / 2.0f);
        //        float height= UnityEngine.Random.Range(screenBounds.y / 5.0f, screenBounds.y / 1.5f);
        float width = UnityEngine.Random.Range(0.5f, 2.0f);
        float height = UnityEngine.Random.Range(1.0f, 1.666f);

        width = width * width_factor;
        height = height * height_factor;

        float currentSpawnX = UnityEngine.Random.Range(-screenBounds.x + xOffset + (width * 2), screenBounds.x - (width * 2) - xOffset);
        float currentSpawnY = UnityEngine.Random.Range(-screenBounds.y / 5.0f, screenBounds.y - yOffset - (screenBounds.y / 2.5f));

//        Debug.Log("screenBounds.y: " + screenBounds.y.ToString() + ", height: " + height.ToString() + ", currentSpawnY:" + currentSpawnY.ToString());

        Block newBlock = Instantiate(blockPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f), Quaternion.identity) as Block;
        newBlock.transform.localScale = new Vector3(width, height, 0.0f);
        this.blockList.Add(newBlock);

    }

    public void NewLevel()
    {
//#if (PI)
        float screen_width = screenBounds.x;
        float slice_width = (screen_width * 2.0f) / 5.0f;
        float slice_height = screenBounds.y / 4.0f;

        slice_width = slice_width * width_factor;
        slice_height = slice_height * height_factor;

        int count;

        ClearBlocks();

        // Change background - make background manager
        // - have list of images (like buffs and debuffs)
        // - drag image to each item in list in Inspector


        for ( count = 0; count < 5; count++)
        {
            float present = UnityEngine.Random.value;
            if( present > 0.5f)
            {
                float width = UnityEngine.Random.Range(0.5f, 2.0f);
                float height = UnityEngine.Random.Range(1.0f, 3.666f);

                float currentSpawnX = -screen_width + width + (slice_width * count);//((slice_width * count) - screen_width) + UnityEngine.Random.Range(0.5f, 1.0f);
                float currentSpawnY = slice_height + UnityEngine.Random.Range(0.5f, 1.0f);

                Block newBlock = Instantiate(blockPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f), Quaternion.identity) as Block;
                newBlock.transform.localScale = new Vector3(width, height, 0.0f);

                this.blockList.Add(newBlock);
            }
        }
//#endif
#if (PI)
        float currentSpawnX;
        float currentSpawnY;
        uint count;
        uint numBlocks = ++level;
        float width;
        float height;

        /* Limit number of blocks to draw */
        if (numBlocks > MAX_BLOCKS)
        {
            numBlocks = MAX_BLOCKS;
        }

        ClearBlocks();

        Debug.Log("blockList.count: " + blockList.Count.ToString() + ", numBlock: " + numBlocks);

        for (count = 0; count < numBlocks; count++)
        {
            width = UnityEngine.Random.Range(1, 2);
            height = UnityEngine.Random.Range(3, 4);

            width = width * width_factor;
            height = height * height_factor;

            currentSpawnX = UnityEngine.Random.Range(-screenBounds.x + xOffset + (width * 2), screenBounds.x - (width * 2) - xOffset);
            currentSpawnY = UnityEngine.Random.Range(-screenBounds.y / 5.0f, screenBounds.y - yOffset - (screenBounds.y / 2.5f));

            Block newBlock = Instantiate(blockPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f), Quaternion.identity) as Block;
            newBlock.transform.localScale = new Vector3(width, height, 0.0f);

            this.blockList.Add(newBlock);
        }
#endif
    }

    private void ClearBlocks()
    {
        int idx;
        int count = blockList.Count;

        for (idx = count - 1; idx >= 0; idx--)
        {
            Block block = blockList[idx];
            block.RemoveBlock();
            blockList.RemoveAt(idx);
        }
    }
}

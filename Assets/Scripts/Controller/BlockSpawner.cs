using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockSpawner : MonoBehaviour
{        
    [SerializeField] private int column = 6;
    [SerializeField] private int row = 8;

    [SerializeField] private float objectZ = 10f;

    private float xDif, yDif;
    private Camera mainCam;
    public static event Action<Block[,]> OnBlocksCreated;


    private void Start()
    {
        mainCam = Camera.main;
        CreateSlots();
    }

    public void CreateSlots()
    {
        var _blocks = new Block[row, column];

        var screenHeight = mainCam.pixelHeight;
        var screenWidth = mainCam.pixelWidth;

        float targetHeightPixel = ((float)row * screenWidth) / (float)column;
        float targetHeight = (float)targetHeightPixel / screenHeight;

        xDif = (float)1f / column;
        yDif = (float)targetHeight / row;
        
        int slotIndex = 0;
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < column; x++)
            {
                //Spawn Process
                Transform block = ObjectPool.Instance.SpawnFromPool("Block", Vector3.zero, Quaternion.identity).transform;
                Block newBlock = block.GetComponent<Block>();
                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
                Sprite sprite = spriteRenderer.sprite;
                _blocks[y,x] = newBlock;

                //Position Process
                float xVP = ((float)xDif / 2) + x * xDif;
                float yVP = ((float)yDif / 2) + y * yDif;
                Vector3 pos = new Vector3(xVP, yVP, objectZ);
                Vector3 screenPoint = mainCam.ViewportToWorldPoint(pos);

                block.position = screenPoint;
                block.parent = transform;


                //Size Process
                var frustumHeight = 2.0f * objectZ * Mathf.Tan(mainCam.fieldOfView * 0.75f * Mathf.Deg2Rad);
                var frustumWidth = frustumHeight * mainCam.aspect;

                var ppu = sprite.pixelsPerUnit;
                var xRes = sprite.bounds.size.x;
                var yRes = sprite.bounds.size.y;

                float bolum = frustumWidth / (float)column;
                float bolen = xRes * block.localScale.x;
                block.rotation = mainCam.transform.rotation;
                block.localScale *= bolum / bolen;

                slotIndex++;
            }
        }
        OnBlocksCreated?.Invoke(_blocks);

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject prefab;
    Transform Blocks;

    Color red = new Color(1, 0, 0);
    Color blue = new Color(0, 0, 1);
    Color green = new Color(0, 1, 0);

    Block currentClick_Block;

    private void Awake()
    {
        Blocks = GameObject.Find("Blocks").GetComponent<Transform>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        GameObject obj;
        for(int i = 0; i < transforms.Length; ++i)
        {
            if (transforms[i].Equals(transform)) continue;
            obj = Instantiate(prefab);
            obj.transform.position = transforms[i].position;
            obj.transform.localScale = transforms[i].localScale;
            obj.transform.parent = Blocks;
            obj.name = "Block" + i.ToString();
            obj.layer = 8;
        }
    }

    private void Start()
    {
        StartCoroutine(SettingBlocks());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnStartClickBlock();
        }
        else if(Input.GetMouseButtonUp(0) && currentClick_Block)
        {
            OnEndClickBlock();
        }
    }

    void OnStartClickBlock()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(pos, pos, 100f, LayerMask.GetMask("Block"));
        if (hit2D)
        {
            currentClick_Block = hit2D.collider.gameObject.GetComponent<Block>();
            currentClick_Block.CheckBlock();
        }
    }

    void OnEndClickBlock()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = pos - (Vector2)currentClick_Block.gameObject.transform.position;

        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        //위
        if(angle > 90 - 30 && angle < 90 + 30)
        {
            Debug.Log(currentClick_Block.bl_Up.name);
        }
        //아래
        if(angle > -90 -30 && angle < -90 + 30)
        {
            Debug.Log(currentClick_Block.bl_Down.name);
        }
        //좌
        if(angle > 180 - 30 || angle < -180 +30)
        {
            Debug.Log(currentClick_Block.bl_Left.name);
        }
        //우
        if(angle < 30 && angle > -30)
        {
            Debug.Log(currentClick_Block.bl_Right.name);
        }

        currentClick_Block = null;
    }

    //블록 첫 세팅
    IEnumerator SettingBlocks()
    {
        yield return new WaitForEndOfFrame();
        Block[] blocks = GameObject.Find("Blocks").GetComponentsInChildren<Block>();
        for (int i = 0; i < blocks.Length; ++i)
        {
            blocks[i].blockColor = (BlockColor)Random.Range(0, 3);
            while (blocks[i].CheckConnectedBlocks(blocks[i].checkList) > 2)
            {
                blocks[i].blockColor = RandomColor(blocks[i].blockColor);
            }

            blocks[i].SetColor(blocks[i].blockColor);
        }
    }

    //랜덤컬러
    BlockColor RandomColor(BlockColor color)
    {
        BlockColor co = (BlockColor)Random.Range(0, 3);
        while (true)
        {
            if (co == color)
                co = (BlockColor)Random.Range(0, 3);
            else
                return co;
        }
    }

}

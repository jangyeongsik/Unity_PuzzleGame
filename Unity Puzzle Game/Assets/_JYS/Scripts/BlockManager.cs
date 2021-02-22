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
        }
    }

    private void Start()
    {
        StartCoroutine(SettingBlocks());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Block[] blocks = GameObject.Find("Blocks").GetComponentsInChildren<Block>();

            for (int i = 0; i < blocks.Length; ++i)
            {
                int c = blocks[i].CheckConnectedBlocks(blocks[i].checkList);
                if(c >)
            }
        }
    }

    IEnumerator SettingBlocks()
    {
        yield return new WaitForEndOfFrame();
        Block[] blocks = GameObject.Find("Blocks").GetComponentsInChildren<Block>();
        for (int i = 0; i < blocks.Length; ++i)
        {
            blocks[i].blockColor = (BlockColor)Random.Range(0, 3);

            blocks[i].SetColor(blocks[i].blockColor);
        }

        

    }

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

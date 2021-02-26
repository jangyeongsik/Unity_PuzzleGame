using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockColor
{
    Block_Red,
    Block_Green,
    Block_Blue,
    Block_None
}

public class Block : MonoBehaviour 
{
    public BlockColor blockColor = BlockColor.Block_None;

    SpriteRenderer spriteRenderer;

    BoxCollider2D boxCollider;

    float size;

    public Dictionary<string,Block> checkList = new Dictionary<string,Block>();

    public Block bl_Left;
    public Block bl_Right;
    public Block bl_Up;
    public Block bl_Down;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        size = boxCollider.size.x * 2;
    }

    //인접 검사
    public int CheckConnectedBlocks(Dictionary<string,Block> _checkList, int count = 0)
    {
        if (blockColor == BlockColor.Block_None) return 0;
        if(count == 0)
        {
            count++;
            _checkList.Clear();
            _checkList.Add(gameObject.name, null);
        }

        RaycastHit2D left = Physics2D.Raycast((Vector2)transform.position + Vector2.left * size, Vector2.left, size, LayerMask.GetMask("Block"));
        if(left)
        {
            if (!_checkList.ContainsKey(left.collider.name) && left.collider.GetComponent<Block>().blockColor == this.blockColor)
            {
                _checkList.Add(left.collider.name, GetComponent<Block>());
                count = left.collider.GetComponent<Block>().CheckConnectedBlocks(_checkList, ++count);
            }
        }

        RaycastHit2D right = Physics2D.Raycast((Vector2)transform.position + Vector2.right * size, Vector2.right, size, LayerMask.GetMask("Block"));
        if (right)
        {
            if (!_checkList.ContainsKey(right.collider.name) && right.collider.GetComponent<Block>().blockColor == this.blockColor)
            {
                _checkList.Add(right.collider.name, GetComponent<Block>());
                count = right.collider.GetComponent<Block>().CheckConnectedBlocks(_checkList, ++count);
            }
        }

        RaycastHit2D up = Physics2D.Raycast((Vector2)transform.position + Vector2.up * size, Vector2.up, size, LayerMask.GetMask("Block"));
        if (up)
        {
            if (!_checkList.ContainsKey(up.collider.name) && up.collider.GetComponent<Block>().blockColor == this.blockColor)
            {
                _checkList.Add(up.collider.name, GetComponent<Block>());
                count = up.collider.GetComponent<Block>().CheckConnectedBlocks(_checkList, ++count);
            }
        }

        RaycastHit2D down = Physics2D.Raycast((Vector2)transform.position + Vector2.down * size, Vector2.down, size, LayerMask.GetMask("Block"));
        if (down)
        {
            if (!_checkList.ContainsKey(down.collider.name) && down.collider.GetComponent<Block>().blockColor == this.blockColor)
            {
                _checkList.Add(down.collider.name, GetComponent<Block>());
                count = down.collider.GetComponent<Block>().CheckConnectedBlocks(_checkList, ++count);
            }
        }


        return count;
    }

    public void CheckBlock()
    {
        RaycastHit2D left = Physics2D.Raycast((Vector2)transform.position + Vector2.left * size, Vector2.left, size, LayerMask.GetMask("Block"));
        if (left)
            bl_Left = left.collider.GetComponent<Block>();
        RaycastHit2D right = Physics2D.Raycast((Vector2)transform.position + Vector2.right * size, Vector2.right, size, LayerMask.GetMask("Block"));
        if (right)
            bl_Right = right.collider.GetComponent<Block>();
        RaycastHit2D up = Physics2D.Raycast((Vector2)transform.position + Vector2.up * size, Vector2.up, size, LayerMask.GetMask("Block"));
        if (up)
            bl_Up = up.collider.GetComponent<Block>();
        RaycastHit2D down = Physics2D.Raycast((Vector2)transform.position + Vector2.down * size, Vector2.down, size, LayerMask.GetMask("Block"));
        if (down)
            bl_Down = down.collider.GetComponent<Block>();

    }

    public void CheckOutBlock()
    {
        bl_Left = null;
        bl_Right = null;
        bl_Up = null;
        bl_Down = null;
    }

    public void SetColor(BlockColor color)
    {
        switch (color)
        {
            case BlockColor.Block_Red:
                spriteRenderer.color = Color.red;
                break;
            case BlockColor.Block_Green:
                spriteRenderer.color = Color.green;
                break;
            case BlockColor.Block_Blue:
                spriteRenderer.color = Color.blue;
                break;
        }
    }
}


using UnityEngine;
using System.Collections.Generic;
[ExecuteInEditMode]
public class UIGrid : MonoBehaviour
{

    public float cellWidth = 2;
    public float cellHeight = 2;
    public int columnCount = 2;
    public Vector2 normalizedOffset;
    void OnEnable()
    {
        
        SetPosition();
    }

 


    public void SetPosition()
    {
        
        
        int childCount = transform.childCount;

        int rowCount = childCount / columnCount;
        if (childCount % columnCount != 0)
        {
            rowCount += 1;
        }
        

        float width = (columnCount - 1) * cellWidth;
        float height = (rowCount -1 )* cellHeight;


        Vector3 topLeft = new Vector3(width * normalizedOffset.x, height * normalizedOffset.y, 0);
        

        for(int i = 0; i < rowCount; i++)
        {
            for(int j = 0; j < columnCount; j++)
            {
                int childIndex = i * columnCount + j;
                if (childIndex >= childCount)
                {
                    return;
                }
                Transform t = transform.GetChild(childIndex);
                t.localPosition = topLeft + new Vector3(j * cellWidth, -i * cellHeight, 0);

            }
        }

        
        
     
    }

#if UNITY_EDITOR
    //public bool rearrange = false;
    void Update()
    {
        //if (rearrange)
        {
            //rearrange = false;
            SetPosition();
        }
    }
#endif
}
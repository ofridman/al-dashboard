#define UI_EXTENSIONS //https://bitbucket.org/UnityUIExtensions/unity-ui-extensions

using UnityEngine;
using UnityEngine.UI.Extensions;


[ExecuteInEditMode(), RequireComponent(typeof(TableLayoutGroup)), RequireComponent(typeof(RectTransform))]
public class CalenderLayoutController : MonoBehaviour
{
#if UI_EXTENSIONS

    [SerializeField]
    TableLayoutGroup tlg;

    [SerializeField]
    RectTransform rt;


    Rect prevRect;
    int prevChildCount = 0;
    

    private void Update()
    {
        if (rt.rect.height != prevRect.height || rt.rect.width != prevRect.width)
            ReCalculate();

        if(rt.childCount != prevChildCount)
        {
            prevChildCount = rt.childCount;
            ReCalculate();
        }
    }

    [ContextMenu("Update")]
    void ReCalculate()
    {
        tlg.ColumnWidths = new float[7];
        for(int i = 0; i < 7; i++)
        {
            float hPadding = tlg.padding.left + tlg.padding.right;
            float hSpacing = tlg.ColumnSpacing * (7-1);

            float deadSpace = hPadding + hSpacing;

            float cellWidth = (rt.rect.width - deadSpace) / 7f;
            tlg.ColumnWidths[i] = cellWidth;
        }

        
        float vPadding = tlg.padding.top + tlg.padding.bottom;
        float vSpacing = tlg.RowSpacing * 5;

        float vDeadSpace = vPadding + vSpacing;

        tlg.MinimumRowHeight = (rt.rect.height-vDeadSpace) / 6;

        prevRect.width = rt.rect.width;
        prevRect.height = rt.rect.height;
    }

#endif
}

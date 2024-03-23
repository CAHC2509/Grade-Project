using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField] private DragDropManager dragDropManager;

    [ContextMenu("Review")]
    public void ReviewQuest()
    {
        if (AllObjectsPlacedCorrectly())
        {
            List<ObjectSettings> objects = dragDropManager.AllObjects;

            foreach (ObjectSettings objectSettings in objects)
                objectSettings.correctSprite.SetActive(true);
        }
    }

    private bool AllObjectsPlacedCorrectly()
    {
        List<PanelSettings> panels = dragDropManager.AllPanels;
        foreach (PanelSettings panel in panels)
        {
            if (panel.spectedObjectId != panel.placedObjectId)
                return false;
        }

        return true;
    }

    public void ResetQuest() => DragDropManager.ResetPositions(dragDropManager);
}

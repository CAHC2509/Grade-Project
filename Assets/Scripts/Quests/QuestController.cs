using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestController : MonoBehaviour
{
    [SerializeField] private DragDropManager dragDropManager;
    [SerializeField] private UnityEvent onQuestCompleted;

    [ContextMenu("Review")]
    public void ReviewQuest()
    {
        if (AllObjectsPlacedCorrectly())
        {
            List<ObjectSettings> objects = dragDropManager.AllObjects;

            foreach (ObjectSettings objectSettings in objects)
            {
                objectSettings.correctSprite.SetActive(true);
                objectSettings.LockObject = true;

                onQuestCompleted?.Invoke();
            }
        }
    }

    private bool AllObjectsPlacedCorrectly()
    {
        List<PanelSettings> panels = dragDropManager.CalificablePanels;

        foreach (PanelSettings panel in panels)
        {
            bool objectPlacedCorrectly = panel.placedObjectId.Contains(panel.spectedObjectId);

            if (!objectPlacedCorrectly)
                return false;
        }

        return true;
    }

    public void ResetQuest() => DragDropManager.ResetPositions(dragDropManager);
}

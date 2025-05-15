using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace TaskSystem
{
    public class TaskAvailabilityDisplay : MonoBehaviour
    {
        private TaskComponent taskComponent;
        private CanvasGroup canvasGroup;

        private bool isVisible;
    
        private void Awake()
        {
            taskComponent = GetComponentInParent<TaskComponent>();
            canvasGroup = GetComponent<CanvasGroup>();
            Assert.IsNotNull(taskComponent, "TaskComponent is not found in parent of TaskAvailabilityDisplay");
            Hide();
        }
    
        private void Update()
        {
            if (taskComponent.isTaskAvailable)
            {
                if (!isVisible)
                {
                    Show();
                }
            }
            else
            {
                if (isVisible)
                {
                    Hide();
                }
            }
        }
    
        private void Show()
        {
            canvasGroup.DOFade(1, 0.5f);
            isVisible = true;
        }

        private void Hide()
        {
            canvasGroup.DOFade(0, 0.5f);
            isVisible = false;
        }
    }
}

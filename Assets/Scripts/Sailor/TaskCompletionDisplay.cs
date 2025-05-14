using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sailor
{
    public class TaskCompletionDisplay : MonoBehaviour
    {
        [SerializeField] private SailorController sailorController;
        [SerializeField] private Image taskProgressBar;
    
        private bool isVisible;
    
        private void Awake()
        {
            taskProgressBar.fillAmount = 0;
        }

        private void Update()
        {
            if (!sailorController.currentTask || !sailorController.currentTask.isTaskInProgress)
            {
                if (isVisible)
                {
                    Hide();
                }
                return;
            }
        
            if (!isVisible)
            {
                Show();
            }
        
            float _taskProgress = sailorController.currentTask.taskTimer / sailorController.currentTask.taskObject.taskDuration;
            taskProgressBar.fillAmount = _taskProgress;
        }

        private void Show()
        {
            taskProgressBar.DOFade(1, 0.5f);
            isVisible = true;
        }

        private void Hide()
        {
            taskProgressBar.DOFade(0, 0.5f);
            isVisible = false;
        }
    }
}

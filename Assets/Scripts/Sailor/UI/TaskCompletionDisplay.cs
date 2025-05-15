using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sailor.UI
{
    public class TaskCompletionDisplay : MonoBehaviour
    {
        [SerializeField] private SailorAI sailorAI;
        [SerializeField] private Image taskProgressBar;
    
        private bool isVisible;
    
        private void Awake()
        {
            Hide();
        }

        private void Update()
        {
            if (!sailorAI.currentTask || !sailorAI.currentTask.isTaskInProgress)
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
        
            float _taskProgress = sailorAI.currentTask.taskTimer / sailorAI.currentTask.taskObject.taskDuration;
            taskProgressBar.fillAmount = _taskProgress;
        }

        private void Show()
        {
            taskProgressBar.transform.DOScale(Vector3.one, 0.5f);
            taskProgressBar.DOFade(1, 0.5f);
            isVisible = true;
        }

        private void Hide()
        {
            taskProgressBar.transform.DOScale(Vector3.zero, 0.5f);
            taskProgressBar.DOFade(0, 0.5f);
            isVisible = false;
        }
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sailor.UI
{
    public class StateUIDisplayer : MonoBehaviour
    {
        [SerializeField] private SailorAI sailorAI;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private SailorStates targetState;
        
        [SerializeField] private float transitionDuration = 0.5f;
        
        private void Awake()
        {
            CheckState(sailorAI.currentState);
            sailorAI.onStateChanged += OnStateChanged;
        }

        private void OnStateChanged(SailorStates _oldState, SailorStates _newState)
        {
            CheckState(_newState);
        }
        
        /// <summary>
        /// check if the state is the same as the target state and show or hide the UI accordingly
        /// </summary>
        private void CheckState(SailorStates _state)
        {
            if (_state == targetState)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Hide()
        {
            canvasGroup.DOFade(0, transitionDuration);
            canvasGroup.transform.DOScale(Vector3.zero, transitionDuration);
        }
        
        private void Show()
        {
            canvasGroup.DOFade(1, transitionDuration);
            canvasGroup.transform.DOScale(Vector3.one, transitionDuration);
        }
    }
}
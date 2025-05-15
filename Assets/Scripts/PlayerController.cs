
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public UnityEvent<Vector2> onSelect = new();
    public UnityEvent<Vector2> onOrder = new();
    
    public void OnSelect(UnityEngine.InputSystem.InputAction.CallbackContext _context)
    {
        if (!_context.started)
        {
            return;
        }
        
        Vector2 _mousePosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        onSelect?.Invoke(_mousePosition);
    }
    
    public void OnOrder(UnityEngine.InputSystem.InputAction.CallbackContext _context)
    {
        if (!_context.started)
        {
            return;
        }
        
        Vector2 _mousePosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        onOrder?.Invoke(_mousePosition);
    }
}
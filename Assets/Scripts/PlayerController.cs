using UnityEngine;
using UnityEngine.InputSystem;
using ezutils.Core;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3Variable _position;
    [SerializeField] private BoolVariable _isTouching;
    private bool _isHolding;
    private Vector3 _startScreenPosition;
    private Vector3 _currentScreenPosition;
    private Vector3 _startWorldPosition;
    private Vector3 _currentWorldPosition;

    private readonly string _startPosActionName = "StartPosition";
    private readonly string _currentPosActionName = "CurrentPosition";
    private readonly string _isTouchingActionName = "IsTouching";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //subscribe methods to the relevant input actions
        _playerInput.actions[_startPosActionName].performed += StartPosition;
        _playerInput.actions[_currentPosActionName].performed += CurrentPosition;
        _playerInput.actions[_isTouchingActionName].performed += StartTouch;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHolding)
        {
            _currentWorldPosition = _camera.ScreenToWorldPoint(
                new Vector3(
                    _currentScreenPosition.x,
                    _currentScreenPosition.y,
                    _camera.nearClipPlane));
            _startWorldPosition = _camera.ScreenToWorldPoint(
                new Vector3(
                    _startScreenPosition.x,
                    _startScreenPosition.y,
                    _camera.nearClipPlane));
            _startWorldPosition.z = 0f;
            _currentWorldPosition.z = 0f;

            _position.Value = _currentWorldPosition;
        }

    }

    private void StartTouch(InputAction.CallbackContext context)
    {

        var value = context.ReadValue<float>();
        Debug.Log($"start touch = {value}");
        _startWorldPosition = _camera.ScreenToWorldPoint(
            new Vector3(
                _startScreenPosition.x,
                _startScreenPosition.y,
                _camera.nearClipPlane));
        if (value > 0)
        {
            //Ray ray = Camera.main.ScreenPointToRay(_startPosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 100, _layerMask))
            //{
            //    Debug.DrawLine(ray.origin, hit.point, Color.cyan);
            StartDrag();
            /*            }
                        else
                        {
                            //nudge the scale
                            _scaleSpring.Velocity += Vector3.one * _scaleAmount;
                        }*/
        }
        else
        {
            if (_isHolding)
            {
                EndDrag();
            }
        }
    }

    private void StartPosition(InputAction.CallbackContext context)
    {
        _startScreenPosition = context.ReadValue<Vector2>();
        Debug.Log("startPos");
    }
    private void CurrentPosition(InputAction.CallbackContext context)
    {
        _currentScreenPosition = context.ReadValue<Vector2>();
        Debug.Log($"currentPos {_currentScreenPosition}");
    }

    private void StartDrag()
    {

        _isHolding = true;
        _isTouching.Value = true;
        Debug.Log("holding");
    }
    private void EndDrag()
    {
        _isHolding = false;
        _isTouching.Value = false;
        Debug.Log("release");
    }
}

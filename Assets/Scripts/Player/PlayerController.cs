using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _charMS = 5f;                // 캐릭터 이동 속도

    private Rigidbody2D _rb;                  // Rigidbody2D 컴포넌트
    private InputAction _inputDir;
    private PlayerInput inputPlayer;
    public bool isAction = false;             // 액션(스킬 등) 중 여부

    private Vector2 moveDirection;
    Vector2 moveDirec;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        inputPlayer = GetComponent<PlayerInput>();
    }

    void Start()
    {
        _inputDir = InputSystem.actions.FindAction("Move");
        Vector2 moveValue = _inputDir.ReadValue<Vector2>();
        






        WeaponManager weaponManager = GetComponentInChildren<WeaponManager>();
        
        Debug.Log(weaponManager);
        if (weaponManager != null && weaponManager.CanAddWeapon())
        {
            GameObject defaultWeapon = weaponManager.availableWeaponPrefabs[0];
            if (defaultWeapon != null)
            {
                weaponManager.AddWeapon(defaultWeapon);
                Debug.Log($"{defaultWeapon.name} 기본 무기 장착 완료!");
            }
        }
    }

    void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime);
        moveDirec = new Vector2(moveDirection.x, moveDirection.y);

    }

    private void OnEnable()
    {
        inputPlayer.onActionTriggered += OnActionTrigered;
    }

    private void OnDisable()
    {
        inputPlayer.onActionTriggered += OnActionTrigered;
    }

    void OnActionTrigered(InputAction.CallbackContext context)
    {
        if(context.action.name == "Move")
        {
            moveDirec = context.ReadValue<Vector2>();
        }
    }

    //void OnMovePerformed(InputAction.CallbackContext context)
    //{
    //    moveDirection = context.ReadValue<Vector2>();
       
    //}
    //void OnMoveCanceled(InputAction.CallbackContext context)
    //{
    //    moveDirection = Vector2.zero;
    //}
    //private void OnDestroy()
    //{
    //    _inputDir.performed -= OnMovePerformed;
    //    _inputDir.canceled -= OnMoveCanceled;
    //}
}
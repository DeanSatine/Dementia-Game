using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static Controls _controls;//Game controls

    public static void Init(Player player)
    {

        _controls = new Controls();//Setup controls

        _controls.Game.Enable();//Enable game controls

        Cursor.lockState = CursorLockMode.Locked;//Lock cursor position
        Cursor.visible = false;//Hide cursor

        _controls.Game.Move.performed += ctx =>//Move player
        {
            player.Move(ctx.ReadValue<Vector3>());
        };

        _controls.Game.Look.performed += ctx =>//Rotate player
        {
            player.Look(ctx.ReadValue<Vector2>());
        };

        _controls.Game.Action.performed += ctx =>//Attemp object interaction
        {
            player.Grab();
        };

    }

}

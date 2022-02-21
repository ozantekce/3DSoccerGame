using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanKeyboardInputter : Inputter
{
    [SerializeField]
    private KeyCode buttonLeftKeyboard = KeyCode.A, buttonRightKeyboard = KeyCode.D
        , buttonUpKeyboard = KeyCode.W, buttonDownKeyboard = KeyCode.S;

    [SerializeField]
    private KeyCode buttonShootKeyboard = KeyCode.J;
    [SerializeField]
    private KeyCode buttonPassKeyboard = KeyCode.K;
    [SerializeField]
    private KeyCode buttonSlideKeyboard = KeyCode.L;
    [SerializeField]
    private KeyCode buttonRunKeyboard = KeyCode.V;
    [SerializeField]
    private KeyCode buttonJumpKeyboard = KeyCode.Space;

    protected override bool LeftButtonPressed()
    {
        return Input.GetKey(buttonLeftKeyboard);
    }

    protected override bool RightButtonPressed()
    {
        return Input.GetKey(buttonRightKeyboard);
    }

    protected override bool UpButtonPressed()
    {
        return Input.GetKey(buttonUpKeyboard);
    }

    protected override bool DownButtonPressed()
    {
        return Input.GetKey(buttonDownKeyboard);
    }

    protected override bool ShootButtonPressed()
    {
        return Input.GetKey(buttonShootKeyboard);
    }

    protected override bool PassButtonPressed()
    {
        return Input.GetKey(buttonPassKeyboard);
    }

    protected override bool SlideButtonPressed()
    {
        return Input.GetKey(buttonSlideKeyboard);
    }

    protected override bool RunButtonPressed()
    {
        return Input.GetKey(buttonRunKeyboard);
    }

    protected override bool JumpButtonPressed()
    {
        return Input.GetKey(buttonJumpKeyboard);
    }
}

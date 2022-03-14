using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inputter : MonoBehaviour
{
    [SerializeField]
    protected float sensivity = 0.01f;

    protected float Horizontal;
    protected float Vertical;

    protected float buttonShootValue;

    protected float buttonPassValue;

    protected float buttonSlideValue;

    protected float buttonRunValue;
    
    protected float buttonJumpValue;

    protected bool thereAreNoInputs;

    public bool ThereAreNoInputs { get => thereAreNoInputs; set => thereAreNoInputs = value; }

    protected void Update()
    {
        Reader();
    }

    private void Reader()
    {

        Vertical
            = ReadJoystickInputs(Vertical, DownButtonPressed(), UpButtonPressed(), sensivity);

        Horizontal
            = ReadJoystickInputs(Horizontal, LeftButtonPressed(), RightButtonPressed(), sensivity);

        buttonShootValue = ReadOtherInputs(buttonShootValue, ShootButtonPressed(), sensivity);
        buttonPassValue = ReadOtherInputs(buttonPassValue, PassButtonPressed(), sensivity);
        buttonSlideValue = ReadOtherInputs(buttonSlideValue, SlideButtonPressed(), sensivity);
        buttonJumpValue = ReadOtherInputs(buttonJumpValue, JumpButtonPressed(), sensivity);
        buttonRunValue = ReadOtherInputs(buttonRunValue, RunButtonPressed(), sensivity);

        thereAreNoInputs = Vertical ==0 && Horizontal ==0 
            && buttonShootValue==0 && buttonPassValue==0 &&
            buttonSlideValue==0 && buttonJumpValue==0;

    }



    public float GetJoyStickHorizontalValue()
    {
        return Horizontal;
    }


    public float GetJoyStickHorizontalValueRaw()
    {
        if (Horizontal > 0)
            return 1;
        else if (Horizontal < 0)
            return -1;
        else
            return 0;
    }


    public float GetJoyStickVerticalValue()
    {
        return Vertical;
    }

    public float GetJoyStickVerticalValueRaw()
    {
        if (Vertical > 0)
            return 1;
        else if (Vertical < 0)
            return -1;
        else
            return 0;
    }

    public float GetButtonShootValue()
    {

        if (ShootButtonPressed())
        {
            return 0;
        }

        return buttonShootValue;

    }

    public float GetButtonPassValue()
    {

        if (PassButtonPressed())
        {
            return 0;
        }
        return buttonPassValue;
    }

    public float GetButtonSlideValue()
    {
        if (SlideButtonPressed())
        {
            return 0;
        }
        return buttonSlideValue;
    }

    public float GetButtonRunValue()
    {
        if (RunButtonPressed())
        {
            return 0;
        }
        return buttonRunValue;
    }

    public float GetButtonJumpValue()
    {
        if (JumpButtonPressed())
        {
            return 0;
        }
        return buttonJumpValue;
    }




    protected abstract bool LeftButtonPressed();
    protected abstract bool RightButtonPressed();
    protected abstract bool UpButtonPressed();
    protected abstract bool DownButtonPressed();
    protected abstract bool ShootButtonPressed();

    protected abstract bool PassButtonPressed();
    protected abstract bool SlideButtonPressed();
    protected abstract bool RunButtonPressed();

    protected abstract bool JumpButtonPressed();

    private float ReadJoystickInputs(float currentValue, bool keyMinus, bool keyPlus, float sensivity)
    {

        if (keyMinus)
        {
            currentValue -= sensivity;
        }
        else if (currentValue < 0)
        {
            currentValue += sensivity;
            if (currentValue >= -sensivity && currentValue <= sensivity)
            {
                currentValue = 0;
            }
        }


        if (keyPlus)
        {
            currentValue += sensivity;
        }
        else if (currentValue > 0)
        {
            currentValue -= sensivity;
            if (currentValue >= -sensivity && currentValue <= sensivity)
            {
                currentValue = 0;
            }
        }

        currentValue = Mathf.Clamp(currentValue, -1f, 1f);


        return currentValue;

    }

    private float ReadOtherInputs(float currentValue, bool keyPressed, float sensivity)
    {

        if (keyPressed)
        {
            currentValue += sensivity;
        }
        else
        {
            currentValue -= sensivity;
        }


        currentValue = Mathf.Clamp(currentValue, 0, 1f);

        return currentValue;

    }


}

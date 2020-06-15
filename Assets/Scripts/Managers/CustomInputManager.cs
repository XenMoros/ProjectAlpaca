using UnityEngine;
using System.Collections;

public class CustomInputManager : MonoBehaviour
{
    KeyCode yell;// LeftShift
    KeyCode jump;// Space
    KeyCode coz;// Mouse1
    KeyCode spit;// Mouse0
    KeyCode interact;// E
    KeyCode start;// T
    KeyCode ret;// Escape

    KeyCode forward;// W
    KeyCode backward;// S
    KeyCode left;// A
    KeyCode right;// D
    public KeyCode walk;// LeftControl

    private void Awake()
    {
        yell = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("yellKey", "LeftShift"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        coz = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("cozKey", "Mouse1"));
        spit = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("spitKey", "Mouse0"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactKey", "E"));
        
        start = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("startKey", "T"));
        ret = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("retKey", "Escape"));

        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        walk = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("controlKey", "LeftControl"));
    }

    public float GetAxis(string axis)
    {
        float valor = 0f;
        switch (axis)
        {
            case "MovementHorizontal":
                valor = Input.GetAxis("LS_h");
                if (Input.GetKey(right)) valor += 1f;
                if (Input.GetKey(left)) valor -= 1f;
                break;
            case "MovementVertical":
                valor = Input.GetAxis("LS_v");
                if (Input.GetKey(forward)) valor += 1f;
                if (Input.GetKey(backward)) valor -= 1f;
                break;
            /*case "CamaraHorizontal":
                valor = Input.GetAxis("RS_h");
                valor += Input.GetAxis("Mouse X");
                break;
            case "CamaraVertical":
                valor = Input.GetAxis("RS_v");
                valor += Input.GetAxis("Mouse Y");
                break;*/
            case "Escupir":
                valor = Input.GetAxis("RT");
                if (Input.GetKey(spit)) valor += 1f;
                break;
            default:
                valor = 0;
                break;
        }

        valor = Mathf.Clamp(valor, -1f, 1f);
        return valor;
    }

    public bool GetButton(string accion)
    {
        switch(accion)
        {
            case "Jump":
                if (Input.GetButton("A") || Input.GetKey(jump))
                    return true;
                break;
            case "Interact":
                if (Input.GetButton("X") || Input.GetKey(interact))
                    return true;
                break;
            case "Yell":
                if (Input.GetButton("Y") || Input.GetKey(yell))
                    return true;
                break;
            case "Coz":
                if (Input.GetButton("B") || Input.GetKey(coz))
                    return true;
                break;
            case "Start":
                if (Input.GetButton("Start") || Input.GetKey(start))
                    return true;
                break;
            case "Return":
                if (Input.GetButton("B") || Input.GetKey(ret))
                    return true;
                break;
            default:
                break;
        }

        return false;
    }

    public bool GetButtonDown(string accion)
    {
        switch (accion)
        {
            case "Jump":
                if (Input.GetButtonDown("A") || Input.GetKeyDown(jump))
                    return true;
                break;
            case "Interact":
                if (Input.GetButtonDown("X") || Input.GetKeyDown(interact))
                    return true;
                break;
            case "Yell":
                if (Input.GetButtonDown("Y") || Input.GetKeyDown(yell))
                    return true;
                break;
            case "Coz":
                if (Input.GetButtonDown("B") || Input.GetKeyDown(coz))
                    return true;
                break;
            case "Start":
                if (Input.GetButtonDown("Start") || Input.GetKeyDown(start))
                    return true;
                break;
            case "Return":
                if (Input.GetButtonDown("B") || Input.GetKeyDown(ret))
                    return true;
                break;
            default:
                break;
        }

        return false;
    }

    public bool GetButtonUp(string accion)
    {
        switch (accion)
        {
            case "Jump":
                if (Input.GetButtonUp("A") || Input.GetKeyUp(jump))
                    return true;
                break;
            case "Interact":
                if (Input.GetButtonUp("X") || Input.GetKeyUp(interact))
                    return true;
                break;
            case "Yell":
                if (Input.GetButtonUp("Y") || Input.GetKeyUp(yell))
                    return true;
                break;
            case "Coz":
                if (Input.GetButtonUp("B") || Input.GetKeyUp(coz))
                    return true;
                break;
            case "Start":
                if (Input.GetButtonUp("Start") || Input.GetKeyUp(start))
                    return true;
                break;
            case "Return":
                if (Input.GetButtonUp("B") || Input.GetKeyUp(ret))
                    return true;
                break;
            default:
                break;
        }

        return false;
    }
}

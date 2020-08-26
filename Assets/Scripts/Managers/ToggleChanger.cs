using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ToggleChanger : MonoBehaviour
    {

        public Toggle toggle;
        public enum ToggleType { AxisV, AxisH };
        public ToggleType toggleType;


        private void Awake()
        {
            switch (toggleType)
            {
                case ToggleType.AxisH:
                    toggle.isOn = StaticManager.axisH;
                    break;
                case ToggleType.AxisV:
                    toggle.isOn = StaticManager.axisV;
                    break;
                default:
                    break;
            }
        }

        public void OnToggleChange()
        {
            switch (toggleType)
            {
                case ToggleType.AxisH:
                    StaticManager.ChangeAxisH();
                    toggle.isOn = StaticManager.axisH;
                    break;
                case ToggleType.AxisV:
                    StaticManager.ChangeAxisV();
                    toggle.isOn = StaticManager.axisV;
                    break;
                default:
                    break;
            }
        }
    }
}
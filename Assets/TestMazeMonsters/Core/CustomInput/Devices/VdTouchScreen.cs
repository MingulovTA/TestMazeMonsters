using System.Collections.Generic;
using TestMazeMonsters.Core.CustomInput.Enums;

namespace TestMazeMonsters.Core.CustomInput.Devices
{
    public class VdTouchScreen:IVirtualDevice
    {
        private bool _isActive;
        
        public VirtualDeviceId VirtualDeviceId => VirtualDeviceId.TouchScreen;
        public Dictionary<InputCmdId, float> InputActions { get; }
        public bool IsAcitve => false;
        
        public void Enable()
        {
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void Tick()
        {
            
        }
    }
}

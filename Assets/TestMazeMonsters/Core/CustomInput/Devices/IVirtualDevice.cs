using System.Collections.Generic;
using TestMazeMonsters.Core.CustomInput.Enums;

namespace TestMazeMonsters.Core.CustomInput.Devices
{
    public interface IVirtualDevice
    {
        VirtualDeviceId VirtualDeviceId { get; }

        Dictionary<InputCmdId, float> InputActions { get; }
        
        bool IsAcitve { get; }

        void Enable();
        
        void Disable();

        void Tick();
    }
}

/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using System.Collections.Generic;

namespace LabW07_DeviceAllocation.Domain
{
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; }

        public ProjectorRemote(string id, string name, string storageLocation, int batteryLevel)
            : base(id, name, storageLocation)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
                throw new ArgumentOutOfRangeException(nameof(batteryLevel), "Mức pin phải từ 0 đến 100.");
            BatteryLevel = batteryLevel;
        }

        public override bool IsReadyForUse() => Status == DeviceStatus.Available && BatteryLevel >= 20;
        public override double GetDefaultPriorityScore() => BatteryLevel;
        public override string ToString() => $"{base.ToString()} | Pin: {BatteryLevel}%";
    }

    public class AirConditionerRemote : TeachingDevice
    {
        private readonly HashSet<string> compatibleRooms;
        public IReadOnlySet<string> CompatibleRooms => compatibleRooms;

        public AirConditionerRemote(string id, string name, string storageLocation, IEnumerable<string> compatibleRooms)
            : base(id, name, storageLocation)
        {
            if (compatibleRooms == null)
                throw new ArgumentNullException(nameof(compatibleRooms));
            this.compatibleRooms = new HashSet<string>(compatibleRooms, StringComparer.OrdinalIgnoreCase);
        }

        public bool IsCompatibleWith(string roomId) =>
            !string.IsNullOrWhiteSpace(roomId) && compatibleRooms.Contains(roomId.Trim());

        public override bool IsReadyForUse() => Status == DeviceStatus.Available;
        public override double GetDefaultPriorityScore() => compatibleRooms.Count > 0 ? 100.0 : 0.0;
        public override string ToString() => $"{base.ToString()} | Phòng tương thích: [{string.Join(", ", compatibleRooms)}]";
    }

    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; }
        public int SignalQuality { get; }

        public WirelessMicrophone(string id, string name, string storageLocation, int batteryLevel, int signalQuality)
            : base(id, name, storageLocation)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
                throw new ArgumentOutOfRangeException(nameof(batteryLevel), "Mức pin phải từ 0 đến 100.");
            if (signalQuality < 0 || signalQuality > 100)
                throw new ArgumentOutOfRangeException(nameof(signalQuality), "Chất lượng tín hiệu phải từ 0 đến 100.");
            BatteryLevel = batteryLevel;
            SignalQuality = signalQuality;
        }

        public override bool IsReadyForUse() =>
            Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40;

        public override double GetDefaultPriorityScore() => (BatteryLevel + SignalQuality) / 2.0;
        public override string ToString() => $"{base.ToString()} | Pin: {BatteryLevel}%, Tín hiệu: {SignalQuality}%";
    }
}

/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using LabW07_DeviceAllocation.Exceptions;

namespace LabW07_DeviceAllocation.Domain
{
    /// <summary>
    /// Lớp cơ sở trừu tượng biểu diễn thiết bị phục vụ giảng dạy trong trường đại học.
    /// </summary>
    public abstract class TeachingDevice : IIdentifiable
    {
        public string Id { get; }
        public string Name { get; }
        public string StorageLocation { get; }
        public DeviceStatus Status { get; protected set; }

        protected TeachingDevice(string id, string name, string storageLocation)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Mã thiết bị (Id) không được để trống.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên thiết bị (Name) không được để trống.", nameof(name));
            if (string.IsNullOrWhiteSpace(storageLocation))
                throw new ArgumentException("Vị trí lưu trữ (StorageLocation) không được để trống.", nameof(storageLocation));

            Id = id.Trim();
            Name = name.Trim();
            StorageLocation = storageLocation.Trim();
            Status = DeviceStatus.Available;
        }

        public abstract bool IsReadyForUse();
        public abstract double GetDefaultPriorityScore();

        public void Borrow()
        {
            if (!IsReadyForUse())
                throw new DeviceNotReadyException(Id, Status);

            Status = DeviceStatus.Borrowed;
        }

        public void Return()
        {
            if (Status != DeviceStatus.Borrowed)
                throw new InvalidOperationException($"Không thể trả thiết bị '{Id}' vì không ở trạng thái Borrowed.");

            Status = DeviceStatus.Available;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} | Kho: {StorageLocation} | Trạng thái: {Status} | Sẵn sàng: {IsReadyForUse()} | Điểm ưu tiên: {GetDefaultPriorityScore():0.##}";
        }
    }
}

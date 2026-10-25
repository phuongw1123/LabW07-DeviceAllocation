/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using LabW07_DeviceAllocation.Domain;

namespace LabW07_DeviceAllocation.Exceptions
{
    public class DuplicateIdException : Exception
    {
        public string DuplicateId { get; }

        public DuplicateIdException(string duplicateId)
            : base($"Lỗi: Đã tồn tại phần tử có mã định danh '{duplicateId}' trong kho lưu trữ.")
        {
            DuplicateId = duplicateId;
        }
    }

    public class NoSuitableItemException : Exception
    {
        public NoSuitableItemException(string message = "Không tìm thấy thiết bị nào phù hợp với điều kiện yêu cầu.")
            : base(message)
        {
        }
    }

    public class DeviceNotReadyException : Exception
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceNotReadyException(string deviceId, DeviceStatus currentStatus, string? reason = null)
            : base($"Thiết bị '{deviceId}' (Trạng thái: {currentStatus}) chưa sẵn sàng để cấp phát. {reason}".Trim())
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }
}

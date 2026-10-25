/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

namespace LabW07_DeviceAllocation.Domain
{
    /// <summary>
    /// Biểu diễn trạng thái hoạt động hiện tại của thiết bị giảng dạy.
    /// </summary>
    public enum DeviceStatus
    {
        Available,
        Borrowed,
        UnderMaintenance
    }

    /// <summary>
    /// Interface cung cấp thuộc tính nhận diện duy nhất cho đối tượng.
    /// </summary>
    public interface IIdentifiable
    {
        string Id { get; }
    }
}

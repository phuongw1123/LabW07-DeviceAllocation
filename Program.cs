/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using System.Text;
using LabW07_DeviceAllocation.Domain;
using LabW07_DeviceAllocation.Exceptions;
using LabW07_DeviceAllocation.Repositories;
using LabW07_DeviceAllocation.Services;
using LabW07_DeviceAllocation.Strategies;

namespace LabW07_DeviceAllocation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IRepository<ProjectorRemote> projectorRepo = new InMemoryRepository<ProjectorRemote>();
            IRepository<AirConditionerRemote> acRepo = new InMemoryRepository<AirConditionerRemote>();
            IRepository<WirelessMicrophone> micRepo = new InMemoryRepository<WirelessMicrophone>();

            projectorRepo.Add(new ProjectorRemote("PR-01", "Remote Máy chiếu Sony", "Tủ A1", 65));
            projectorRepo.Add(new ProjectorRemote("PR-02", "Remote Máy chiếu Epson", "Tủ A1", 92));
            projectorRepo.Add(new ProjectorRemote("PR-03", "Remote Máy chiếu Panasonic", "Tủ A2", 15));

            acRepo.Add(new AirConditionerRemote("AC-01", "Remote Điều hòa Daikin Tầng 1", "Tủ B1", new[] { "A101", "A102", "A103" }));
            acRepo.Add(new AirConditionerRemote("AC-02", "Remote Điều hòa Daikin Tầng 2", "Tủ B2", new[] { "A201", "A202", "A203" }));

            micRepo.Add(new WirelessMicrophone("MC-01", "Micro Shure B1", "Tủ C1", 25, 80));
            micRepo.Add(new WirelessMicrophone("MC-02", "Micro Sennheiser G4", "Tủ C1", 70, 80));
            micRepo.Add(new WirelessMicrophone("MC-03", "Micro Bose Pro", "Tủ C2", 90, 95));

            var prService = new AllocationService<ProjectorRemote>(
                projectorRepo,
                new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore()));
            var allocatedPr = prService.Allocate();
            Console.WriteLine($"Đã cấp phát máy chiếu: {allocatedPr}");

            var acService = new AllocationService<AirConditionerRemote>(
                acRepo,
                new FirstAvailableStrategy<AirConditionerRemote>());
            var allocatedAc = acService.Allocate(d => d.IsCompatibleWith("A203"));
            Console.WriteLine($"Đã cấp phát điều hòa phòng A203: {allocatedAc}");

            var micService = new AllocationService<WirelessMicrophone>(
                micRepo,
                new FirstAvailableStrategy<WirelessMicrophone>());
            var firstMic = micService.Allocate();
            Console.WriteLine($"Micro đầu tiên sẵn sàng: {firstMic}");
            firstMic.Return();

            micService.ChangeStrategy(new HighestPriorityStrategy<WirelessMicrophone>(m => m.GetDefaultPriorityScore()));
            var bestMic = micService.Allocate();
            Console.WriteLine($"Micro điểm cao nhất sau khi đổi chiến lược: {bestMic}");
        }
    }
}

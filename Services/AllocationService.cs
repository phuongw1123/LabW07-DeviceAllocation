/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using System.Collections.Generic;
using LabW07_DeviceAllocation.Domain;
using LabW07_DeviceAllocation.Repositories;
using LabW07_DeviceAllocation.Strategies;

namespace LabW07_DeviceAllocation.Services
{
    public class AllocationService<T> where T : TeachingDevice
    {
        private readonly IRepository<T> repository;
        private ISelectionStrategy<T> strategy;

        public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
        {
            strategy = newStrategy ?? throw new ArgumentNullException(nameof(newStrategy));
        }

        public T Allocate(Predicate<T>? additionalCondition = null)
        {
            IReadOnlyList<T> candidates = repository.FindAll(device =>
                device.IsReadyForUse() && (additionalCondition == null || additionalCondition(device)));

            T selectedDevice = strategy.Select(candidates);
            selectedDevice.Borrow();
            return selectedDevice;
        }
    }
}

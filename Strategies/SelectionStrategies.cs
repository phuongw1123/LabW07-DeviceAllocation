/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using System.Collections.Generic;
using LabW07_DeviceAllocation.Exceptions;

namespace LabW07_DeviceAllocation.Strategies
{
    public interface ISelectionStrategy<T>
    {
        T Select(IReadOnlyList<T> candidates);
    }

    public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
    {
        private readonly Func<T, double> scoreSelector;

        public HighestPriorityStrategy(Func<T, double> scoreSelector)
        {
            this.scoreSelector = scoreSelector ?? throw new ArgumentNullException(nameof(scoreSelector));
        }

        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException();

            T bestCandidate = candidates[0];
            double bestScore = scoreSelector(bestCandidate);

            for (int i = 1; i < candidates.Count; i++)
            {
                double currentScore = scoreSelector(candidates[i]);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    bestCandidate = candidates[i];
                }
            }
            return bestCandidate;
        }
    }

    public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
    {
        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException();
            return candidates[0];
        }
    }
}

using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AwesomeCare.DataTransferObject.EqualityComparers
{
    public class LiveTrackerEqualityComparer : EqualityComparer<LiveTracker>
    {
        public override bool Equals([AllowNull] LiveTracker x, [AllowNull] LiveTracker y)
        {
            if (x == null && y == null) return true;

            if (x == null || y == null) return false;

            if ((x.Period == y.Period) && (x.StaffRotaPeriodId == y.StaffRotaPeriodId)) return true;

            return false;
        }

        public override int GetHashCode([DisallowNull] LiveTracker obj)
        {
            var hashCode = obj.StaffRotaPeriodId;

            return hashCode.GetHashCode();
        }
    }

    public class MedTrackerEqualityComparer : EqualityComparer<MedTracker>
    {
        public override bool Equals([AllowNull] MedTracker x, [AllowNull] MedTracker y)
        {
            if (x == null && y == null) return true;

            if (x == null || y == null) return false;

            if ((x.PERIOD == y.PERIOD)) return true;

            return false;
        }

        public override int GetHashCode([DisallowNull] MedTracker obj)
        {
            var hashCode = obj.PERIOD;

            return hashCode.GetHashCode();
        }
    }
}

using AwesomeCare.DataTransferObject.DTOs.Rotering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AwesomeCare.DataTransferObject.EqualityComparers
{
    public class GetStaffRotaItemEqualityComparer : EqualityComparer<Item>
    {
        public override bool Equals([AllowNull] Item x, [AllowNull] Item y)
        {
            if (x == null && y == null) return true;

            if (x == null || y == null) return false;

            if ((x.ClientId == y.ClientId) && (x.StaffRotaPeriodId == y.StaffRotaPeriodId)) return true;

            return false;
        }

        public override int GetHashCode([DisallowNull] Item obj)
        {
            var hashCode = obj.StaffRotaPeriodId ^ obj.ClientId;

            return hashCode.GetHashCode();
        }
    }
}

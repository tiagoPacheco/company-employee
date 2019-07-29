using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace challenge_samsung.Services
{
    public interface IPromotionService
    {
        /// <summary>
        /// Promotes employees using an specific criteria
        /// </summary>
        /// <param name="qtyEmployeesToPromote">Quantity of users that will be promoted</param>
        void Promote(int qtyEmployeesToPromote);
    }
}

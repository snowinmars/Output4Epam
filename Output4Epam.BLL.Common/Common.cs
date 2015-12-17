using Outpu4Epam.DAL.Interface;
using Outpu4Epam.DAL.SQL;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Output4Epam.BLL.Common
{
    internal class Common
    {
        internal static ILotDao<Lot> LotDao { get; } = new LotDao();
        internal static IRegUserDao<RegUser> RegUserDao { get; } = new RegUserDao();
    }
}

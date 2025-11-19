using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using PowerOnRentwebapp.ServiceTerritory;

namespace PowerOnRentwebapp.Territory
{
    public partial class TestTR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UC_Territory1.VisiableUserList = false;
        }

        #region Fill Territory
        [WebMethod]
        public static List<mTerritory> PMFillddlLevel(long level, long parentID)
        {
            List<mTerritory> TerritoryList = new List<mTerritory>();
            try
            {
                UC_Territory uc_territory = new UC_Territory();
                TerritoryList = uc_territory.GetTerritoryList(level, parentID).ToList();
            }
            catch { }
            finally { }
            return TerritoryList;
        }
        #endregion

        [WebMethod]
        public static List<ServiceTerritory.vGetUserProfileList> PMFillddlUserListByTerritory(long level, long parentID)
        {
            List<ServiceTerritory.vGetUserProfileList> UserList = new List<ServiceTerritory.vGetUserProfileList>();
            try
            {
                UC_Territory uc_territory = new UC_Territory();
                UserList = uc_territory.GetUserListByTerritory(level, parentID).ToList();
            }
            catch { }
            finally { }
            return UserList;
        }
    }
}
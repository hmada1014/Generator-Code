using Generator_Code_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Code_Business_Layer
{
    public class clsDatabaseConnectionManager
    {
        public static bool ConnectionStatues(string Server, string Password, string UserID)
        {
           return clsDatabaseConnectionManager_DataAccess.ConnectionStatus(Server,Password,UserID);
        }

    }
}

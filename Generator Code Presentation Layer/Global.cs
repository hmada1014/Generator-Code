using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator_Code_DataAccess_Layer
{
    public class clsGlobal
    {
        public static bool SaveToTheFile(string FilePath)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(FilePath))
                {
                    file.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            
                return false;   
        }

        public struct stOptions
        {
            public bool GetBy;
            public bool AddNew;
            public bool Update;
            public bool Delete;
            public bool GetAll;
            public bool GetTotal;
            public bool IsExists;
            public bool SearchBy;
            public bool GetBusiness;
        }


    }
}

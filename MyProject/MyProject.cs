using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Shared
{
    public struct stCodeGenerateOptions
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

    public struct stCodeGenerateTriggerOptions
    {
        public bool AfterInsert;
        public bool AfterUpdate;
        public bool AfterDelete;

        public bool instedOfInsert;
        public bool instedOfUpdate;
        public bool instedOfDelete;
    }
}

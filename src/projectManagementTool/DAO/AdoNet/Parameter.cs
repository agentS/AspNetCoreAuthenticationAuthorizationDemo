using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectManagementTool.DAO.AdoNet
{
    public struct Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Parameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}

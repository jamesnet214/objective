using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objective.Core.Models
{
    public class ModeModel
    {
        public string Display { get; set; }
        public string Value { get; set; }

        public ModeModel(string display, string value)
        {
            Display = display;
            Value = value;
        }
    }
}

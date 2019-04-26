using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectFocus.Model
{
    public class ProblemModel 
    {
        private string _name;
        private string _description;
        public ProblemModel(string name = "New Problem")
        {
            _name = name;
        }
        public string Name { get; set; }
        public string Description{ get; set; }
    }
}

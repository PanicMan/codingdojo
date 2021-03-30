using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Services
{
    public class AzureWorkItem
    {
        public int WorkItemId { get; set; }
        public string Assigned { get; set; }
        public string Sprint => Iteration.IterationPath;
        public string Title { get; set; }
        [DisplayAttribute(AutoGenerateField = false), Browsable(false)]
        public Iteration Iteration { get; set; }
    }

    public class Iteration
    {
        public string IterationPath { get; set; }
    }
}

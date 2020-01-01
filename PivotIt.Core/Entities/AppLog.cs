using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PivotIt.Core.Entities
{
    public class AppLog
    {
        [Key]
        public string LogID { get; set; }

        public string Level { get; set; }

        public string Source { get; set; }

        public string Details { get; set; }

        public string Exception { get; set; }

        public DateTime LogDate { get; set; }
    }
}

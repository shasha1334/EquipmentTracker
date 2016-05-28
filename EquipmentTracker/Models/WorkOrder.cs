using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EquipmentTracker.Models
{
    public class WorkOrder
    {
        [Display(Name = "Work Order Number")]
        public int WorkOrderID { get; set; }

        public string AssetID { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Problem description cannot be longer than 150 characters.")]
        public string Problem { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date Requested")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRequested { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
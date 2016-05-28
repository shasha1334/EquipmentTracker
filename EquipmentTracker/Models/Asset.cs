using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EquipmentTracker.Models
{
    public class Asset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Asset")]
        public string AssetID { get; set; }

        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters")]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "Serial number cannot be longer than 50 characters")]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Work Orders")]
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquipmentTracker.Models;

namespace EquipmentTracker.ViewModels
{
    public class AssetIndexData
    {
        public IEnumerable<Asset> Assets { get; set; }
        public IEnumerable<WorkOrder> WorkOrders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EquipmentTracker.Models;

namespace EquipmentTracker.DAL
{
    public class EquipmentInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EquipmentContext>
    {
        protected override void Seed(EquipmentContext context)
        {
            var assets = new List<Asset>
            {
                new Asset {AssetID="E21710",Description="C-Arm",SerialNumber="E2-1710"},
                new Asset {AssetID="815395OSFER1",Description="X-ray Radiography Room",SerialNumber="4242"},
                new Asset {AssetID="815226HDCT",Description="CT Scanner",SerialNumber="46546489"}
            };

            assets.ForEach(a => context.Assets.Add(a));
            context.SaveChanges();
            var workOrders = new List<WorkOrder>
            {
                new WorkOrder {WorkOrderID=1,AssetID="E21710",Problem="Will not Fluoro",DateRequested=DateTime.Parse("2016-02-05")},
                new WorkOrder {WorkOrderID=2,AssetID="815395OSFER1",Problem="Not passing QAP",DateRequested=DateTime.Parse("2016-01-25")},
                new WorkOrder {WorkOrderID=3,AssetID="815226HDCT",Problem="Hardware stops",DateRequested=DateTime.Parse("2015-10-15")}
            };

            workOrders.ForEach(w => context.WorkOrders.Add(w));
            context.SaveChanges();
        }
    }
}
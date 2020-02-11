using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace RSCD.DAL
{
    public class RSCDDataModel
    {
        public ObjectId Id { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string LastUpdatedAt { get; set; }
    }

    public class RSCDDataEntryModel : RSCDDataModel
    {
        public string ReferenceCode { get; set; }
        public bool IsActive { get; set; }
        public string InActiveReason { get; set; }
    }
}

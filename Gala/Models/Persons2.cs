using System;
using System.Collections.Generic;

namespace Gala.Models
{
    public partial class Persons2
    {
        public string Iin { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public byte PersonStatusCode { get; set; }
        public DateTime? DeathDate { get; set; }
        public short DistrictCode { get; set; }
        public int RegionCode { get; set; }
        public string RegAddressCity { get; set; }
        public string RegAddressStreet { get; set; }
        public string RegAddressBuilding { get; set; }
        public string RegAddressCorpus { get; set; }
        public string RegAddressFlat { get; set; }
        public byte? UdlTypeCode { get; set; }
        public int? UdlNumber { get; set; }
        public DateTime? UdlBeginDate { get; set; }
        public byte? UdlOrganCode { get; set; }
        public byte? PasspTypeCode { get; set; }
        public string PasspNumber { get; set; }
        public DateTime? PasspBeginDate { get; set; }
        public byte? PasspOrganCode { get; set; }
        public byte? VnzhTypeCode { get; set; }
        public long? VnzhNumber { get; set; }
        public DateTime? VnzhBeginDate { get; set; }
        public byte? VnzhOrganCode { get; set; }
        public byte? LbgTypeCode { get; set; }
        public string LbgNumber { get; set; }
        public DateTime? LbgBeginDate { get; set; }
        public byte? LbgOrganCode { get; set; }

        public virtual Districts DistrictCodeNavigation { get; set; }
        public virtual Regions RegionCodeNavigation { get; set; }
        //public virtual string DistrictName { get; set; }
        //public virtual string RegionName { get; set; }
    }
}

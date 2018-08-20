using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCC.CustomerOrderData.WEBAPI.Models
{

    #region Customer Order Data
    public class OrderData
    {
        public string mobile { get; set; }
    }
    public class Output
    {
        public Package[] WellnessPackage { get; set; }
        public VEPackage[] VanityCube { get; set; }
        public VEPackage[] PersonalCare { get; set; }
        public Status status { get; set; }
    }

    public class Package
    {
        public string PackageId { get; set; }
        public string baseAmount { get; set; }
        public string finalAmount { get; set; }
        public DateTime PackageJoiningDate { get; set; }
        public Service[] Service { get; set; }
        public PaymentMode[] Payments { get; set; }

    }
    public class VEPackage
    {
        public string PackageId { get; set; }
        public string baseAmount { get; set; }
        public string finalAmount { get; set; }
        public DateTime PackageJoiningDate { get; set; }
        public Service[] Service { get; set; }
    }

    public class Service
    {
        public string ServiceName { get; set; }
        public string Quantity { get; set; }
    }

    public class PaymentMode
    {
        public string InvoiceID { get; set; }
        public string PaymentID { get; set; }
        public string AmountPaid { get; set; }
        public string PaymentDate { get; set; }
    }
    #endregion

    #region Profile
    public class ProfileData
    {
        public Profile Profile { get; set; }
        public Status Status { get; set; }
    }

    public class Profile
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string MaritalStatus { get; set; }
        public string Anniversary { get; set; }
        public string HeightinCentimeter { get; set; }
        public string WaistinCentimeter { get; set; }
        public string SkinTone { get; set; }
        public string HairColour { get; set; }
        public string BodyType { get; set; }
        public string BloodGroup { get; set; }
        public string HeartAilments { get; set; }
        public string Bone_JointAilments { get; set; }
        public string PhysicalChallenges { get; set; }
        public string AllergictoanyCosmetics { get; set; }
        public string HairLossIssues { get; set; }
        public string CurrentlyPregnant { get; set; }
        public string customerGuid { get; set; }
        public string centerCode { get; set; }
        public string clientID { get; set; }
    }

    public class UpdateProfile
    {
        public string mobile { get; set; }
        public Profile Profile { get; set; }
    }
    #endregion

    #region Lead
    public class LeadInput
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGender { get; set; }
        public string PhoneNo { get; set; }
        public string CustomerCity { get; set; }
        public string CenterCode { get; set; }
        public string ServiceCategory { get; set; }
        public string Service { get; set; }
        public string SubService { get; set; }
        public string CampaignCode { get; set; }
        public string Source { get; set; }
    }

    #endregion

    #region Common Class
    public class Status
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    #endregion

}
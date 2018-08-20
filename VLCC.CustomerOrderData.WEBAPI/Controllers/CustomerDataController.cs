
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Description;
using System.Text.RegularExpressions;
using System.Web.Http;
using VLCC.CustomerOrderData.WEBAPI.DataSet;
using VLCC.CustomerOrderData.WEBAPI.Models;

namespace VLCC.CustomerOrderData.WEBAPI.Controllers
{
    public class CustomerDataController : ApiController
    {
        public static IOrganizationService ConnectToMSCRM(string UserName, string Password, string SoapOrgServiceUri)
        {
            IOrganizationService _service = null;
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = UserName;
                credentials.UserName.Password = Password;
                Uri serviceUri = new Uri(SoapOrgServiceUri);
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                proxy.EnableProxyTypes();
                _service = (IOrganizationService)proxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
            }
            return _service;
        }

        [AcceptVerbs("POST")]
        public Output getCustomerOrderData([FromBody] OrderData input)
        {
            //string crmURL = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.CRMConnectionString;
            //CrmConnection connections = CrmConnection.Parse(crmURL);
            //OrganizationService orgService = new OrganizationService(connections);
            string UserName = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.UserName;
            string Password = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Password;
            string service = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Service;
            IOrganizationService orgService = ConnectToMSCRM(UserName, Password, service);

            Status status = new Status();
            List<Package> lPackage = new List<Package>();
            List<VEPackage> lVCPackage = new List<VEPackage>();
            List<VEPackage> lPCPackage = new List<VEPackage>();
            FetchOrderData fetchData = new FetchOrderData();
            Output output = new Output();
            try
            {
                if (!string.IsNullOrEmpty(input.mobile))
                {
                    lPackage = fetchData.fetchWellnessData(input.mobile, orgService);
                    lVCPackage = fetchData.fetchVanityCubeData(input.mobile, orgService);
                    lPCPackage = fetchData.fetchPersonalCare(input.mobile, orgService);

                    if (lPackage.Count == 0 && lVCPackage.Count == 0 && lPCPackage.Count == 0)
                    {
                        status.code = "201";
                        status.message = "No Packages found against the Mobile No";
                    }
                    else
                    {
                        if (lPackage.Count != 0)
                            output.WellnessPackage = lPackage.ToArray();
                        if (lVCPackage.Count != 0)
                            output.VanityCube = lVCPackage.ToArray();
                        if (lPCPackage.Count != 0)
                            output.PersonalCare = lPCPackage.ToArray();
                        status.code = "200";
                        status.message = "SUCCESS";
                    }
                }
                else
                {
                    status.code = "201";
                    status.message = "Mobile Number is mandatory";
                }
            }
            catch (Exception ex)
            {
                status.code = "202";
                status.message = ex.Message;
            }
            output.status = status;
            return output;
        }

        [AcceptVerbs("POST")]
        public ProfileData getProfileData([FromBody] OrderData input)
        {
        //    string crmURL = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.CRMConnectionString;
        //    CrmConnection connections = CrmConnection.Parse(crmURL);
        //    OrganizationService orgService = new OrganizationService(connections);

            string UserName = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.UserName;
            string Password = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Password;
            string service = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Service;
            IOrganizationService orgService = ConnectToMSCRM(UserName, Password, service);
            Status status = new Status();
            ProfileData _profile = new ProfileData();
            FetchProfileData fetchProfile = new FetchProfileData();

            if (!string.IsNullOrEmpty(input.mobile))
            {
                _profile = fetchProfile.fetchProfile(input.mobile, orgService);
            }
            else
            {
                status.code = "202";
                status.message = "Mobile Number is mandatory";
                _profile.Status = status;

            }
            return _profile;
        }

        [AcceptVerbs("POST")]
        public Status updateProfile([FromBody] UpdateProfile input)
        {
            //string crmURL = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.CRMConnectionString;
            //CrmConnection connections = CrmConnection.Parse(crmURL);
            //OrganizationService orgService = new OrganizationService(connections);

            string UserName = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.UserName;
            string Password = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Password;
            string service = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Service;
            IOrganizationService orgService = ConnectToMSCRM(UserName, Password, service);
            Status status = new Status();
            FetchProfileData _fetchProfile = new FetchProfileData();

            if (!string.IsNullOrEmpty(input.mobile))
            {
                status = _fetchProfile.updateProfileData(input, orgService);
            }
            else
            {
                status.code = "201";
                status.message = "Mobile Number Not present";
            }
            return status;
        }

        [AcceptVerbs("POST")]
        public Status createLead([FromBody] LeadInput input)
        {
            //string crmURL = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.CRMConnectionString;
            //CrmConnection connections = CrmConnection.Parse(crmURL);
            //OrganizationService orgService = new OrganizationService(connections);
            string UserName = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.UserName;
            string Password = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Password;
            string service = VLCC.CustomerOrderData.WEBAPI.Properties.Settings.Default.Service;
            IOrganizationService orgService = ConnectToMSCRM(UserName, Password, service);
            Status status = new Status();
            ClientEnquiry clientEnquiry = new ClientEnquiry();

            if (!string.IsNullOrEmpty(input.PhoneNo))
            {
                if (Regex.IsMatch(input.PhoneNo, @"\d{10}$"))
                {
                    status = clientEnquiry.createEnquiry(input, orgService);
                }
                else
                {
                    status.code = "201";
                    status.message = "Mobile number is invalid";
                }
            }
            else
            {
                status.code = "201";
                status.message = "Mobile Number is mandatory";
            }
            return status;
        }
    }
}

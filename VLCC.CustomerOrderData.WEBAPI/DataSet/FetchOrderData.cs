
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;
using VLCC.CustomerOrderData.WEBAPI.Models;

namespace VLCC.CustomerOrderData.WEBAPI.DataSet
{
    public class FetchOrderData
    {
        #region Wellness
        public List<Package> fetchWellnessData(string mobile, IOrganizationService orgService)
        {
            List<Package> lPackage = new List<Package>();

            #region Package
            QueryExpression qePackage = new QueryExpression("new_new_packagebooking1");
            qePackage.ColumnSet = new ColumnSet("new_name", "new_joiningdate", "pcl_finalamount", "new_amountextax");
            qePackage.AddOrder("new_joiningdate", OrderType.Descending);
            qePackage.PageInfo = new PagingInfo();
            qePackage.PageInfo.Count = 5;//Top 5 records
            qePackage.PageInfo.PageNumber = 1;
            LinkEntity leClient = new LinkEntity
            {
                LinkFromEntityName = "new_new_packagebooking1",
                LinkFromAttributeName = "new_clientname",
                LinkToEntityName = "new_client1",
                LinkToAttributeName = "new_client1id"
            };
            leClient.LinkCriteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
            qePackage.LinkEntities.Add(leClient);
            EntityCollection ecPackage = orgService.RetrieveMultiple(qePackage);

            foreach (Entity itemP in ecPackage.Entities)
            {
                Package _package = new Package();
                if (itemP.Contains("new_name"))
                    _package.PackageId = itemP["new_name"].ToString();
                if (itemP.Contains("new_joiningdate"))
                    _package.PackageJoiningDate = Convert.ToDateTime(itemP["new_joiningdate"]).AddMinutes(330);//.ToString();
                if (itemP.Contains("new_amountextax"))
                    _package.baseAmount = ((Money)itemP["new_amountextax"]).Value.ToString();
                if (itemP.Contains("pcl_finalamount"))
                    _package.finalAmount = ((Money)itemP["pcl_finalamount"]).Value.ToString();
                List<Service> lService = fetchServices(itemP.Id, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, orgService);
                if (lService.Count != 0)
                {
                    _package.Service = lService.ToArray();
                }
                List<PaymentMode> lPayment = fetchPayments(itemP.Id, Guid.Empty, Guid.Empty, orgService);
                if (lPayment.Count != 0)
                {
                    _package.Payments = lPayment.ToArray();
                }

                lPackage.Add(_package);
            }
            #endregion

            #region RBS
            QueryExpression qeRBS = new QueryExpression("new_beautyregularserviceslip");
            qeRBS.ColumnSet = new ColumnSet("new_name", "pcl_date", "pcl_finalamount", "new_totalamo");
            qeRBS.AddOrder("pcl_date", OrderType.Descending);
            qeRBS.PageInfo = new PagingInfo();
            qeRBS.PageInfo.Count = 5;//Top 5 records
            qeRBS.PageInfo.PageNumber = 1;
            LinkEntity leClientRBS = new LinkEntity
            {
                LinkFromEntityName = "new_new_packagebooking1",
                LinkFromAttributeName = "new_clientname",
                LinkToEntityName = "new_client1",
                LinkToAttributeName = "new_client1id"
            };
            leClientRBS.LinkCriteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
            qeRBS.LinkEntities.Add(leClientRBS);
            EntityCollection ecRBS = orgService.RetrieveMultiple(qeRBS);

            foreach (Entity itemRBS in ecRBS.Entities)
            {
                Package _package = new Package();
                if (itemRBS.Contains("new_name"))
                    _package.PackageId = itemRBS["new_name"].ToString();
                if (itemRBS.Contains("pcl_date"))
                    _package.PackageJoiningDate = Convert.ToDateTime(itemRBS["pcl_date"]).AddMinutes(330);//.ToString();
                if (itemRBS.Contains("new_totalamo"))
                    _package.baseAmount = ((Money)itemRBS["new_totalamo"]).Value.ToString();
                if (itemRBS.Contains("pcl_finalamount"))
                    _package.finalAmount = ((Money)itemRBS["pcl_finalamount"]).Value.ToString();
                List<Service> lService = fetchServices(Guid.Empty, itemRBS.Id, Guid.Empty, Guid.Empty, Guid.Empty, orgService);
                if (lService.Count != 0)
                {
                    _package.Service = lService.ToArray();
                }
                List<PaymentMode> lPayment = fetchPayments(Guid.Empty, itemRBS.Id, Guid.Empty, orgService);
                if (lPayment.Count != 0)
                {
                    _package.Payments = lPayment.ToArray();
                }

                lPackage.Add(_package);
            }
            #endregion

            #region Retail
            QueryExpression qeRetail = new QueryExpression("new_retailbilling");
            qeRetail.ColumnSet = new ColumnSet("new_amountextax1", "new_totalamountinctax1", "new_name", "pcl_date");
            qeRetail.AddOrder("pcl_date", OrderType.Descending);
            qeRetail.PageInfo = new PagingInfo();
            qeRetail.PageInfo.Count = 5;//Top 5 records
            qeRetail.PageInfo.PageNumber = 1;
            LinkEntity leClientRetail = new LinkEntity
            {
                LinkFromEntityName = "new_retailbilling",
                LinkFromAttributeName = "new_clientname",
                LinkToEntityName = "new_client1",
                LinkToAttributeName = "new_client1id"
            };
            leClientRetail.LinkCriteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
            qeRetail.LinkEntities.Add(leClientRetail);
            EntityCollection ecRetail = orgService.RetrieveMultiple(qeRetail);

            foreach (Entity itemRetail in ecRetail.Entities)
            {
                Package _package = new Package();
                if (itemRetail.Contains("new_name"))
                    _package.PackageId = itemRetail["new_name"].ToString();
                if (itemRetail.Contains("pcl_date"))
                    _package.PackageJoiningDate = Convert.ToDateTime(itemRetail["pcl_date"]).AddMinutes(330);//.ToString();
                if (itemRetail.Contains("new_amountextax1"))
                    _package.baseAmount = ((Money)itemRetail["new_amountextax1"]).Value.ToString();
                if (itemRetail.Contains("new_totalamountinctax1"))
                    _package.finalAmount = ((Money)itemRetail["new_totalamountinctax1"]).Value.ToString();
                List<Service> lService = fetchServices(Guid.Empty, Guid.Empty, itemRetail.Id, Guid.Empty, Guid.Empty, orgService);
                if (lService.Count != 0)
                {
                    _package.Service = lService.ToArray();
                }
                List<PaymentMode> lPayment = fetchPayments(Guid.Empty, Guid.Empty, itemRetail.Id, orgService);
                if (lPayment.Count != 0)
                {
                    _package.Payments = lPayment.ToArray();
                }

                lPackage.Add(_package);
            }
            #endregion

            if (lPackage.Count > 0)
            {
                lPackage = lPackage.OrderByDescending(o => o.PackageJoiningDate).Take(5).ToList();
            }

            return lPackage;
        }
        public List<PaymentMode> fetchPayments(Guid PackageId, Guid RBSId, Guid RetailId, IOrganizationService orgService)
        {
            List<PaymentMode> lPayment = new List<PaymentMode>();

            QueryExpression qePayment = new QueryExpression("new_paymentmode");
            qePayment.ColumnSet = new ColumnSet("pcl_invoice", "new_paymentmode", "pcl_paymentdate", "new_name", "new_amount");
            LinkEntity leInvoice = new LinkEntity
            {
                LinkFromEntityName = "new_paymentmode",
                LinkFromAttributeName = "pcl_invoice",
                LinkToEntityName = "pcl_invoice",
                LinkToAttributeName = "pcl_invoiceid",
                EntityAlias = "eaInvoice"
            };
            if (PackageId != Guid.Empty)
            {
                LinkEntity lePackage = new LinkEntity
                {
                    LinkFromEntityName = "pcl_invoice",
                    LinkFromAttributeName = "pcl_packagebooking",
                    LinkToEntityName = "new_new_packagebooking1",
                    LinkToAttributeName = "new_new_packagebooking1id",
                    Columns = new ColumnSet("new_center"),
                    EntityAlias = "eaPackage"
                };
                lePackage.LinkCriteria.AddCondition("new_new_packagebooking1id", ConditionOperator.Equal, PackageId);
                leInvoice.LinkEntities.Add(lePackage);
            }
            else if (RBSId != Guid.Empty)
            {
                LinkEntity leRBS = new LinkEntity
                {
                    LinkFromEntityName = "pcl_invoice",
                    LinkFromAttributeName = "pcl_rbs",
                    LinkToEntityName = "new_beautyregularserviceslip",
                    LinkToAttributeName = "new_beautyregularserviceslipid",
                    //Columns = new ColumnSet("new_center"),
                    EntityAlias = "eaRBS"
                };
                leRBS.LinkCriteria.AddCondition("new_beautyregularserviceslipid", ConditionOperator.Equal, RBSId);
                leInvoice.LinkEntities.Add(leRBS);
            }
            else if (RetailId != Guid.Empty)
            {
                LinkEntity leRetail = new LinkEntity
                {
                    LinkFromEntityName = "pcl_invoice",
                    LinkFromAttributeName = "pcl_retailbilling",
                    LinkToEntityName = "new_retailbilling",
                    LinkToAttributeName = "new_retailbillingid",
                    //Columns = new ColumnSet("new_center"),
                    EntityAlias = "eaRetail"
                };
                leRetail.LinkCriteria.AddCondition("new_retailbillingid", ConditionOperator.Equal, RetailId);
                leInvoice.LinkEntities.Add(leRetail);
            }
            else
            {
                new List<PaymentMode>();
            }
            qePayment.LinkEntities.Add(leInvoice);

            EntityCollection ecPayment = orgService.RetrieveMultiple(qePayment);

            foreach (Entity itemPayment in ecPayment.Entities)
            {
                PaymentMode _payment = new PaymentMode();
                if (itemPayment.Contains("pcl_paymentdate"))
                    _payment.PaymentDate = Convert.ToDateTime(itemPayment["pcl_paymentdate"]).AddMinutes(330).Date.ToString();
                if (itemPayment.Contains("new_amount"))
                    _payment.AmountPaid = ((Money)itemPayment["new_amount"]).Value.ToString();
                if (itemPayment.Contains("pcl_invoice"))
                    _payment.InvoiceID = ((EntityReference)itemPayment["pcl_invoice"]).Name.ToString();
                if (itemPayment.Contains("new_name"))
                    _payment.PaymentID = itemPayment["new_name"].ToString();
                if (_payment != null)
                    lPayment.Add(_payment);
            }
            return lPayment;
        }

        #endregion

        #region Vanity Cube
        public List<VEPackage> fetchVanityCubeData(string mobile, IOrganizationService orgService)
        {
            List<VEPackage> lVanityPackage = new List<VEPackage>();

            QueryExpression qeVCPackage = new QueryExpression("pcl_vanitycubebooking");
            qeVCPackage.ColumnSet = new ColumnSet("pcl_name", "pcl_servicedate", "pcl_totalamount", "pcl_serviceamount");
            qeVCPackage.AddOrder("pcl_servicedate", OrderType.Descending);
            qeVCPackage.PageInfo = new PagingInfo();
            qeVCPackage.PageInfo.Count = 5;//Top 5 records
            qeVCPackage.PageInfo.PageNumber = 1;
            LinkEntity leClient = new LinkEntity
            {
                LinkFromEntityName = "pcl_vanitycubebooking",
                LinkFromAttributeName = "pcl_clientid",
                LinkToEntityName = "new_client1",
                LinkToAttributeName = "new_client1id"
            };
            leClient.LinkCriteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
            qeVCPackage.LinkEntities.Add(leClient);

            EntityCollection ecVCPackage = orgService.RetrieveMultiple(qeVCPackage);
            foreach (Entity itemVC in ecVCPackage.Entities)
            {
                VEPackage _package = new VEPackage();
                if (itemVC.Contains("pcl_name"))
                    _package.PackageId = itemVC["pcl_name"].ToString();
                if (itemVC.Contains("pcl_servicedate"))
                    _package.PackageJoiningDate = Convert.ToDateTime(itemVC["pcl_servicedate"]).AddMinutes(330);//.ToString();
                if (itemVC.Contains("pcl_serviceamount"))
                    _package.baseAmount = ((Money)itemVC["pcl_serviceamount"]).Value.ToString();
                if (itemVC.Contains("pcl_totalamount"))
                    _package.finalAmount = ((Money)itemVC["pcl_totalamount"]).Value.ToString();
                List<Service> lService = fetchServices(Guid.Empty, Guid.Empty, Guid.Empty, itemVC.Id, Guid.Empty, orgService);
                if (lService.Count != 0)
                {
                    _package.Service = lService.ToArray();
                }

                lVanityPackage.Add(_package);
            }
            if (lVanityPackage.Count > 0)
            {
                lVanityPackage = lVanityPackage.OrderByDescending(o => o.PackageJoiningDate).Take(5).ToList();
            }
            return lVanityPackage;
        }
        #endregion

        #region Personal Care
        public List<VEPackage> fetchPersonalCare(string mobile, IOrganizationService orgService)
        {
            List<VEPackage> lPersonalCare = new List<VEPackage>();

            QueryExpression qeEPackage = new QueryExpression("pcl_ecommerceorderhistory");
            qeEPackage.ColumnSet = new ColumnSet("pcl_ordernumber", "pcl_orderdate", "pcl_finalorderamount", "pcl_orderamount");
            qeEPackage.AddOrder("pcl_orderdate", OrderType.Descending);
            qeEPackage.PageInfo = new PagingInfo();
            qeEPackage.PageInfo.Count = 5;//Top 5 records
            qeEPackage.PageInfo.PageNumber = 1;
            LinkEntity leClient = new LinkEntity
            {
                LinkFromEntityName = "pcl_ecommerceorderhistory",
                LinkFromAttributeName = "pcl_customername",
                LinkToEntityName = "new_client1",
                LinkToAttributeName = "new_client1id"
            };
            leClient.LinkCriteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
            qeEPackage.LinkEntities.Add(leClient);

            EntityCollection ecEPackage = orgService.RetrieveMultiple(qeEPackage);
            foreach (Entity itemPCare in ecEPackage.Entities)
            {
                VEPackage _package = new VEPackage();
                if (itemPCare.Contains("pcl_ordernumber"))
                    _package.PackageId = itemPCare["pcl_ordernumber"].ToString();
                if (itemPCare.Contains("pcl_orderdate"))
                    _package.PackageJoiningDate = Convert.ToDateTime(itemPCare["pcl_orderdate"]).AddMinutes(330);//.ToString();
                if (itemPCare.Contains("pcl_orderamount"))
                    _package.baseAmount = ((Money)itemPCare["pcl_orderamount"]).Value.ToString();
                if (itemPCare.Contains("pcl_finalorderamount"))
                    _package.finalAmount = ((Money)itemPCare["pcl_finalorderamount"]).Value.ToString();
                List<Service> lService = fetchServices(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, itemPCare.Id, orgService);
                if (lService.Count != 0)
                {
                    _package.Service = lService.ToArray();
                }

                lPersonalCare.Add(_package);
            }
            if (lPersonalCare.Count > 0)
            {
                lPersonalCare = lPersonalCare.OrderBy(o => o.PackageJoiningDate).Take(5).ToList();
            }

            return lPersonalCare;
        }
        #endregion

        #region Common Function
        public List<Service> fetchServices(Guid PackageId, Guid RBSId, Guid RetailId, Guid VanityCubeId, Guid PCareId, IOrganizationService orgService)
        {
            List<Service> lService = new List<Service>();

            if (RetailId != Guid.Empty)
            {
                QueryExpression qeStorageBill = new QueryExpression("new_storagebill");
                qeStorageBill.ColumnSet = new ColumnSet("new_productname", "new_itemqty");
                qeStorageBill.Criteria.AddCondition("new_retaillink", ConditionOperator.Equal, RetailId);
                EntityCollection ecStorageBill = orgService.RetrieveMultiple(qeStorageBill);

                foreach (Entity itemStorage in ecStorageBill.Entities)
                {
                    Service _service = new Service();
                    if (itemStorage.Contains("new_productname"))
                    {
                        _service.ServiceName = ((EntityReference)itemStorage["new_productname"]).Name.ToString();
                    }
                    if (!string.IsNullOrEmpty(_service.ServiceName))
                        if (itemStorage.Contains("new_itemqty"))
                            _service.Quantity = itemStorage["new_itemqty"].ToString();
                    if (_service.ServiceName != null)
                        lService.Add(_service);
                }
            }
            else if (PackageId != Guid.Empty || RBSId != Guid.Empty)
            {
                QueryExpression qePExec = new QueryExpression("new_packageexecution");
                if (PackageId != Guid.Empty)
                {
                    qePExec.ColumnSet = new ColumnSet("new_servicename1", "new_quantity", "new_complementaryservicename");
                    qePExec.Criteria.AddCondition("new_packageid", ConditionOperator.Equal, PackageId);
                }
                else if (RBSId != Guid.Empty)
                {
                    qePExec.ColumnSet = new ColumnSet("new_rbsmaster", "new_quantity");
                    qePExec.Criteria.AddCondition("new_rbs", ConditionOperator.Equal, RBSId);
                }
                else
                {
                    return new List<Service>();
                }
                EntityCollection ecPackageExec = orgService.RetrieveMultiple(qePExec);

                foreach (Entity itemPExec in ecPackageExec.Entities)
                {
                    Service _service = new Service();
                    if (PackageId != Guid.Empty)
                    {
                        if (itemPExec.Contains("new_servicename1"))
                        {
                            _service.ServiceName = ((EntityReference)itemPExec["new_servicename1"]).Name.ToString();
                        }
                        else if (itemPExec.Contains("new_complementaryservicename"))
                        {
                            _service.ServiceName = ((EntityReference)itemPExec["new_complementaryservicename"]).Name.ToString();
                        }
                    }
                    else if (RBSId != Guid.Empty)
                    {
                        if (itemPExec.Contains("new_rbsmaster"))
                        {
                            _service.ServiceName = ((EntityReference)itemPExec["new_rbsmaster"]).Name.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(_service.ServiceName))
                    {
                        if (RBSId != Guid.Empty)
                        {
                            if (itemPExec.Contains("new_quantity"))
                                _service.Quantity = itemPExec["new_quantity"].ToString();
                            else
                                _service.Quantity = "1";
                        }
                        else
                        {
                            if (itemPExec.Contains("new_quantity"))
                                _service.Quantity = itemPExec["new_quantity"].ToString();
                        }
                    }
                    if (_service.ServiceName != null)
                        lService.Add(_service);
                }
            }
            else if (VanityCubeId != Guid.Empty)
            {
                QueryExpression qeVanityService = new QueryExpression("pcl_vanitycubebookingservices");
                qeVanityService.ColumnSet = new ColumnSet("pcl_servicetypeid", "pcl_person");
                qeVanityService.Criteria.AddCondition("pcl_vanitycubebookingid", ConditionOperator.Equal, VanityCubeId);
                EntityCollection ecVanityService = orgService.RetrieveMultiple(qeVanityService);

                foreach (Entity itemVanityService in ecVanityService.Entities)
                {
                    Service _service = new Service();
                    if (itemVanityService.Contains("pcl_servicetypeid"))
                    {
                        _service.ServiceName = ((EntityReference)itemVanityService["pcl_servicetypeid"]).Name.ToString();
                    }
                    if (!string.IsNullOrEmpty(_service.ServiceName))
                        if (itemVanityService.Contains("pcl_person"))
                            _service.Quantity = itemVanityService["pcl_person"].ToString();
                    if (_service.ServiceName != null)
                        lService.Add(_service);
                }
            }
            else if (PCareId != Guid.Empty)
            {
                QueryExpression qePersonalCare = new QueryExpression("pcl_ecommerceorderitem");
                qePersonalCare.ColumnSet = new ColumnSet("pcl_product", "pcl_quantity");
                qePersonalCare.Criteria.AddCondition("pcl_ecommerceorderhistory", ConditionOperator.Equal, PCareId);
                EntityCollection ecPersonalCare = orgService.RetrieveMultiple(qePersonalCare);

                foreach (Entity itemPersonalCare in ecPersonalCare.Entities)
                {
                    Service _service = new Service();
                    if (itemPersonalCare.Contains("pcl_product"))
                    {
                        _service.ServiceName = ((EntityReference)itemPersonalCare["pcl_product"]).Name.ToString();
                    }
                    if (!string.IsNullOrEmpty(_service.ServiceName))
                        if (itemPersonalCare.Contains("pcl_quantity"))
                            _service.Quantity = itemPersonalCare["pcl_quantity"].ToString();
                    if (_service.ServiceName != null)
                        lService.Add(_service);
                }
            }
            return lService;
        }
        #endregion
    }
    public class FetchProfileData
    {
        public ProfileData fetchProfile(string mobile, IOrganizationService orgService)
        {
            Profile _profile = new Profile();
            ProfileData _profiledata = new ProfileData();
            Status _status = new Status();
            try
            {
                QueryExpression qeCustomer = new QueryExpression("new_client1");
                qeCustomer.Criteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
                qeCustomer.AddOrder("pcl_joiningdate", OrderType.Descending);
                qeCustomer.ColumnSet = new ColumnSet("new_name", "new_emailid", "new_genderoption", "pcl_bodytype", "pcl_haircolour", "new_birthday",
                "new_address", "new_addressline2", "new_cty", "new_city", "new_maritalstatus", "pcl_bonejointailments", "pcl_havehairlossissues", "pcl_waist",
                "new_anniversary", "new_heightin", "pcl_allergictocosmetics", "pcl_areyoucurrentlypregnant", "pcl_bloodgroup", "pcl_heartailments", "pcl_physicalchallenges", "pcl_skintone", "new_client1id", "new_centre", "new_clientid");
                EntityCollection ecCustomer = orgService.RetrieveMultiple(qeCustomer);

                if (ecCustomer.Entities.Count > 0)
                {
                    //new_centrecode
                    if (ecCustomer[0].Contains("new_centre"))
                    {
                        string centerguid = ((EntityReference)ecCustomer[0].Attributes["new_centre"]).Id.ToString();
                        QueryExpression qeCenterCode = new QueryExpression("new_center");
                        qeCenterCode.Criteria.AddCondition("new_centerid",ConditionOperator.Equal,centerguid);
                        qeCenterCode.ColumnSet.AddColumns("new_centrecode");
                        EntityCollection ecCenterCode = orgService.RetrieveMultiple(qeCenterCode);
                        if (ecCenterCode.Entities.Count > 0 && ecCenterCode.Entities != null)
                        {
                            _profile.centerCode = ecCenterCode.Entities[0].Attributes["new_centrecode"].ToString();
                        }
                    }
                   

                    string address1 = string.Empty, address2 = string.Empty, city = string.Empty, pincode = string.Empty, address = string.Empty;
                    string waist = string.Empty;
                    Entity eCustomer = ecCustomer[0];
                    if (eCustomer.Contains("new_name"))
                        _profile.Name = eCustomer["new_name"].ToString();
                    if (eCustomer.Contains("new_emailid"))
                        _profile.EmailId = eCustomer["new_emailid"].ToString();
                    if (eCustomer.Contains("new_genderoption"))
                        _profile.Gender = eCustomer.FormattedValues["new_genderoption"].ToString();
                    if (eCustomer.Contains("new_client1id"))
                    {
                        _profile.customerGuid = eCustomer["new_client1id"].ToString();
                    }
                    if(eCustomer.Contains("new_clientid"))
                    {
                        _profile.clientID = eCustomer["new_clientid"].ToString();
                    }

                    #region Address
                    if (eCustomer.Contains("new_address"))
                        address1 = eCustomer["new_address"].ToString();
                    if (eCustomer.Contains("new_addressline2"))
                        address2 = eCustomer["new_addressline2"].ToString();
                    if (eCustomer.Contains("new_cty"))
                        city = ((EntityReference)eCustomer["new_cty"]).Name.ToString();
                    if (eCustomer.Contains("new_city"))
                        pincode = eCustomer["new_city"].ToString();
                    address = address1 + " " + address2 + " " + city + " " + pincode;
                    if (!string.IsNullOrEmpty(address))
                        _profile.Address = address;
                    #endregion

                    if (eCustomer.Contains("new_maritalstatus"))
                        _profile.MaritalStatus = eCustomer.FormattedValues["new_maritalstatus"].ToString();
                    if (eCustomer.Contains("new_anniversary"))
                        _profile.Anniversary = Convert.ToDateTime(eCustomer["new_anniversary"]).AddMinutes(330).Date.ToString("dd/MM/yyyy");
                    if (eCustomer.Contains("new_heightin"))
                        _profile.HeightinCentimeter = Math.Round(Convert.ToDecimal(eCustomer["new_heightin"]), 2).ToString();
                    if (eCustomer.Contains("new_birthday"))
                        _profile.Birthday = Convert.ToDateTime(eCustomer["new_birthday"]).AddMinutes(330).Date.ToString("dd/MM/yyyy");
                    if (eCustomer.Contains("pcl_bodytype"))
                        _profile.BodyType = eCustomer["pcl_bodytype"].ToString();
                    if (eCustomer.Contains("pcl_haircolour"))
                        _profile.HairColour = eCustomer["pcl_haircolour"].ToString();
                    if (eCustomer.Contains("pcl_bonejointailments"))
                        _profile.Bone_JointAilments = eCustomer["pcl_bonejointailments"].ToString();
                    if (eCustomer.Contains("pcl_havehairlossissues"))
                        _profile.HairLossIssues = eCustomer.FormattedValues["pcl_havehairlossissues"].ToString();
                    if (eCustomer.Contains("pcl_allergictocosmetics"))
                        _profile.AllergictoanyCosmetics = eCustomer["pcl_allergictocosmetics"].ToString();
                    if (eCustomer.Contains("pcl_areyoucurrentlypregnant"))
                        _profile.CurrentlyPregnant = eCustomer.FormattedValues["pcl_areyoucurrentlypregnant"].ToString();
                    if (eCustomer.Contains("pcl_bloodgroup"))
                        _profile.BloodGroup = eCustomer["pcl_bloodgroup"].ToString();
                    if (eCustomer.Contains("pcl_heartailments"))
                        _profile.HeartAilments = eCustomer["pcl_heartailments"].ToString();
                    if (eCustomer.Contains("pcl_physicalchallenges"))
                        _profile.PhysicalChallenges = eCustomer["pcl_physicalchallenges"].ToString();
                    if (eCustomer.Contains("pcl_skintone"))
                        _profile.SkinTone = eCustomer["pcl_skintone"].ToString();
                    if (eCustomer.Contains("pcl_waist"))
                        _profile.WaistinCentimeter = Math.Round(Convert.ToDecimal(eCustomer["pcl_waist"]), 2).ToString();
                    _status.code = "200";
                    _status.message = "SUCCESS";
                }
                else
                {
                    _status.code = "201";
                    _status.message = "No Customer Found";
                }
            }
            catch (Exception ex)
            {
                _status.code = "201";
                _status.code = ex.Message;
            }
            _profiledata.Profile = _profile;
            _profiledata.Status = _status;
            return _profiledata;
        }

        public Status updateProfileData(UpdateProfile input, IOrganizationService orgService)
        {
            Status _status = new Status();
            try
            {
                Guid CustomerId = CheckExistingCustomer(input.mobile, orgService);

                if (CustomerId != Guid.Empty)
                {
                    Entity eCustomer = new Entity("new_client1");

                    if (!string.IsNullOrEmpty(input.Profile.EmailId))
                        eCustomer["new_emailid"] = input.Profile.EmailId;
                    if (!string.IsNullOrEmpty(input.Profile.Address))
                    {
                        if (input.Profile.Address.Length > 100)
                        {
                            eCustomer["new_address"] = input.Profile.Address.Substring(0, 100);
                            eCustomer["new_addressline2"] = input.Profile.Address.Substring(100, input.Profile.Address.Length - 100);
                        }
                        else
                        {
                            eCustomer["new_address"] = input.Profile.Address;
                        }
                    }
                    if (!string.IsNullOrEmpty(input.Profile.Birthday))
                        eCustomer["new_birthday"] = Convert.ToDateTime(input.Profile.Birthday);
                    if (!string.IsNullOrEmpty(input.Profile.AllergictoanyCosmetics))
                        eCustomer["pcl_allergictocosmetics"] = input.Profile.AllergictoanyCosmetics;
                    if (!string.IsNullOrEmpty(input.Profile.Anniversary))
                        eCustomer["new_anniversary"] = Convert.ToDateTime(input.Profile.Anniversary);
                    if (!string.IsNullOrEmpty(input.Profile.BloodGroup))
                        eCustomer["pcl_bloodgroup"] = input.Profile.BloodGroup;
                    if (!string.IsNullOrEmpty(input.Profile.BodyType))
                        eCustomer["pcl_bodytype"] = input.Profile.BodyType;
                    if (!string.IsNullOrEmpty(input.Profile.Bone_JointAilments))
                        eCustomer["pcl_bonejointailments"] = input.Profile.Bone_JointAilments;
                    if (!string.IsNullOrEmpty(input.Profile.CurrentlyPregnant))
                    {
                        eCustomer["pcl_areyoucurrentlypregnant"] = input.Profile.CurrentlyPregnant.ToLower() == "yes" ? true : false;
                    }
                    if (!string.IsNullOrEmpty(input.Profile.Gender))
                    {
                        string gender = input.Profile.Gender.ToLower();
                        if (gender.Equals("male"))
                            eCustomer["new_genderoption"] = new OptionSetValue(100000001);
                        else if (gender.Equals("female"))
                            eCustomer["new_genderoption"] = new OptionSetValue(100000000);
                        else if (gender.Equals("transgender"))
                            eCustomer["new_genderoption"] = new OptionSetValue(3);
                        else
                        {
                            _status.code = "202";
                            _status.message = "Gender should be either Male,Female or Transgender";
                            return _status;
                        }
                    }
                    if (!string.IsNullOrEmpty(input.Profile.HairColour))
                        eCustomer["pcl_haircolour"] = input.Profile.HairColour;
                    if (!string.IsNullOrEmpty(input.Profile.HairLossIssues))
                        eCustomer["pcl_havehairlossissues"] = input.Profile.HairLossIssues.ToLower() == "yes" ? true : false;
                    if (!string.IsNullOrEmpty(input.Profile.HeartAilments))
                        eCustomer["pcl_heartailments"] = input.Profile.HeartAilments;
                    if (!string.IsNullOrEmpty(input.Profile.HeightinCentimeter))
                        eCustomer["new_heightin"] = Convert.ToDecimal(input.Profile.HeightinCentimeter);
                    if (!string.IsNullOrEmpty(input.Profile.MaritalStatus))
                    {
                        string maritalStatus = input.Profile.MaritalStatus.ToLower();
                        if (maritalStatus.Equals("married"))
                            eCustomer["new_maritalstatus"] = new OptionSetValue(100000000);
                        else if (maritalStatus.Equals("unmarried"))
                            eCustomer["new_maritalstatus"] = new OptionSetValue(100000001);
                        else if (maritalStatus.Equals("single"))
                            eCustomer["new_maritalstatus"] = new OptionSetValue(100000002);
                        else if (maritalStatus.Equals("divorced"))
                            eCustomer["new_maritalstatus"] = new OptionSetValue(100000004);
                        else
                        {
                            _status.code = "202";
                            _status.message = "Maritial Status can be Married,Unmarried,Single,Divorced";
                            return _status;
                        }
                    }
                    if (!string.IsNullOrEmpty(input.Profile.Name))
                        eCustomer["new_name"] = input.Profile.Name;
                    if (!string.IsNullOrEmpty(input.Profile.PhysicalChallenges))
                        eCustomer["pcl_physicalchallenges"] = input.Profile.PhysicalChallenges;
                    if (!string.IsNullOrEmpty(input.Profile.SkinTone))
                        eCustomer["pcl_skintone"] = input.Profile.SkinTone;
                    if (!string.IsNullOrEmpty(input.Profile.WaistinCentimeter))
                        eCustomer["pcl_waist"] = Convert.ToDecimal(input.Profile.WaistinCentimeter);

                    eCustomer.Id = CustomerId;
                    orgService.Update(eCustomer);

                    _status.code = "200";
                    _status.message = "SUCCESS";
                }
                else
                {
                    _status.code = "201";
                    _status.message = "Customer Does Not Exist";
                }
            }
            catch (Exception ex)
            {
                _status.code = "201";
                _status.message = ex.Message;
            }
            return _status;
        }

        public Guid CheckExistingCustomer(string mobile, IOrganizationService orgService)
        {
            Guid Customer = Guid.Empty;

            QueryExpression qeCustomer = new QueryExpression("new_client1");
            qeCustomer.Criteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);
            qeCustomer.AddOrder("pcl_joiningdate", OrderType.Descending);

            EntityCollection ecCustomer = orgService.RetrieveMultiple(qeCustomer);

            if (ecCustomer.Entities.Count > 0)
            {
                Customer = ecCustomer[0].Id;
            }
            return Customer;
        }


    }

    public class ClientEnquiry
    {
        public Status createEnquiry(LeadInput input, IOrganizationService orgService)
        {
            Status _status = new Status();
            try
            {

                Entity eLead = new Entity("new_cientenquiry1");
                if (!string.IsNullOrEmpty(input.CustomerCity))
                {
                    Guid cityId = getCityId(input.CustomerCity, orgService);
                    if (cityId != Guid.Empty)
                        eLead["new_ct"] = new EntityReference("new_city", cityId);
                    else
                    {
                        _status.code = "202";
                        _status.message = "City Not Found in CRM";
                        return _status;
                    }
                }
                if (!string.IsNullOrEmpty(input.CenterCode))
                {
                    Guid centerId = getCenterId(input.CenterCode, orgService);
                    if (centerId != Guid.Empty)
                        eLead["new_center"] = new EntityReference("new_center", centerId);
                    else
                    {
                        _status.code = "202";
                        _status.message = "Center Not Found in CRM";
                        return _status;
                    }
                }
                if (!string.IsNullOrEmpty(input.CustomerEmail))
                    eLead["new_emailid1"] = input.CustomerEmail;
                if (!string.IsNullOrEmpty(input.CustomerName))
                    eLead["new_name"] = input.CustomerName;
                if (!string.IsNullOrEmpty(input.CustomerGender))
                {
                    string gender = input.CustomerGender.ToLower();
                    if (gender.Equals("male"))
                        eLead["new_gen"] = new OptionSetValue(100000001);
                    else if (gender.Equals("female"))
                        eLead["new_gen"] = new OptionSetValue(100000000);
                    else if (gender.Equals("transgender"))
                        eLead["new_gen"] = new OptionSetValue(3);
                    else
                    {
                        _status.code = "202";
                        _status.message = "Gender should be either Male,Female or Transgender";
                        return _status;
                    }
                }
                if (!string.IsNullOrEmpty(input.ServiceCategory))
                    eLead["pcl_servicecategoy"] = input.ServiceCategory;
                if (!string.IsNullOrEmpty(input.Service))
                    eLead["pcl_service"] = input.Service;
                if (!string.IsNullOrEmpty(input.SubService))
                    eLead["pcl_subservice"] = input.SubService;
                if (!string.IsNullOrEmpty(input.CampaignCode))
                    eLead["pcl_campaignname"] = input.CampaignCode;
                if (!string.IsNullOrEmpty(input.Source))
                    eLead["pcl_source"] = input.Source;
                eLead["new_phone"] = input.PhoneNo;
                eLead["new_re"] = new OptionSetValue(100000018);

                if (string.IsNullOrEmpty(input.Source) && string.IsNullOrEmpty(input.CampaignCode) && string.IsNullOrEmpty(input.Service) && string.IsNullOrEmpty(input.SubService) && string.IsNullOrEmpty(input.ServiceCategory) && string.IsNullOrEmpty(input.CustomerGender) && string.IsNullOrEmpty(input.CustomerName) && string.IsNullOrEmpty(input.CustomerEmail) && string.IsNullOrEmpty(input.CenterCode) && string.IsNullOrEmpty(input.CustomerCity))
                {
                    _status.code = "202";
                    _status.message = "No data present except Mobile";
                }
                else
                {
                    orgService.Create(eLead);
                    _status.code = "200";
                    _status.message = "SUCCESS";
                }

            }
            catch (Exception ex)
            {
                _status.code = "201";
                _status.message = ex.Message;
            }
            return _status;
        }

        public bool CheckExistingLead(string mobile, IOrganizationService orgService)
        {

            QueryExpression qeLead = new QueryExpression("new_cientenquiry1");
            qeLead.Criteria.AddCondition("new_re", ConditionOperator.Equal, 100000018); //Refrence Source : Wellness Portal
            qeLead.Criteria.AddCondition("new_phone", ConditionOperator.Equal, mobile);

            EntityCollection ecLead = orgService.RetrieveMultiple(qeLead);

            if (ecLead.Entities.Count > 0)
                return true;
            else
                return false;
        }

        public Guid getCityId(string cityname, IOrganizationService orgService)
        {
            Guid cityId = Guid.Empty;
            QueryExpression qeCity = new QueryExpression("new_city");
            qeCity.Criteria.AddCondition("new_name", ConditionOperator.Equal, cityname);
            qeCity.AddOrder("createdon", OrderType.Descending);

            EntityCollection ecCity = orgService.RetrieveMultiple(qeCity);

            if (ecCity.Entities.Count > 0)
                cityId = ecCity[0].Id;
            return cityId;
        }

        public Guid getCenterId(string centercode, IOrganizationService orgService)
        {
            Guid centerId = Guid.Empty;
            QueryExpression qeCenter = new QueryExpression("new_center");
            qeCenter.Criteria.AddCondition("new_centrecode", ConditionOperator.Equal, centercode);
            qeCenter.AddOrder("createdon", OrderType.Descending);

            EntityCollection ecCenter = orgService.RetrieveMultiple(qeCenter);

            if (ecCenter.Entities.Count > 0)
                centerId = ecCenter[0].Id;
            return centerId;
        }
    }
    public class OrgConnection
    {

    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace N11ShipmentService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.n11.com/ws/schemas", ConfigurationName="N11ShipmentService.ShipmentServicePort")]
    public interface ShipmentServicePort
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<N11ShipmentService.CreateOrUpdateShipmentTemplateResponse1> CreateOrUpdateShipmentTemplateAsync(N11ShipmentService.CreateOrUpdateShipmentTemplateRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<N11ShipmentService.GetShipmentTemplateResponse1> GetShipmentTemplateAsync(N11ShipmentService.GetShipmentTemplateRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<N11ShipmentService.GetShipmentTemplateListResponse1> GetShipmentTemplateListAsync(N11ShipmentService.GetShipmentTemplateListRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class CreateOrUpdateShipmentTemplateRequest
    {
        
        private Authentication authField;
        
        private ShipmentApiModel shipmentField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public Authentication auth
        {
            get
            {
                return this.authField;
            }
            set
            {
                this.authField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public ShipmentApiModel shipment
        {
            get
            {
                return this.shipmentField;
            }
            set
            {
                this.shipmentField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class Authentication
    {
        
        private string appKeyField;
        
        private string appSecretField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string appKey
        {
            get
            {
                return this.appKeyField;
            }
            set
            {
                this.appKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string appSecret
        {
            get
            {
                return this.appSecretField;
            }
            set
            {
                this.appSecretField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class ResultInfo
    {
        
        private string statusField;
        
        private string errorCodeField;
        
        private string errorMessageField;
        
        private string errorCategoryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string errorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public string errorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=3)]
        public string errorCategory
        {
            get
            {
                return this.errorCategoryField;
            }
            set
            {
                this.errorCategoryField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class ShipmentCompanyApiModel
    {
        
        private string nameField;
        
        private string shortNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string shortName
        {
            get
            {
                return this.shortNameField;
            }
            set
            {
                this.shortNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class CityApiModel
    {
        
        private string nameField;
        
        private string codeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer", Order=1)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class DistrictApiModel
    {
        
        private string nameField;
        
        private long idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public long id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class ShipmentSaveAddress
    {
        
        private string titleField;
        
        private string addressField;
        
        private DistrictApiModel districtField;
        
        private CityApiModel cityField;
        
        private string postalCodeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public DistrictApiModel district
        {
            get
            {
                return this.districtField;
            }
            set
            {
                this.districtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public CityApiModel city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string postalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class ShipmentApiModel
    {
        
        private string templateNameField;
        
        private string installmentInfoField;
        
        private string exchangeInfoField;
        
        private string shippingInfoField;
        
        private bool specialDeliveryField;
        
        private string deliveryFeeTypeField;
        
        private bool combinedShipmentAllowedField;
        
        private string shipmentMethodField;
        
        private ShipmentSaveAddress warehouseAddressField;
        
        private ShipmentSaveAddress exchangeAddressField;
        
        private ShipmentCompanyApiModel[] shipmentCompaniesField;
        
        private CityApiModel[] deliverableCitiesField;
        
        private ShipmentCompanyApiModel claimShipmentCompanyField;
        
        private string cargoAccountNoField;
        
        private bool useDmallCargoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string templateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string installmentInfo
        {
            get
            {
                return this.installmentInfoField;
            }
            set
            {
                this.installmentInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string exchangeInfo
        {
            get
            {
                return this.exchangeInfoField;
            }
            set
            {
                this.exchangeInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string shippingInfo
        {
            get
            {
                return this.shippingInfoField;
            }
            set
            {
                this.shippingInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public bool specialDelivery
        {
            get
            {
                return this.specialDeliveryField;
            }
            set
            {
                this.specialDeliveryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string deliveryFeeType
        {
            get
            {
                return this.deliveryFeeTypeField;
            }
            set
            {
                this.deliveryFeeTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public bool combinedShipmentAllowed
        {
            get
            {
                return this.combinedShipmentAllowedField;
            }
            set
            {
                this.combinedShipmentAllowedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string shipmentMethod
        {
            get
            {
                return this.shipmentMethodField;
            }
            set
            {
                this.shipmentMethodField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public ShipmentSaveAddress warehouseAddress
        {
            get
            {
                return this.warehouseAddressField;
            }
            set
            {
                this.warehouseAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public ShipmentSaveAddress exchangeAddress
        {
            get
            {
                return this.exchangeAddressField;
            }
            set
            {
                this.exchangeAddressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=10)]
        [System.Xml.Serialization.XmlArrayItemAttribute("shipmentCompany", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ShipmentCompanyApiModel[] shipmentCompanies
        {
            get
            {
                return this.shipmentCompaniesField;
            }
            set
            {
                this.shipmentCompaniesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=11)]
        [System.Xml.Serialization.XmlArrayItemAttribute("city", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public CityApiModel[] deliverableCities
        {
            get
            {
                return this.deliverableCitiesField;
            }
            set
            {
                this.deliverableCitiesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=12)]
        public ShipmentCompanyApiModel claimShipmentCompany
        {
            get
            {
                return this.claimShipmentCompanyField;
            }
            set
            {
                this.claimShipmentCompanyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=13)]
        public string cargoAccountNo
        {
            get
            {
                return this.cargoAccountNoField;
            }
            set
            {
                this.cargoAccountNoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=14)]
        public bool useDmallCargo
        {
            get
            {
                return this.useDmallCargoField;
            }
            set
            {
                this.useDmallCargoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class CreateOrUpdateShipmentTemplateResponse
    {
        
        private ResultInfo resultField;
        
        private ShipmentApiModel shipmentTemplateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public ResultInfo result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public ShipmentApiModel shipmentTemplate
        {
            get
            {
                return this.shipmentTemplateField;
            }
            set
            {
                this.shipmentTemplateField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateOrUpdateShipmentTemplateRequest1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11ShipmentService.CreateOrUpdateShipmentTemplateRequest CreateOrUpdateShipmentTemplateRequest;
        
        public CreateOrUpdateShipmentTemplateRequest1()
        {
        }
        
        public CreateOrUpdateShipmentTemplateRequest1(N11ShipmentService.CreateOrUpdateShipmentTemplateRequest CreateOrUpdateShipmentTemplateRequest)
        {
            this.CreateOrUpdateShipmentTemplateRequest = CreateOrUpdateShipmentTemplateRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateOrUpdateShipmentTemplateResponse1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11ShipmentService.CreateOrUpdateShipmentTemplateResponse CreateOrUpdateShipmentTemplateResponse;
        
        public CreateOrUpdateShipmentTemplateResponse1()
        {
        }
        
        public CreateOrUpdateShipmentTemplateResponse1(N11ShipmentService.CreateOrUpdateShipmentTemplateResponse CreateOrUpdateShipmentTemplateResponse)
        {
            this.CreateOrUpdateShipmentTemplateResponse = CreateOrUpdateShipmentTemplateResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class GetShipmentTemplateRequest
    {
        
        private Authentication authField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public Authentication auth
        {
            get
            {
                return this.authField;
            }
            set
            {
                this.authField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class GetShipmentTemplateResponse
    {
        
        private ResultInfo resultField;
        
        private ShipmentApiModel shipmentTemplateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public ResultInfo result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public ShipmentApiModel shipmentTemplate
        {
            get
            {
                return this.shipmentTemplateField;
            }
            set
            {
                this.shipmentTemplateField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetShipmentTemplateRequest1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11ShipmentService.GetShipmentTemplateRequest GetShipmentTemplateRequest;
        
        public GetShipmentTemplateRequest1()
        {
        }
        
        public GetShipmentTemplateRequest1(N11ShipmentService.GetShipmentTemplateRequest GetShipmentTemplateRequest)
        {
            this.GetShipmentTemplateRequest = GetShipmentTemplateRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetShipmentTemplateResponse1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11ShipmentService.GetShipmentTemplateResponse GetShipmentTemplateResponse;
        
        public GetShipmentTemplateResponse1()
        {
        }
        
        public GetShipmentTemplateResponse1(N11ShipmentService.GetShipmentTemplateResponse GetShipmentTemplateResponse)
        {
            this.GetShipmentTemplateResponse = GetShipmentTemplateResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class GetShipmentTemplateListRequest
    {
        
        private Authentication authField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public Authentication auth
        {
            get
            {
                return this.authField;
            }
            set
            {
                this.authField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class GetShipmentTemplateListResponse
    {
        
        private ResultInfo resultField;
        
        private ShipmentApiModel[] shipmentTemplatesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public ResultInfo result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("shipmentTemplate", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ShipmentApiModel[] shipmentTemplates
        {
            get
            {
                return this.shipmentTemplatesField;
            }
            set
            {
                this.shipmentTemplatesField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetShipmentTemplateListRequest1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11ShipmentService.GetShipmentTemplateListRequest GetShipmentTemplateListRequest;
        
        public GetShipmentTemplateListRequest1()
        {
        }
        
        public GetShipmentTemplateListRequest1(N11ShipmentService.GetShipmentTemplateListRequest GetShipmentTemplateListRequest)
        {
            this.GetShipmentTemplateListRequest = GetShipmentTemplateListRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetShipmentTemplateListResponse1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11ShipmentService.GetShipmentTemplateListResponse GetShipmentTemplateListResponse;
        
        public GetShipmentTemplateListResponse1()
        {
        }
        
        public GetShipmentTemplateListResponse1(N11ShipmentService.GetShipmentTemplateListResponse GetShipmentTemplateListResponse)
        {
            this.GetShipmentTemplateListResponse = GetShipmentTemplateListResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ShipmentServicePortChannel : N11ShipmentService.ShipmentServicePort, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class ShipmentServicePortClient : System.ServiceModel.ClientBase<N11ShipmentService.ShipmentServicePort>, N11ShipmentService.ShipmentServicePort
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ShipmentServicePortClient() : 
                base(ShipmentServicePortClient.GetDefaultBinding(), ShipmentServicePortClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.ShipmentServicePortSoap11.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ShipmentServicePortClient(EndpointConfiguration endpointConfiguration) : 
                base(ShipmentServicePortClient.GetBindingForEndpoint(endpointConfiguration), ShipmentServicePortClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ShipmentServicePortClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ShipmentServicePortClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ShipmentServicePortClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ShipmentServicePortClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ShipmentServicePortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<N11ShipmentService.CreateOrUpdateShipmentTemplateResponse1> N11ShipmentService.ShipmentServicePort.CreateOrUpdateShipmentTemplateAsync(N11ShipmentService.CreateOrUpdateShipmentTemplateRequest1 request)
        {
            return base.Channel.CreateOrUpdateShipmentTemplateAsync(request);
        }
        
        public System.Threading.Tasks.Task<N11ShipmentService.CreateOrUpdateShipmentTemplateResponse1> CreateOrUpdateShipmentTemplateAsync(N11ShipmentService.CreateOrUpdateShipmentTemplateRequest CreateOrUpdateShipmentTemplateRequest)
        {
            N11ShipmentService.CreateOrUpdateShipmentTemplateRequest1 inValue = new N11ShipmentService.CreateOrUpdateShipmentTemplateRequest1();
            inValue.CreateOrUpdateShipmentTemplateRequest = CreateOrUpdateShipmentTemplateRequest;
            return ((N11ShipmentService.ShipmentServicePort)(this)).CreateOrUpdateShipmentTemplateAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<N11ShipmentService.GetShipmentTemplateResponse1> N11ShipmentService.ShipmentServicePort.GetShipmentTemplateAsync(N11ShipmentService.GetShipmentTemplateRequest1 request)
        {
            return base.Channel.GetShipmentTemplateAsync(request);
        }
        
        public System.Threading.Tasks.Task<N11ShipmentService.GetShipmentTemplateResponse1> GetShipmentTemplateAsync(N11ShipmentService.GetShipmentTemplateRequest GetShipmentTemplateRequest)
        {
            N11ShipmentService.GetShipmentTemplateRequest1 inValue = new N11ShipmentService.GetShipmentTemplateRequest1();
            inValue.GetShipmentTemplateRequest = GetShipmentTemplateRequest;
            return ((N11ShipmentService.ShipmentServicePort)(this)).GetShipmentTemplateAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<N11ShipmentService.GetShipmentTemplateListResponse1> N11ShipmentService.ShipmentServicePort.GetShipmentTemplateListAsync(N11ShipmentService.GetShipmentTemplateListRequest1 request)
        {
            return base.Channel.GetShipmentTemplateListAsync(request);
        }
        
        public System.Threading.Tasks.Task<N11ShipmentService.GetShipmentTemplateListResponse1> GetShipmentTemplateListAsync(N11ShipmentService.GetShipmentTemplateListRequest GetShipmentTemplateListRequest)
        {
            N11ShipmentService.GetShipmentTemplateListRequest1 inValue = new N11ShipmentService.GetShipmentTemplateListRequest1();
            inValue.GetShipmentTemplateListRequest = GetShipmentTemplateListRequest;
            return ((N11ShipmentService.ShipmentServicePort)(this)).GetShipmentTemplateListAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ShipmentServicePortSoap11))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ShipmentServicePortSoap11))
            {
                return new System.ServiceModel.EndpointAddress("https://api.n11.com/ws/shipmentService/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ShipmentServicePortClient.GetBindingForEndpoint(EndpointConfiguration.ShipmentServicePortSoap11);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ShipmentServicePortClient.GetEndpointAddress(EndpointConfiguration.ShipmentServicePortSoap11);
        }
        
        public enum EndpointConfiguration
        {
            
            ShipmentServicePortSoap11,
        }
    }
}
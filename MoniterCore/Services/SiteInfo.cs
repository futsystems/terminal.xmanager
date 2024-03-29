﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public class ContractorInfo
    {
        public ContractorInfo(RspMGRLoginResponse response)
        {
            this.QQ = response.LoginResponse.QQ;
            this.Mobile = response.LoginResponse.Mobile;
            this.LoginID = response.LoginResponse.LoginID;
            this.Name = response.LoginResponse.Name;

        }
        public string QQ { get; set; }
        public string Mobile { get; set; }
        public string LoginID { get; set; }
        public string Name { get; set; }
    }
    public class SiteInfo
    {
        ManagerSetting _manger = null;

        RspMGRLoginResponse _response = null;

        internal RspMGRLoginResponse LoginResponse
        {
            get { return _response; }
            set { 
                _response = value;
                _info = new ContractorInfo(_response);
            }
        }

        ContractorInfo _info = null;
        public ContractorInfo ContractorInfo
        {
            get
            {
                return _info;
            }
        }
        /// <summary>
        /// 当前管理员
        /// </summary>
        public ManagerSetting Manager
        {
            get { return _manger; }
            internal set { _manger = value; }
        }

        AgentSetting _agent;
        /// <summary>
        /// 当前代理账户
        /// </summary>
        public AgentSetting Agent
        {
            get { return _agent;}
            internal set { _agent = value; }
        }


        public int MGRID
        {
            get 
            {
                if (_response != null)
                    return _response.LoginResponse.MGRID;
                return 0;
            }
        }

        public int BaseMGRFK
        {
            get
            {
                if (_response != null)
                    return _response.LoginResponse.BaseMGRFK;
                return 0;
            }
        }

        public DomainImpl Domain
        {
            get
            {
                if (_response != null)
                    return _response.LoginResponse.Domain;
                return null;
            }
        }

        public Permission Permission
        {
            get
            {
                if (_response != null)
                    return _response.LoginResponse.Permission;
                return null;
            }
        }

        /// <summary>
        /// 当前产品类别
        /// 用于生成不同的产品终端界面
        /// </summary>
        public QSEnumProductType ProductType
        {
            get
            {
                return QSEnumProductType.CounterSystem;
            }
        }

        /// <summary>
        /// 平台类型 windows或unix
        /// </summary>
        public PlatformID Platform
        {
            get
            {
                if (CoreService.TLClient.Negotiation == null)
                {
                    return PlatformID.Win32NT;
                }
                return CoreService.TLClient.Negotiation.PlatformID;
            }
        }
    }
}

﻿///* <id name="GraphDB – AdministrationServiceHost" />
// * <copyright file="AdministrationServiceHost.cs"
// *            company="sones GmbH">
// * Copyright (c) sones GmbH. All rights reserved.
// * </copyright>
// * <developer>Daniel Kirstenpfad</developer>
// * <summary>Implements the AdministrationServiceHost</summary>
// */
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ServiceModel.Description;
//using System.ServiceModel;
//using sones.Lib.Networking.TCPSocket;
//using sones.Notifications;
//using System.Threading;
//using System.Security.Cryptography.X509Certificates;
//using sones.Notifications.Autodiscovery;
//using System.Net;

//namespace sones.GraphDB.Applications.Administration
//{
//    /// <summary>
//    ///  This is a server implementation for the IGraphDBSession Admninistration wcf service interface host.
//    /// </summary>
//    public class DBAdministrationServiceHost
//    {

//        #region Definition

//        #region Fields
//        private ServiceHost _ServiceHost;
//        private Uri _BaseAddress;
//        //private NotificationDispatcher _NotificationDispatcher;
//        private Announcer _AdministrationServiceAnnouncer;
//        #endregion

//        #region Properties
//        /// <summary>
//        /// The uri on which the service host is listening
//        /// </summary>
//        public Uri BaseAddress
//        {
//            get
//            {
//                return _BaseAddress;
//            }
//        }

//        /// <summary>
//        /// The ServiceHost which handles the net connections
//        /// </summary>
//        public ServiceHost UnderlyingServiceHost
//        {
//            get
//            {
//                return _ServiceHost;
//            }
//        }
//        #endregion

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Create an administration interface using NetTcpBinding on port 8112
//        /// </summary>
//        public DBAdministrationServiceHost(/*NotificationDispatcher myNotificationDispatcher*/)
//        {
//            _BaseAddress = new Uri("net.tcp://localhost:8113");
//            //_NotificationDispatcher = myNotificationDispatcher;
//        }

//       /// <summary>
//        /// Create the administration interface for a specified Uri
//        /// </summary>
//        /// <param name="myBaseAddress">The uri including the protocol (binding), address and port </param>
//        public DBAdministrationServiceHost(/*NotificationDispatcher myNotificationDispatcher, */Uri myBaseAddress)
//        {
//            //_NotificationDispatcher = myNotificationDispatcher;
//            myBaseAddress = _BaseAddress;
//        }

//        #endregion

//        /// <summary>
//        /// Start the interface
//        /// </summary>
//        /// <param name="myIGraphDBSession">An instance of IGraphDBSession</param>
//        public void Start(IGraphDBSession myIGraphDBSession/*, String myEndpointPath*/)
//        {
//            if (myIGraphDBSession== null)
//                throw new ArgumentNullException("IGraphDBSession has to be an instance.");

//            _ServiceHost = new ServiceHost(myIGraphDBSession, _BaseAddress);

//            try
//            {
//                ServiceBehaviorAttribute serviceBehaviorAttribute = new ServiceBehaviorAttribute();
//                if (_ServiceHost.Description.Behaviors[typeof(ServiceBehaviorAttribute)] != null)
//                    serviceBehaviorAttribute = (ServiceBehaviorAttribute)_ServiceHost.Description.Behaviors[typeof(ServiceBehaviorAttribute)];
//                else
//                    _ServiceHost.Description.Behaviors.Add(serviceBehaviorAttribute);

//                serviceBehaviorAttribute.InstanceContextMode = InstanceContextMode.Single;
//                serviceBehaviorAttribute.IncludeExceptionDetailInFaults = true;

//                //_ServiceHost.Description.Behaviors[typeof(ServiceBehaviorAttribute)];
//                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
//                binding.ReceiveTimeout = TimeSpan.MaxValue;
//                binding.PortSharingEnabled = false;
//                #if(!__MonoCS__)
//                binding.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;
//                #endif

//                #region Security
//                /*
//                binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
//                _ServiceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindByIssuerName, "SONES GmbH - Mail CA");
//                _ServiceHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
//                */
//                #endregion

//                _ServiceHost.AddServiceEndpoint(typeof(IGraphDBSession), binding, _BaseAddress);
//                _ServiceHost.Open(new TimeSpan(0,1,0));

//                _AdministrationServiceAnnouncer = new Announcer(myIGraphDBSession.GetDatabaseUniqueID().ToString(), _BaseAddress, DiscoverableServiceType.Database);
//            }
//            catch (CommunicationException ce)
//            {
//                /*
//                 * If you're getting an exception while starting up this the first time you probably want to do
//                 * these things:
//                 * 
//                 * 1. Start this as local Administrator OR Add your User to the SMSvcHost.exe.config file in the
//                 *    \Windows\Microsoft.NET\Framework\v3.0\Windows Communication Foundation\ directory
//                 * 2. You want to run "sc.exe config NetTcpPortSharing start= demand" as local Administrator to
//                 *    enable the net.tcp Port Sharing.
//                 *
//                 * */
//                // TODO: Insert the above todo into the installer/wizard for final customer deployment.

//                System.Diagnostics.Debug.WriteLine(ce);
//                _ServiceHost.Abort();
//                ((IDisposable)_ServiceHost).Dispose();

//                throw ce;
//            }

//        }

//        /// <summary>
//        /// Stop the bridge and clean up the ressources
//        /// </summary>
//        public void Stop()
//        {
//            if (_AdministrationServiceAnnouncer != null)
//                _AdministrationServiceAnnouncer.StopAnnouncer();
//            if (_ServiceHost != null)
//            {
//                _ServiceHost.Close();
//                ((IDisposable)_ServiceHost).Dispose();
//            }
//        }

//    }
//}

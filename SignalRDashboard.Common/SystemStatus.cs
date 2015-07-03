using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDashboard.Common
{
    /// <summary>
    /// Holds counters for all machines in the system.
    /// </summary>
    public class SystemStatus
    {
        public SystemStatus()
        {
            // first machine in the dev environment
            Dev1 = new StatusCounter
            {
                CounterName = "DEV1",
                Maximum = 10,
                Minimum = 1,
                Value = 1,
                WarningThreshold = 7,
                CriticalThreshold = 9
            };

            // second machine in the dev environment
            Dev2 = new StatusCounter
            {
                CounterName = "DEV2",
                Maximum = 20,
                Minimum = 1,
                Value = 1,
                WarningThreshold = 15,
                CriticalThreshold = 17
            };

            // first machine in the uat environment
            UAT1 = new StatusCounter
            {
                CounterName = "UAT1",
                Maximum = 10,
                Minimum = 1,
                Value = 1,
                WarningThreshold = 7,
                CriticalThreshold = 9
            };

            // second machine in the uat environment
            UAT2 = new StatusCounter
            {
                CounterName = "UAT2",
                Maximum = 20,
                Minimum = 1,
                Value = 1,
                WarningThreshold = 15,
                CriticalThreshold = 17
            };

            // first machine in the prd environment
            Prd1 = new StatusCounter
            {
                CounterName = "PRD1",
                Maximum = 10,
                Minimum = 1,
                Value = 1,
                WarningThreshold = 7,
                CriticalThreshold = 9
            };

            // second machine in the prd environment
            Prd2 = new StatusCounter
            {
                CounterName = "PRD2",
                Maximum = 20,
                Minimum = 1,
                Value = 1,
                WarningThreshold = 15,
                CriticalThreshold = 17
            };

        }

        // first machine in the dev environment
        public StatusCounter Dev1 { get; private set; }

        // second machine in the dev environment
        public StatusCounter Dev2 { get; private set; }

        // first machine in the uat environment
        public StatusCounter UAT1 { get; private set; }

        // second machine in the uat environment
        public StatusCounter UAT2 { get; private set; }

        // first machine in the prd environment
        public StatusCounter Prd1 { get; private set; }

        // second machine in the prd environment
        public StatusCounter Prd2 { get; private set; }

        // dashboard client id - same for all machines
        public string ClientId
        {
            get
            {
                return Dev1.ClientId;
            }
            set
            {
                string clientId = value.Trim();

                Dev1.ClientId = clientId;
                Dev2.ClientId = clientId;
                UAT1.ClientId = clientId;
                UAT2.ClientId = clientId;
                Prd1.ClientId = clientId;
                Prd2.ClientId = clientId;
            }
        }
    }
}
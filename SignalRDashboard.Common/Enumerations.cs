using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDashboard.Common
{
    /// <summary>
    /// Status values used to describe the condition of a machine when certain value thresholds are reached.
    /// </summary>
    public enum Status
    {
        Unknown,
        Normal,
        Warning,
        Critical
    }
}
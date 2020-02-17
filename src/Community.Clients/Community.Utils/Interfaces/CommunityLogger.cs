﻿using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using Community.Utils;
using System.Diagnostics;
using Community.Utils.Interfaces;

[assembly:Dependency(typeof(CommunityLogger))]
namespace Community.Utils.Interfaces
{
    public class CommunityLogger : ILogger
    { 
        
        public virtual void TrackPage(string page, string id = null)
        {
            Debug.WriteLine("Conference Logger: TrackPage: " + page.ToString() + " Id: " + id ?? string.Empty);
            
        }


        public virtual void Track(string trackIdentifier)
        {
            Debug.WriteLine("Conference Logger: Track: " + trackIdentifier);

        }

        public virtual void Track(string trackIdentifier, string key, string value)
        {
            Debug.WriteLine("Conference Logger: Track: " + trackIdentifier + " key: " + key + " value: " + value);

        }
       
        public virtual void Report(Exception exception = null, Severity warningLevel = Severity.Warning)
        {
            Debug.WriteLine("Conference Logger: Report: " + exception);

        }
        public virtual void Report(Exception exception, IDictionary extraData, Severity warningLevel = Severity.Warning)
        {
            Debug.WriteLine("Conference Logger: Report: " + exception);
        }
        public virtual void Report(Exception exception, string key, string value, Severity warningLevel = Severity.Warning)
        {
            Debug.WriteLine("Conference Logger: Report: " + exception + " key: " + key + " value: " + value);
        }
    }

  
}


using System;

namespace Community.Utils.Helpers
{
   
    public static class ApiKeys
    {
        public const string AzureServiceBusName = "";
        public const string AzureServiceBusUrl = ""; 
        public const string AzureKey ="";  // TODO: take from your Azure portal

        public const string AzureHubName = "";
        public const string AzureListenConneciton = "";


        public const string GoogleSenderId =""; // TODO: take from your Google Developer Console
        public const string BingMapsUWPKey = "";
    }

    public static class EventInfo
    {
        public const string EventShortName = "LaComarca";
        public const string EventName = "LaComarcaDO";
        public const string Address1 = "";
        public const string Address2 = "";
        public const string VenueName = "";
        public const string VenuePhoneNumber = "";
        public const double Latitude = 52.340681d;
        public const double Longitude = 4.889541d;
        public const string TimeZoneName = "America/Santo_Domingo"; //https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
        public const string HashTag = "#LaComarcaDO";
        public const string WiFiSSIDDefault = "LaComarcaDO";
        public const string WiFiPassDefault = "";
        public const string WifiUrl = AboutThisApp.CdnUrl + "wifi.json";

        public const string MiniHackStaffMemberName = "Mini-Hack coach";
        public const string MiniHackUnlockTag = "LaComarcaDO";

        public const string TicketUrl = "https://lacomarca.do/events/ticket";
        public static readonly DateTime StartOfConference = new DateTime(2016, 10, 04, 6, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime EndOfConference = new DateTime(2016, 10, 05, 15, 30, 0, DateTimeKind.Utc);
    }

    public static class AboutThisApp
    {
        public const string AppLinkProtocol = "lacomarca";
        public const string PackageName = "com.lacomarca.app";
        public const string AppName = "LaComarca Events";
        public const string CompanyName = "LaComarca";
        public const string Developer = "LaComarca Community";
        public const string DeveloperWebsite = "https://lacomarca.do";
        public const string OpenSourceUrl = "https://github.com/lacomarcaDO/app-conference";
        public const string TermsOfUseUrl = "http://go.microsoft.com/fwlink/?linkid=206977";
        public const string PrivacyPolicyUrl = "http://go.microsoft.com/fwlink/?LinkId=521839";
        public const string OpenSourceNoticeUrl = "https://github.com/lacomarcaDO/app-conferencen";
        public const string EventRegistrationPage = "http://lacomarcado.eventbrite.com";
        public const string CdnUrl = ""; // TODO: set up your own CDN for static content
        public const string AppLinksBaseDomain = "www.lacomarca.do"; // TODO: use the domain name of the site you want to integrate AppLinks with
        public const string SessionsSiteSubdirectory = "Sessies";
        public const string SpeakersSiteSubdirectory = "Sprekers";
        public const string SponsorsSiteSubdirectory = "Sponsors";
        public const string MiniHacksSiteSubdirectory = "MiniHacks";
        public const string Copyright = "Copyright 2016 - LaComarca";
        public const string CodeOfConductPageTitle = "Permission to be filmed";

        public const string Credits = "";
    }

    public static class PublicationSettings
    {
        public const uint iTunesAppId = 1111111111; // Your iTunes app ID
    }

    public static class MessageKeys
    {
        public const string NavigateToEvent = "navigate_event";
        public const string NavigateToSession = "navigate_session";
        public const string NavigateToSpeaker = "navigate_speaker";
        public const string NavigateToSponsor = "navigate_sponsor";
        public const string NavigateToImage = "navigate_image";
        public const string NavigateLogin = "navigate_login";
        public const string Error = "error";
        public const string Connection = "connection";
        public const string LoggedIn = "loggedin";
        public const string Message = "message";
        public const string Question = "question";
        public const string Choice = "choice";
    }
}


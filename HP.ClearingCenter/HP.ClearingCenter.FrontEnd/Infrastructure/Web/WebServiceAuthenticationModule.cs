using HP.ClearingCenter.Application.Security.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public sealed class WebServiceAuthenticationModule : IHttpModule
    {
        private WebServiceAuthenticationEventHandler _eventHandler = null;

        public event WebServiceAuthenticationEventHandler Authenticate
        {
            add { _eventHandler += value; }
            remove { _eventHandler -= value; }
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += new EventHandler(this.OnEnter);
        }

        private void OnAuthenticate(WebServiceAuthenticationEvent e)
        {
            if (_eventHandler == null)
                return;

            _eventHandler(this, e);

            if (e.User != null)
                e.Context.User = e.Principal;
        }

        public string ModuleName
        {
            get { return "WebServiceAuthentication"; }
        }

        void OnEnter(Object source, EventArgs eventArgs)
        {   
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            Stream HttpStream = context.Request.InputStream;

            // Save the current position of stream.
            long posStream = HttpStream.Position;

            // If the request contains an HTTP_SOAPACTION 
            // header, look at this message.
            if (context.Request.ServerVariables["HTTP_SOAPACTION"] == null)
                return;

            // Load the body of the HTTP message
            // into an XML document.
            XmlDocument dom = new XmlDocument();
            string soapUser;
            string soapPassword;

            try
            {
                dom.Load(HttpStream);

                // Reset the stream position.
                HttpStream.Position = posStream;

                // Bind to the Authentication header.
                soapUser = GetSingleRequiredElementByTagName("Username", dom);
                soapPassword = GetSingleRequiredElementByTagName("ApiKey", dom);
            }
            catch (Exception e)
            {
                // Reset the position of stream.
                HttpStream.Position = posStream;

                // Throw a SOAP exception.
                XmlQualifiedName name = new XmlQualifiedName("Load");
                throw new SoapException("Unable to read SOAP request", name, e);
            }

            PerformAuthentication(context, soapUser, soapPassword);

            // Raise the custom global.asax event.
            OnAuthenticate(new WebServiceAuthenticationEvent
                         (context, soapUser, soapPassword));
        }

        private string GetSingleRequiredElementByTagName(string tagName, XmlDocument dom)
        {
            var tag = dom.GetElementsByTagName(tagName);
            if (tag == null || tag.Count == 0) return string.Empty;
            var first = tag.Item(0);

            return (first.InnerText ?? string.Empty).Trim();
        }

        private void PerformAuthentication(HttpContext context, string username, string apiKey)
        {
            var authResult = this.AuthenticateCredentials(username, apiKey);
            if (!authResult.IsSuccessful())
            {
                XmlQualifiedName name = new XmlQualifiedName("Authenticate");
                throw new SoapException(authResult.Errors.ToDelimitedValues() , name);
            }
        }

        private CommandResult AuthenticateCredentials(string username, string apiKey)
        {
            var appContext = MvcApplication.ApplicationContextFactory();

            return appContext.ExecuteCommand(new AuthenticateApiRequestCommand
            {
                Username = username, 
                ApiKey = apiKey
            });
        }
    }
}
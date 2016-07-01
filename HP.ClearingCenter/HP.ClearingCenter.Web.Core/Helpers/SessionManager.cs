using System;
using System.Web;

namespace HP.ClearingCenter.Web.Core.Helpers
{
    /// <summary>
    /// Session Manager
    /// </summary>
    public class SessionManager : IDisposable
    {
        public const string GlobalSessionKey = "GreeNova.SessionManager";

        #region SessionManager constructors...

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        protected SessionManager()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="sessionKey">The session key.</param>
        protected SessionManager(HttpSessionStateBase session, string sessionKey)
        {
            SessionProvider = session;
            SessionKey = sessionKey;
        }

        #endregion

        #region SessionManager properties...

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed { get; protected set; }

        /// <summary>
        /// Gets or sets the session key.
        /// </summary>
        /// <value>
        /// The session key.
        /// </value>
        protected string SessionKey { get; set; }

        /// <summary>
        /// Gets or sets the session provider.
        /// </summary>
        /// <value>
        /// The session provider.
        /// </value>
        protected HttpSessionStateBase SessionProvider { get; set; }

        #endregion

        #region SessionManager methods...

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public static T CreateInstance<T>(HttpSessionStateBase session)
            where T : SessionManager, new()
        {
            var sessionManager = session[GlobalSessionKey] as T;
            if (sessionManager == null)
            {
                sessionManager = new T { SessionProvider = session, SessionKey = GlobalSessionKey };
                session[GlobalSessionKey] = sessionManager;
            }
            return sessionManager;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static T CreateInstance<T>(HttpSessionStateBase session, Type type)
            where T : SessionManager, new()
        {
            string sessionKey = type.GUID + ":" + GlobalSessionKey;
            var sessionManager = session[sessionKey] as T;
            if (sessionManager == null)
            {
                sessionManager = new T { SessionKey = sessionKey, SessionProvider = session };
                session[sessionKey] = sessionManager;
            }
            return sessionManager;
        }

        /// <summary>
        /// Disposes the session.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <param name="type">The type.</param>
        public static void DisposeSession<T>(HttpSessionStateBase session, Type type)
            where T : SessionManager, new()
        {
            string sessionKey = type.GUID + ":" + GlobalSessionKey;
            var sessionManager = session[sessionKey] as T;
            if (sessionManager != null)
                session[sessionKey] = null;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                SessionProvider[SessionKey] = null;
                IsDisposed = true;
            }
        }

        #endregion
    }
}

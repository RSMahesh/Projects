using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DataBaseConnectionProvider.Interface;

namespace InfoWebTicketSystem.DAL
{
    /// <summary>
    /// Base Class for all DAL classes
    /// </summary>
    public class DALBase : System.IDisposable
    {
        /// <summary>
        /// Class variables
        /// </summary>
        public IConnection connection;
        // Track whether Connection is internal.
        private bool internalConnection = false;
        // Track whether Dispose has been called.
        private bool disposed = false;


        /// <summary>
        /// Initializes a new instance of the DALBase class with the connection object provided.
        /// </summary>
        /// <param name="connection"></param>
        public DALBase(IConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~DALBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if (internalConnection)
                    {
                        if (null != connection)
                        {
                            connection.Dispose();
                        }
                    }
                }
                // clean up unmanaged resources here.

            }
            disposed = true;
        }


        ///// <summary>
        ///// Get config setting from the configuration file.
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //protected string GetConfigSetting(string key)
        //{
        //    return ConfigurationManager.AppSettings[key];
        //}

        protected string HandleSingleQuotes(string text)
        {
            return text.Replace("'", "''");
        }

    }
}
/*---------------------------------------------------------------------------------------------------------------
 * File Name   : BaseEntity.cs
 * Purpose     : Model Class for Base Entity
 * Date Created: 16 April 2018
 * Created By  : 10xDS Team
 * 
 * History
 * ==============================================================================================================
 * Number   Date Modified   Modification Information      Modified By
 * ==============================================================================================================
 *       
 * 
 * ==============================================================================================================
 *
 */
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FloodManagementSystem.Data.Models
{
    public class BaseEntity:IDisposable
    {
        public BaseEntity() { }
        //Common properties
        public int Id { get; set; }
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
    }
}
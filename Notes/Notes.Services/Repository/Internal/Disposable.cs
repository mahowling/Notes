using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Services.Internal
{
    /// <summary>
    /// Basic Disposable class
    /// </summary>
    /// <remarks>Override the DisposeCore method to add custom Disposal code</remarks>
    public class Disposable : IDisposable
    {

        #region Internal Properies

        /// <summary>
        /// Flags whether the object has been disposed
        /// </summary>
        protected bool IsDisposed { get; private set; }
        /// <summary>
        /// Flags whther the object is currently being disposed
        /// </summary>
        /// <remarks>This helps stop circular disposal</remarks>
        protected bool IsDisposing { get; private set; }

        #endregion

        #region Destructor

        ~Disposable()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose of the object
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposing)
            {
                IsDisposed = true;
                Dispose(IsDisposing);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Dispose of the object
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                DisposeCore();
                IsDisposed = true;
            }
        }

        /// <summary>
        /// Override this method to handle custom Disposal
        /// </summary>
        protected virtual void DisposeCore()
        { }

        #endregion
    }
}

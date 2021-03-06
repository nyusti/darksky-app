﻿namespace DarkSky.Ui.Desktop.Components
{
    using System;
    using System.Threading;
    using GalaSoft.MvvmLight;
    using Unity.Attributes;

    /// <summary>
    /// Component View model base
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase"/>
    /// <seealso cref="System.IDisposable"/>
    public abstract class ComponentViewModel : ViewModelBase, IDisposable
    {
        /// <summary>
        /// Gets the cancellation token.
        /// </summary>
        /// <value>The cancellation token.</value>
        protected CancellationToken CancellationToken => this.CancellationTokenSource.Token;

        /// <summary>
        /// Gets the cancellation token source. This shuld be scoped to the view model.
        /// </summary>
        /// <value>The cancellation token source.</value>
        [Dependency]
        protected CancellationTokenSource CancellationTokenSource { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Cleanup();
            this.Dispose(true);
        }

        /// <summary>
        /// Initializes this instance. Override if the descandant wants to have initialization logic.
        /// </summary>
        [InjectionMethod]
        public virtual void Init()
        {
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.CancellationTokenSource?.IsCancellationRequested == false)
                {
                    this.CancellationTokenSource.Cancel();
                }
            }
        }
    }
}
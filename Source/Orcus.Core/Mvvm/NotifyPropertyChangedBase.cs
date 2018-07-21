using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Orcus.Core.Mvvm
{
    /// <summary>
    /// Abstraction to simplfy usage of <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// public class Example : NotifyPropertyChangedBase
    /// {
    ///     private int number;
    ///     public int Number
    ///     {
    ///         get { return number; }
    ///         set { SetField(ref number, value); }
    ///     }
    ///     
    ///     public Example()
    ///     {
    ///         PropertyChanged += ExamplePropertyChanged;
    ///     }
    /// 
    ///     private void ExamplePropertyChanged(object sender, PropertyChangedEventArgs args)
    ///     {
    ///         switch (args.PropertyName)
    ///         {
    ///             case nameof(Number):
    ///                 // Things you want to to after Number has changed.
    ///                 break;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Invoked if <see cref="SetField{T}(ref T, T, string)"/> or <see cref="OnPropertyChanged(PropertyChangedEventArgs)"/> is called.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the field and notifies listeners on the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <remarks>
        /// Ideally this is only called in the setter of a property.
        /// Does not notify listeners if <paramref name="storage"/> equals <paramref name="newValue"/>. Implement <see cref="IEqualityComparer{T}"/> to override default comparison behaviour.
        /// </remarks>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="storage">Reference to the field which will be set to <paramref name="newValue"/>.</param>
        /// <param name="newValue">Value that will be stored in <paramref name="storage"/>.</param>
        /// <param name="propertyName">[Optional] Name of the property that has changed - used to notify listeners. If <see cref="SetField{T}(ref T, T, string)"/> is called from a property setter, the name is injected for you.</param>
        /// <returns>True if <paramref name="storage"/> was changed. False if <paramref name="storage"/> equals <paramref name="value"/>.</returns>
        protected virtual bool SetField<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, newValue))
                return false;

            storage = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

            return true;
        }

        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> containing the name of the changed property.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
    }
}

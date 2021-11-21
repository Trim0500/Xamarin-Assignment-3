using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataBinding
{
    /// <summary>
    /// This class is used for the binding properties and defining how they are changed and used.
    /// 
    /// We define a few fields that each affect each other based on some kind of event. In this case it's OnPropertyChanged.
    /// We define a field "nameValue" that is set by another field "NameValue" and retrieved by getting the value of the previous field
    /// Then we define the NameValue field that will change it's properties based on what is set for nameValue
    /// Finally we define GetNameValue where all we do is get it's value via a fixed string using NameValue as it is changed
    /// </summary>
    public class Binding : INotifyPropertyChanged
    {
        //Define your first field that is to be changed
        string nameValue = null;
        //Define a field that will change it's properties using an event handler based on the first field
        public string NameValue
        {
            get => nameValue;
            set
            {
                if (value == nameValue)
                    return;

                nameValue = value;

                OnPropertyChanged(nameof(NameValue));
                OnPropertyChanged(nameof(GetNameValue));
            }

        }
        //Define one last field that has a fixed message using the NameValue field
        public string GetNameValue
        {
            get => $"You entered {NameValue}";
        }

        //Define an event handler of type PropertyChangedEventHandler to be used in the OnPropertyChanged event handler method
        public event PropertyChangedEventHandler PropertyChanged;

        //Define a method that will act as the event handler that will dynamically change the fields we use it on
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

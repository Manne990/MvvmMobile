using System;
using System.Linq.Expressions;
using System.Reflection;
using MvvmMobile.Core.ViewModel;
using UIKit;

namespace MvvmMobile.Sample.iOS.Binding
{
    public static class ViewModelExtensions
    {
        public static void SetBinding(this IBaseViewModel vm, UITableView tableView, Action updateAction)
        {
            if (vm == null)
            {
                return;
            }

            vm.PropertyChanged += (sender, e) => 
            {
                updateAction?.Invoke();
                tableView?.ReloadData();
            };
        }

        public static void SetBinding(this IBaseViewModel vm, UITextField textField, string path, bool bothWays)
        {
            if (vm == null)
            {
                return;
            }

            if (bothWays)
            {
                textField.Delegate = new TextFieldDelegate(() => 
                {
                    SetDeepPropertyValue(vm, path, textField.Text);
                });
            }

            var propertyName = path.Split('.')[0];

            vm.PropertyChanged += (sender, e) => 
            {
                if (e.PropertyName == propertyName)
                {
                    textField.Text = GetDeepPropertyValue(vm, path).ToString();
                }
            };
        }

        private static object GetDeepPropertyValue(object instance, string path)
        {
            var pp = path.Split('.');
            Type t = instance.GetType();

            foreach(var prop in pp)
            {
                var propInfo = t.GetProperty(prop);
                if (propInfo == null)
                {
                    throw new ArgumentException("Properties path is not correct");
                }

                instance = propInfo.GetValue(instance, null);
                t = propInfo.PropertyType;
            }

            return instance;
        }

        private static void SetDeepPropertyValue(object instance, string path, object value)
        {
            var pp = path.Split('.');
            Type t = instance.GetType();

            int i = 0;
            foreach(var prop in pp)
            {
                var propInfo = t.GetProperty(prop);
                if (propInfo == null)
                {
                    throw new ArgumentException("Properties path is not correct");
                }

                var newInstance = propInfo.GetValue(instance, null);
                if (i == pp.Length - 1)
                {
                    propInfo.SetValue(instance, value);
                    return;
                }

                instance = newInstance;
                t = propInfo.PropertyType;
                i++;
            }
        }

        private static bool IsValueType(object obj) 
        {
            return obj != null && obj.GetType().IsValueType;
        }
    }

    public class TextFieldDelegate : UITextFieldDelegate
    {
        private readonly Action _editingEnded;

        public TextFieldDelegate(Action editingEnded)
        {
            _editingEnded = editingEnded;
        }

        public override void EditingEnded(UITextField textField)
        {
            _editingEnded?.Invoke();
        }
    }
}
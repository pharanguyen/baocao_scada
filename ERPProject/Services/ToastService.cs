using System;

namespace ERPProject.Services
{
    public enum ToastLevel
    {
        info,
        success,
        warning,
        danger
    }

    public class ToastService
    {
        public event Action<string, string, ToastLevel> OnShow;

        

        public void ShowToast(string message, string title, ToastLevel level)
        {
            OnShow?.Invoke(message, title, level);;
        }

        public void ShowInfo(string message)
        {

            OnShow?.Invoke(message, "", ToastLevel.info);

        }

        public void ShowSuccess(string message)
        {

            OnShow?.Invoke(message, "", ToastLevel.success); ;

        }

        public void ShowDanger(string message)
        {

            OnShow?.Invoke(message, "", ToastLevel.danger); ;

        }

        public void ShowWarning(string message)
        {

            OnShow?.Invoke(message, "", ToastLevel.warning); ;

        }
    }


}

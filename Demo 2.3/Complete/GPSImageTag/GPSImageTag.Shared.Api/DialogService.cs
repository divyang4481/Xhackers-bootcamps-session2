using Acr.UserDialogs;
using GPSImageTag.Core.Interfaces;


namespace GPSImageTag
{ 
    public class DialogService: IDialogService
    {
        public void ShowLoading()
        {
            UserDialogs.Instance.ShowLoading();
        }

        public void ShowLoading(string loadingMessage)
        {
            UserDialogs.Instance.ShowLoading(loadingMessage);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        public void ShowError(string errorMessage)
        {
            UserDialogs.Instance.ShowError(errorMessage);
        }

        public void ShowSuccess(string successMessage)
        {
            UserDialogs.Instance.ShowSuccess(successMessage);
        }

        public void ShowSuccess(string successMessage, int timeOut)
        {
            UserDialogs.Instance.ShowSuccess(successMessage, timeOut);
        }

        //public void ShowConfirmation(string confirmMessage)
        //{
        //    var config = new ConfirmConfig();
        //    //config.OkText = "Ok";
        //    //config.Message = confirmMessage;
        //    //UserDialogs.Instance.Confirm(config);

        //}

    }
}

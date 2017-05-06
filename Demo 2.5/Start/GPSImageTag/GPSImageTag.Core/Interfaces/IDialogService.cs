namespace GPSImageTag.Core.Interfaces
{
    public interface IDialogService
    {
        void ShowLoading();

        void ShowLoading(string loadingMessage);

        void HideLoading();

        void ShowError(string errorMessage);

        void ShowSuccess(string successMessage);

        void ShowSuccess(string successMessage, int timeOut);

        //void ShowConfirmation(string confirmMessage);
    }
}

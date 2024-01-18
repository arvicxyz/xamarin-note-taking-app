using NoteTakingApp.ViewModels;
using NoteTakingApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace NoteTakingApp.Utilities
{
    public static class DialogPopup
    {
        public static async Task Show(string title, string message, string cancel)
        {
            DialogMessagePopup messagePopup = new DialogMessagePopup();
            var viewModel = (DialogMessagePopupViewModel)messagePopup.BindingContext;
            viewModel.TitleText = title;
            viewModel.MessageText = message;
            viewModel.CancelText = cancel;
            await PopupNavigation.Instance.PushAsync(messagePopup);
            await messagePopup.DialogClosedTask;
        }

        public static async Task<bool> Show(string title, string message, string accept, string cancel)
        {
            DialogMessagePopup messagePopup = new DialogMessagePopup();
            var viewModel = (DialogMessagePopupViewModel)messagePopup.BindingContext;
            viewModel.TitleText = title;
            viewModel.MessageText = message;
            viewModel.AcceptText = accept;
            viewModel.CancelText = cancel;
            await PopupNavigation.Instance.PushAsync(messagePopup);
            await messagePopup.DialogClosedTask;
            return viewModel?.IsAccepted ?? false;
        }
    }
}

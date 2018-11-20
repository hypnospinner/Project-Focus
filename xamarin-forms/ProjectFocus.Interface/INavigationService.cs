namespace ProjectFocus.Interface
{
    public interface INavigationService
    {
        void Navigate(PageKey target, object viewModel);

        void Back();

        void Home();
    }
}

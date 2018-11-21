namespace ProjectFocus.Interface
{
    // [Think][ToDo] Do we need to return Task to support async?
    public interface INavigationService
    {
        void Navigate(PageKey target, object viewModel);

        void Back();

        void Home();
    }
}

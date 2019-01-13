namespace ProjectFocus.Interface
{
    public interface IProblemViewModel
    {
        string Text { get; set; }

        float Importance { get; set; }

        float Urgency { get; set; }
    }
}

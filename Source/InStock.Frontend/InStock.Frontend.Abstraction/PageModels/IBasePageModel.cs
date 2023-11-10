namespace InStock.Frontend.Abstraction.PageModels
{
    public interface IBasePageModel
    {
        void Appearing(object? sender, EventArgs e);
        void Disappearing(object? sender, EventArgs e);
        Task InitializeAsync(object? navigationData = null);
    }
}
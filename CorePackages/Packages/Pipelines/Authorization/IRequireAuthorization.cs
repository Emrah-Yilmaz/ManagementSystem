namespace Packages.Pipelines.Authorization
{
    public interface IRequireAuthorization
    {
        string RequiredRole { get; }
    }

}

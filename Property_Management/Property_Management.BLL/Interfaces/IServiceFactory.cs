namespace Property_Management.BLL.Interfaces
{
    public interface IServiceFactory
    {
        T GetService<T>() where T : class;
    }
}

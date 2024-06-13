namespace AutoImport_WPF.import;

public interface IListDataImport<T>
{
    void Import(List<T> dataList);
}
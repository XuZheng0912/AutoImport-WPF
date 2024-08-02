namespace AutoImport_WPF.domain;

public interface IHealthForm
{
    string CheckDate { get; }

    string Name { get; }

    string Id { get; }

    bool IsElder { get; }
    
    
}
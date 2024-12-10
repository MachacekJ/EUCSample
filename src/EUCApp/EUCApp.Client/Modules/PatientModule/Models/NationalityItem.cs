namespace EUCApp.Client.Modules.PatientModule.Models;

public class NationalityItem(string id, string name)
{
  public string Id { get; set; } = id;
  public string Name { get; set; } = name;
}
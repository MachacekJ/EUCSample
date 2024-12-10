using System.Reflection;

namespace EUCApp.Client.Extensions;

public static class ObjectExtensions
{
  public static object? PropertyValue(this object self, string propertyName)
    => GetProperty(self, propertyName)?.GetValue(self);

  private static PropertyInfo? GetProperty(this object self, string propertyName)
    => self.GetType().GetProperty(propertyName);
}
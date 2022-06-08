using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTracker.Storage.Helpers;

public class SqlHelper
{
    /// <summary>
    /// Executes SqlScript From Embedded Resources
    /// </summary>
    /// <param name="name">Filename of the SQL-Script in SqlScriptsFolder</param>
    /// <param name="migrationBuilder"></param>
    public static void ExecuteSqlScriptFromEmbeddedResources(string name, MigrationBuilder migrationBuilder)
    {
        var unitsFillScript = GetResourceOrThrow($"{Assembly.GetCallingAssembly().GetName().Name}.Scripts.{name}", Assembly.GetExecutingAssembly());

        migrationBuilder.Sql(unitsFillScript);
    }

    /// <summary> Return content of embedded resources (File -> Properties -> Build actions -> "Embedded Resource")</summary>
    /// <param name="resourceName">"MyAssembly.MyFile.txt"</param>
    /// <param name="assembly">how to get: Assembly.GetExecutingAssembly();</param>
    /// <returns>if the resource is not found, then throw NotFoundException</returns>
    private static string GetResourceOrThrow(string resourceName, Assembly assembly)
    {
        try
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                               ?? throw new KeyNotFoundException(string.Empty);

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        catch
        {
            throw new KeyNotFoundException($"Embedded resource file \"{resourceName}\"");
        }
    }
}
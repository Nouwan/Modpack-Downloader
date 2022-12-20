// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local Ef core

namespace api;

public record Modpack(int Id, string Name)
{
    public IEnumerable<Version> Versions { get; private set; } = new List<Version>(1);
}

public record Version(int Id, int ModpackId, string Number)
{
    
    public IEnumerable<Config> Configs { get; private set; } = new List<Config>(0);
    public required Forge Forge { get; set; }
    public IEnumerable<Mod> Mods { get; private set; } = new List<Mod>(0);
}


public record Forge(int Id, int VersionId, string Url);

public record Mod(int Id, int VersionId, string Name, string Url);

public record Config(int Id, int VersionId, string Content, string Path);
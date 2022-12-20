namespace Common;

public record Version(string name, System.Version Number, Mod[] Mods,
    string Forge, Config[] Configs);
public record Mod(string Name, string Url);

public record Config(string Content, string Path);
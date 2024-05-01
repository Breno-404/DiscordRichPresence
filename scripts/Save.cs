using Godot;
using System;
using Godot.Collections;

public partial class Save : Node {

    private const string savePath = "user://presets//";
    public void SaveData(Dictionary data, int id) {
        GD.Print("Saving...");
        DirAccess dir = DirAccess.Open("user://");
        if (!dir.DirExists(savePath)) {
            dir.MakeDir(savePath);
        }
        string saveFile = savePath + "preset" + id + ".sav";
        FileAccess file = FileAccess.Open(saveFile, FileAccess.ModeFlags.Write);
        file.StoreLine(Json.Stringify(data));
        GD.Print("data saved: [" + Json.Stringify(data) + "]");
        file.Close();
    }

    public Dictionary<string, Variant> LoadData(int id) {
        Dictionary<string, Variant> data = new() {};

        string loadFile = savePath + "preset" + id + ".sav";
        
        if (FileAccess.FileExists(loadFile)) {
            FileAccess file = FileAccess.Open(loadFile, FileAccess.ModeFlags.Read);
            var jsonString = file.GetLine();
            var json = new Json();
            json.Parse(jsonString);
            data = new Dictionary<string, Variant>((Dictionary)json.Data);
            GD.Print("config loaded: [" + Json.Stringify(jsonString) + "]");
            return data;
        }

        return data;
    }
} 
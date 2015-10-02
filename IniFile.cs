using System.Runtime.InteropServices;

public class IniFile
{

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringA")]
    static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, System.Text.StringBuilder lpReturnedString, int nSize, string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA")]
    static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileIntA")]
    static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA")]
    static extern int FlushPrivateProfileString(int lpApplicationName, int lpKeyName, int lpString, string lpFileName);

    private string strFilename;

    //  Constructor, accepting a filename
    public IniFile(string Filename)
    {
        strFilename = Filename;
    }

    //  Read-only filename property
    string FileName
    {
        get
        {
            return strFilename;
        }
    }

    public string GetString(string Section, string Key, string Default)
    {
        //  Returns a string from your INI file
        int intCharCount;
        System.Text.StringBuilder objResult = new System.Text.StringBuilder(256);
        intCharCount = IniFile.GetPrivateProfileString(Section, Key, Default, objResult, objResult.Capacity, strFilename);
        if ((intCharCount > 0))
        {
            return objResult.ToString().Substring(0, intCharCount);
        }
        return null;
    }

    int GetInteger(string Section, string Key, int Default)
    {
        //  Returns an integer from your INI file
        return IniFile.GetPrivateProfileInt(Section, Key, Default, strFilename);
    }

    public bool GetBoolean(string Section, string Key, bool Default)
    {
        //  Returns a boolean from your INI file
        return (IniFile.GetPrivateProfileInt(Section, Key, (Default ? 1 : 0), strFilename) == 1);
    }

    public void WriteString(string Section, string Key, string Value)
    {
        //  Writes a string to your INI file
        IniFile.WritePrivateProfileString(Section, Key, Value, strFilename);
        this.Flush();
    }

    public void WriteInteger(string Section, string Key, int Value)
    {
        //  Writes an integer to your INI file
        this.WriteString(Section, Key, Value.ToString());
        this.Flush();
    }

    public void WriteBoolean(string Section, string Key, bool Value)
    {
        //  Writes a boolean to your INI file
        this.WriteString(Section, Key, (Value ? 1 : 0).ToString());
        this.Flush();
    }

    private void Flush()
    {
        //  Stores all the cached changes to your INI file
        IniFile.FlushPrivateProfileString(0, 0, 0, strFilename);
    }
}
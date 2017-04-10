public interface NetworkObject
{
    void Start();

    int GetID();

    string GetTableName();

    string[] GetCollumsDB();

    string[] GetValues();

    string GetIDCollumName();

    void GetUpdate(TableDecode TableContent);
}
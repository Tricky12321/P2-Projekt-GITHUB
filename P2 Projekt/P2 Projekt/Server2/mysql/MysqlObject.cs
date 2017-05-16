using System;

public abstract class MysqlObject : NetworkObject
{
    // Start metode, bliver ikke rigtigt brugt...
    public abstract void Start();

    // Henter kollonnerne for tabellen der høre til objektet
    public string[] GetCollumsDB()
    {
        return MysqlControls.SelectOne(GetTableName()).RowData[0].Colums.ToArray();
    }

    public abstract string[] GetValues();

    public abstract string[] GetValuesDB();

    public abstract int GetID();

    public abstract string GetIDCollumName();

    public abstract string GetTableName();

    public abstract void GetUpdate();

    public abstract void Update(TableDecode TableContent);

    public abstract TableDecode GetThisFromDB();

    public abstract string WhereID();

    public abstract TableDecode GetThisFromDB(string WhereCondition);

    public void UploadToDatabase()
    {
        // Hvis der allerede eksistere et objekt i databasen med dette ID, så skal det opdateres, ellers skal det oprettes.
        if (MysqlControls.IsIDInDatabase(this))
        {
            MysqlControls.UpdateWhere(GetTableName(), GetCollumsDB(), GetValues(), WhereID());
        } else
        {
            string[] Colums = MysqlControls.GetColumsFromDBRaw(GetTableName());
            string[] Values = GetValues();
            MysqlControls.Insert(GetTableName(), Colums, Values);
        }
    }
}
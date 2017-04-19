using System;

public abstract class MysqlObject : NetworkObject
{

    public abstract void Start();

    public string[] GetCollumsDB()
    {
        return MysqlControls.SelectOne(this.GetTableName()).RowData[0].Colums.ToArray();
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

    public void UploadToDatabase()
    {
        
        if (MysqlControls.IsIDInDatabase(this))
        {
            MysqlControls.UpdateWhere(GetTableName(), GetCollumsDB(), GetValues(), WhereID());
        } else
        {
            string[] Colums = MysqlControls.GetColumsFromDBRaw(GetTableName());
            string[] Values = GetValues();
            MysqlControls.Insert(GetTableName(), Colums, GetValues());
        }
    }
}
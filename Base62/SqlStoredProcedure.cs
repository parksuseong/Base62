using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

using System.Collections.Generic;
using System.Linq;
using System.Text;


public partial class StoredProcedures
{
    private static char Base62Digit(int d)
    {
        if (d < 10)
        {
            return (char)('0' + d);
        }
        else if (d < 36)
        {
            return (char)('A' + d - 10);
        }
        else if (d < 62)
        {
            return (char)('a' + d - 36);
        }
        else
        {
            throw new ArgumentException("d");
        }
    }
    public static void EncodeBase62(SqlString n)
    {
        int n2 = int.Parse(n.ToString());
        var res = "";
        while (n2 != 0)
        {
            res = Base62Digit(n2 % 62) + res;
            n2 /= 62;
        }
        SqlDataRecord record = new SqlDataRecord(new SqlMetaData("stringcol", SqlDbType.NVarChar, 128));
        record.SetSqlString(0, res);
        SqlContext.Pipe.Send(record);

    }
    private static int Base62Decode(char c)
    {
        if (c >= 'a' && c <= 'z')
        {
            return 36 + c - 'a';
        }
        else if (c >= 'A' && c <= 'Z')
        {
            return 10 + c - 'A';
        }
        else if (c >= '0' && c <= '9')
        {
            return c - '0';
        }
        else
        {
            throw new ArgumentException("c");
        }
    }
    public static void DecodeBase62(SqlString s)
    {
        SqlString res = "";
        string s2 = s.ToString();
        res = (s2.Aggregate(0, (current, c) => current * 62 + Base62Decode(c))).ToString();
        SqlDataRecord record = new SqlDataRecord(new SqlMetaData("stringcol", SqlDbType.NVarChar, 128));
        record.SetSqlString(0, res);
        SqlContext.Pipe.Send(record);

    }
}

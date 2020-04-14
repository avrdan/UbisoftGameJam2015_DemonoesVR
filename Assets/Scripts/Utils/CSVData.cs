using UnityEngine;
using System.Collections.Generic;
using System.IO;


public abstract class CSVEntry
{
    public int rowIndex;
    public abstract string TrySetDataFromRow(string[] row, int rowCount);

    protected void TryParseColumn(string[] row, int column, ref string error, out int result)
    {
        if (!int.TryParse(row[column], out result))
        {
            if (error == null)
            {
                error = "";
            }
            error += string.Format("\nCannot parse int at column {0}; row = {1}|", column, row[column]);
        }
    }
}

public class CSVTable<T> where T : CSVEntry, new()
{
    List<T> entries = new List<T>();
	public delegate void RowAddedEventHandler(string[] row, int rowIndex);


    public List<T> Entries
    {
        get
        {
            return entries;
        }
    }

    bool fileNotFound = false;

    public bool FileNotFound
    {
        get
        {
            return fileNotFound;
        }
    }

    public CSVTable(string textFile, bool skipHeader, RowAddedEventHandler onRowAdd = null, bool isResourceFile = true)
    {
        string text;
        if (isResourceFile)
        {
            TextAsset textData = (TextAsset)Resources.Load(textFile, typeof(TextAsset));
            if (textData != null)
            {
                text = textData.text;
            }
            else
            {
                fileNotFound = true;
                return;
            }
        }
        else
        {
            if (File.Exists(textFile))
            {
                StreamReader streamReader = new StreamReader(textFile);
                text = streamReader.ReadToEnd();
                streamReader.Close();
            }
            else
            {
                fileNotFound = true;
                return;
            }
        }

        using (StringReader readFile = new StringReader(text))
        {
            string line;
            string[] row;

            if (skipHeader)
            {
                line = readFile.ReadLine();//skip header
            }
            int rowCount = 0;

            char[] splitVar = { '|' };


            while ((line = readFile.ReadLine()) != null)
            {
                row = line.Split(splitVar, System.StringSplitOptions.None);

                T entry = new T();
                string columnError = entry.TrySetDataFromRow(row, rowCount);
                if (columnError == null)
                {
                    entries.Add(entry);
					if(onRowAdd != null)
					{
						onRowAdd(row, rowCount);
					}
                }
                else
                {
                    Debug.Log(textFile + ": ignoring row " + line + " since it does not have propper format at row " + rowCount + " errors:\n" + columnError);
                }

                rowCount++;

            }

            readFile.Close();
        }


    }

}
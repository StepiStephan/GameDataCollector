using DataClasses;
using DataManaging.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvTeDataManagingst
{
    public class CsvParser: IExternalWishListPaser
    {
        public TableClass ParseCsv(string path)
        {
            var result = new TableClass();
            var content = File.ReadAllLines(path);

            foreach(var line in content)
            {
                
                var values = line.Split(';');

                if (values.First() == "Gesamtsumme")
                    break;

                if(line == content.First())
                {
                    result.TableName = values[0];
                }
                else
                {
                    var entryCount = values.Where(x => x != string.Empty).Count();
                    if (entryCount != 0)
                    {
                        if(values.Where(x => float.TryParse(x, out float val)).Count() == 0 && 
                            entryCount == values.Count())
                        {
                            result.ColumnCaptions.AddRange(values);
                            result.ColumnCaptions.RemoveAt(result.ColumnCaptions.Count-1);
                        }
                        else
                        { 
                            int index = 0;
                            result.Content.Names.Add(values[index].Trim());
                            index++;

                            if (values.Length >= 7)
                            {
                                result.Content.ConsoleType.Add(values[index]);
                                index ++;
                            }
                            else
                                result.Content.ConsoleType.Add(string.Empty);

                            result.Content.ReleaseDate.Add(DateTime.TryParse(values[index], out DateTime date) ? date : new DateTime());
                            index++;

                            if (values[index] != string.Empty)
                                result.Content.Pice1.Add(float.TryParse(values[index].Substring(0, values[index].Length - 1), out float val) ? val : 0);
                            else
                                result.Content.Pice1.Add(0);
                            index++;

                            if (values[index] != string.Empty)
                                result.Content.Pice2.Add(float.TryParse(values[index].Substring(0, values[index].Length - 1), out float val) ? val : 0);
                            else
                                result.Content.Pice2.Add(0);
                            index++;

                            if (values[index] != string.Empty)
                                result.Content.Pice3.Add(float.TryParse(values[index].Substring(0, values[index].Length - 1), out float val) ? val : 0);
                            else 
                                result.Content.Pice3.Add(0);
                            index++;

                            if (values.Length >= 6)
                                result.Content.Günstigster.Add(float.TryParse(values[index].Substring(0, values[index].Length - 1), out float cheap) ? cheap : 0);
                            else
                                result.Content.Günstigster.Add(0);


                        }
                    }
                }
            }

            return result;
        }

        public void ParseTable(TableClass table, string path)
        {
            var text = new StringBuilder();

            text.AppendLine(table.TableName);

            foreach(var colName in table.ColumnCaptions)
            {
                text.Append(colName + ";");
            }
            text.Remove(text.Length - 1, 1);
            text.Append(Environment.NewLine);

            for(int i = 0; i < table.Content.Names.Count; i++)
            {
                text.Append(table.Content.Names[i] + ";");
                text.Append(table.Content.ConsoleType[i] + ";");
                text.Append(table.Content.ReleaseDate[i].ToString() + ";");
                text.Append(table.Content.Pice1[i] + ";");
                text.Append(table.Content.Pice2[i] + ";");
                text.Append(table.Content.Pice3[i] + ";");
                text.Append(table.Content.Günstigster[i]);
                text.Append(Environment.NewLine);
            }

            File.WriteAllText(path, text.ToString());
        }
    }
}

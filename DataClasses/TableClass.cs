using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class TableClass
    {
        public string TableName { get; set; }
        public List<string> ColumnCaptions{ get; set; } = new List<string>();
        public TableContent Content { get; set; } = new TableContent();
        public override string ToString()
        {
            var text = new StringBuilder();
            text.AppendLine(TableName);

            foreach (var column in ColumnCaptions)
            { 
                text.Append(column.ToString());
                text.Append("\t");
            }
            text.Append(Environment.NewLine);

            for (int i = 0; i < Content.Names.Count; i++)
            {
                var line = new StringBuilder();
                
                line.Append(Content.Names[i]);
                line.Append("\t");
                line.Append(Content.ReleaseDate[i].ToString("dd-MM-yyyy"));
                line.Append("\t");
                line.Append(Content.Pice1[i]);
                line.Append("\t");
                line.Append(Content.Pice2[i]);
                line.Append("\t");
                line.Append(Content.Pice3[i]);
                line.Append("\t");

                text.AppendLine(line.ToString());
            }

            return text.ToString();
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAutoFramework.Helpers
{
    public class HtmlTableHelper
    {
        private static List<TableDataCollection> _tableDataCollection;

        public static void ReadTable(IWebElement table)
        {
            //initialize the table
            _tableDataCollection = new List<TableDataCollection>();

            //get all the columns from the table
            var columns = table.FindElements(By.TagName("th"));

            //get all the rows
            var rows = table.FindElements(By.TagName("tr"));

            //create row index
            int rowIndex = 0;
            foreach (var row in rows)
            {
                int colIndex = 0;

                var colDatas = row.FindElements(By.TagName("td"));

                //store data only if it has value in row
                if (colDatas.Count != 0)
                {
                    foreach (var colValue in colDatas)
                    {
                        _tableDataCollection.Add(new TableDataCollection
                        {
                            RowNumber = rowIndex,
                            ColumnName = columns[colIndex].Text != "" ?
                                            columns[colIndex].Text : colIndex.ToString(),
                            ColumnValue = colValue.Text,
                            columnSpecialValue = GetControl(colValue);
                        });
                    //move to next column
                    colIndex++;
                    }
                rowIndex++;
                }
            }

        private static ColumnSpecialValue GetControl(IWebElement colValue)
        {
            ColumnSpecialValue columnSpecialValue = null;

            //check if the control has specfic tags like input/hyperlink 
            if (colValue.FindElements(By.TagName("a")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = colValue.FindElements(By.TagName("a")),
                    ControlType = "hyperLink"
                };
            }
            if (colValue.FindElements(By.TagName("input")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = colValue.FindElements(By.TagName("a")),
                    ControlType = "input"
                };
            }
            return columnSpecialValue;
        }

        public static void PerformActionOnCell(string columnIndex, string refColumnName,string refColumnValue,string controlToOperate= null)
        {
            foreach (int rowNumber in GetDynamicRowNumber(refColumnName,refColumnValue))
            {
                var cell = (from e in _tableDataCollection
                            where e.ColumnName == columnIndex && e.RowNumber == rowNumber
                            select e.columnSpecialValue).SingleOrDefault();


                if (controlToOperate != null && cell != null)
                {
                    if (cell.ControlType == "hyperLink")
                    {
                        var returnedControl = (from c in cell.ElementCollection
                                               where c.Text == controlToOperate
                                               select c).SingleOrDefault();
                        //ToDo: Currenly only click suported, future is not take care here
                        returnedControl?.Click();
                    }
                    if (cell.ControlType == "input")
                    {
                        var returnedControl = (from c in cell.ElementCollection
                                               where c.GetAttribute("value") == controlToOperate
                                               select c).SingleOrDefault();
                        returnedControl?.Click();
                    }
                }
                else
                {
                    //sta znaci ovaj upitnik ->?<- posle collection
                    cell.ElementCollection?.First().Click();
                }
            }
        }

        private static IEnumerable GetDynamicRowNumber(string columnName, string columnValue)
        {
            //dynamyc row
            foreach (var table in _tableDataCollection)
            {
                if (table.ColumnName == columnName && table.ColumnValue == columnName)
                {
                    yield return table.RowNumber;
                }
            }
        }

    }


    }

    public class TableDataCollection
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public ColumnSpecialValue columnSpecialValue { get; set; }
    }

    public class ColumnSpecialValue
    {
        public IEnumerable<IWebElement> ElementCollection { get; set; }
        public string ControlType { get; set; }
    }
    

}

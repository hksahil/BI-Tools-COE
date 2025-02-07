// Tabular Editor 101

// Measure Creation
var measure = Model.Tables["Sales"].AddMeasure(
    "Total Sales", 
    "SUM(Sales[SalesAmount])", 
    "Financial Metrics"
);

// Measure Formatting
measure.FormatString = "$#,0.00";
measure.Description = "Total Sales Revenue";
measure.DataType = DataType.Currency;
measure.IsHidden = false;

// Looping
/*Looping Through Tables*/ 
foreach (var table in Model.Tables)
{ 
    Info(table.Name);
}
Looping Through Columns in a Table
foreach (var column in Model.Tables["Sales"].Columns)
{
    Info(column.Name + " - " + column.DataType);
}
Looping Through Measures in some table
foreach (var measure in Model.Tables["Sales"].Measures)
{
    Info("Measure: " + measure.Name + " | DAX: " + measure.Expression);
}

4️⃣ Looping Through All Measures in all tables
foreach (var table in Model.Tables)
{
    foreach (var measure in table.Measures)
    {
        Info(measure.Name + " is in " + table.Name);
    }
}   

🔹 Use Case: Print all relationships between tables.

foreach (var rel in Model.Relationships)
{
    Info("Relationship between " + rel.FromTable.Name + " and " + rel.ToTable.Name);
}


// Bulk Explicit Measure Creation of Selected measures [Selected on left panel of Tabular Editor]
for each (var c in Selected.Columns)
{
    var newMeasure=c.Table.AddMeasure(
        "Sum of"+c.Name, // Name
        "Sum("+c.DaxObjectFullName+")", //Dax Expression
        c.DisplayFolder // Display Folder
    );

    newMeasure.Formattring="$0.00";

    newMeasure.Descrition="This measure is sum of column"+c.DaxObjectFullName;
}


// Bulk Renaming Measures [eg, Prefixing Sales_ to each measure name]
foreach(var x in Model.AllMeasures) 
{ 
    x.Name = "Sales_" + x.Name; 
}

// Bulk Rename Measures [eg, replace space by underscores in all measure names] 
foreach(var x in Model.AllMeasures)
{
    x.Name = x.Name.Replace(" ", "_");
}

// Bulk DAX change [eg, whereever we have SUM, replace it by SUMX]
foreach(var x in Model.AllMeasures)
{
    if (x.Expression.Contains("SUM("))
    {
        x.Expression = x.Expression.Replace("SUM(", "SUMX(");
    }
}

// Bulk change Table referemce [eg, Sales table to Fact Sales Table]
foreach(var x in Model.AllMeasures)
{
    x.Expression = x.Expression.Replace("Sales[", "FactSales[");
}

// Get the list of measures containing certain function [eg, CALCULATE in this example ]
foreach(var x in Model.AllMeasures.Where(mea => mea.Expression.Contains("CALCULATE")))
{
    System.Console.WriteLine(x.Name + " contains CALCULATE.");
}

// Bulk Measure Formatting [eg, add two decimals with currency symbol in these measures]
foreach(var x in Model.AllMeasures)
{
    if (x.Name.Contains("Sales") || x.Name.Contains("Revenue") || x.Name.Contains("Profit"))
    {
        x.FormatString = "$#,##0.00";
    }
}
// Bulk Measure Formatting [eg, add percentage symbols in Margin, Rate, Ration, Percentage]
foreach(var m in Model.AllMeasures)
{
    if (m.Name.Contains("Margin") || m.Name.Contains("Rate") || m.Name.Contains("Ratio") || m.Name.Contains("Percentage"))
    {
        m.FormatString = "0.00%";
    }
}

// Bulk Measure Formatting [eg, add two decimals to specific measures]
foreach(var m in Model.AllMeasures)
{
    if (m.Name.Contains("Average") || m.Name.Contains("Per Unit") || m.Name.Contains("Cost"))
    {
        m.FormatString = "#,##0.00";
    }
}

// Dynamic Measure formatting [ decimal measures to have two decimals, integers to have no decimal ]
foreach(var m in Model.AllMeasures)
{
    if (m.DataType == DataType.Decimal)
    {
        m.FormatString = "#,##0.00";
    }
    else if (m.DataType == DataType.Integer)
    {
        m.FormatString = "#,##0";
    }
}

// Dynamic Measure formatting [ apply different formats depending on the measure name and type. ]
foreach(var m in Model.AllMeasures)
{
    if (m.Name.Contains("Sales") || m.Name.Contains("Revenue"))
        m.FormatString = "$#,##0.00";  // Currency Format
    else if (m.Name.Contains("Rate") || m.Name.Contains("Margin"))
        m.FormatString = "0.00%";  // Percentage Format
    else if (m.Name.Contains("Time") || m.Name.Contains("Duration"))
        m.FormatString = "hh:mm:ss";  // Time Format
    else
        m.FormatString = "#,##0";  // Default Integer Format
}

// Bulk measures movement [ eg, copy measures fro Sales table to Finance Table]
var sourceTable = Model.Tables["Sales"];
var destinationTable = Model.Tables["Finance"];

foreach(var m in sourceTable.Measures.ToList())
{
    var newMeasure = destinationTable.AddMeasure(m.Name, m.Expression, m.DisplayFolder);
    newMeasure.FormatString = m.FormatString;
}`

// Mass duplicate measures with certian changes
foreach(var m in Model.AllMeasures.ToList())
{
    var newMeasure = m.Table.AddMeasure(m.Name + "_Adj", "(" + m.DaxObjectFullName + ") * 1.10", m.DisplayFolder);
    newMeasure.FormatString = m.FormatString;
}

// Mass Hiding [ eg, Hide all ID columns from model]
foreach (var table in Model.Tables)
{
    foreach (var column in table.Columns)
    {
        if (column.Name.EndsWith("ID"))
        {
            column.IsHidden = true;
        }
    }
}

// Mass Measure Creation [eg, you allready have list of what measures to create, you want to create them with formatting asw]
var table = Model.Tables["Table Name"];
var measures=new[]

{
    new { Name = "Sales", Formula="SUM(Sales)" },
    new { Name = "Cost", Formula="SUM(Cost)" },
    new { Name = "Profit", Formula="SUM(Profit)" },
    new { Name = "Discount", Formula="SUM(Discount)" }
};

foreach (var measure in measures)
{
    var newMeasure=table.AddMeasure(measure.Name, measure.Formula);
    newMeasure.FormatString="0.00";
    newMeasure.Description="This measure is"+measure.Name;
}


// Mass Measure creation [eg, Create YTD Calcs for every numeric column]
foreach (var col in table.Columns.Where(c => c.DataType == DataType.Decimal))
{
    var measure = table.AddMeasure(col.Name + "_YTD", "TOTALYTD(SUM(" + col.DaxObjectFullName + "), 'Calendar'[Date])", col.DisplayFolder);
    measure.FormatString = "$#,##0.00"; // Adjust based on data type
}

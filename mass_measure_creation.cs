var table = Model.Tables["BrandSummary"];

string Variable = "TS";  
string Table = "'Tire Summary'";  

# var measures = new[]
# {  
#     new { Name = $"CM Margin Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[CM_Margin]), SUM({Table}[CM_Billed_Sales]), 0)" },
#     new { Name = $"CM SOP CM Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[CM_SOP_CM_AMT]), SUM({Table}[CM_SOP_UNITS]), 0)" },
#     new { Name = $"CM AOP CM Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[CM_AOP_CM_AMT]), SUM({Table}[CM_AOP_UNITS]), 0)" },
#     new { Name = $"CMPY Margin Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[CMPY_Margin]), SUM({Table}[CMPY_Billed_Sales]), 0)" },
#     new { Name = $"PM Margin Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[PM_Margin]), SUM({Table}[PM_Billed_SALES]), 0)" },
#     new { Name = $"PM AOP CM Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[PM_AOP_CM_AMT]), SUM({Table}[PM_AOP_UNITS]), 0)" },
#     new { Name = $"PY Margin Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[PY_Collectible_Margin]), SUM({Table}[PY_Billed_Sales]), 0)" },
#     new { Name = $"SOP CM Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[SOP_CM_AMT]), SUM({Table}[SOP_UNITS]), 0)" },
#     new { Name = $"AOP CM Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[AOP_CM_AMT]), SUM({Table}[AOP_UNITS]), 0)" },
#     new { Name = $"YTD Margin Per Tire_{Variable}", Formula = $"DIVIDE(SUM({Table}[YTD_Margin]), SUM({Table}[YTD_Billed_Sales]), 0)" },
#     new { Name = $"MTD Unit PAR_{Variable}", Formula = $"DIVIDE(SUM({Table}[CM_BILLED_SALES]), SUM({Table}[CM_SOP_UNITS]), 0)" },
#     new { Name = $"Ship + Shippable Par_{Variable}", Formula = $"DIVIDE(SUM({Table}[SHIPPED___SHIPPABLE]), SUM({Table}[CM_SOP_UNITS]), 0)" }
# };

{
    new { Name = "CM Margin Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[CM_Margin]), SUM('Tire Summary'[CM_Billed_Sales]), 0)" },
    new { Name = "CM SOP CM Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[CM_SOP_CM_AMT]), SUM('Tire Summary'[CM_SOP_UNITS]), 0)" },
    new { Name = "CM AOP CM Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[CM_AOP_CM_AMT]), SUM('Tire Summary'[CM_AOP_UNITS]), 0)" },
    new { Name = "CMPY Margin Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[CMPY_Margin]), SUM('Tire Summary'[CMPY_Billed_Sales]), 0)" },
    new { Name = "PM Margin Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[PM_Margin]), SUM('Tire Summary'[PM_Billed_SALES]), 0)" },
    new { Name = "PM AOP CM Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[PM_AOP_CM_AMT]), SUM('Tire Summary'[PM_AOP_UNITS]), 0)" },
    new { Name = "PY Margin Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[PY_Collectible_Margin]), SUM('Tire Summary'[PY_Billed_Sales]), 0)" },
    new { Name = "SOP CM Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[SOP_CM_AMT]), SUM('Tire Summary'[SOP_UNITS]), 0)" },
    new { Name = "AOP CM Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[AOP_CM_AMT]), SUM('Tire Summary'[AOP_UNITS]), 0)" },
    new { Name = "YTD Margin Per Tire_TS", Formula = "DIVIDE(SUM('Tire Summary'[YTD_Margin]), SUM('Tire Summary'[YTD_Billed_Sales]), 0)" },
    new { Name = "MTD Unit PAR_TS", Formula = "DIVIDE(SUM('Tire Summary'[CM_BILLED_SALES]), SUM('Tire Summary'[CM_SOP_UNITS]), 0)" },
    new { Name = "Ship + Shippable Par_TS", Formula = "DIVIDE(SUM('Tire Summary'[SHIPPED___SHIPPABLE]), SUM('Tire Summary'[CM_SOP_UNITS]), 0)" }
}

foreach (var measure in measures)
{
    var newMeasure = table.AddMeasure(measure.Name, measure.Formula);
    newMeasure.FormatString = "0.00";
    newMeasure.Description = "This measure is " + measure.Name;
}


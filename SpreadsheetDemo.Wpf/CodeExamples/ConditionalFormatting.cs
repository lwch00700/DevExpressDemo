using System;
using System.Drawing;
using DevExpress.Spreadsheet;
using System.Collections.Generic;
using Formatting = DevExpress.Spreadsheet.Formatting;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class ConditionalFormatting {
        static void AddAverageConditionalFormatting(IWorkbook workbook) {
            #region #AverageConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingCollection conditionalFormattings = worksheet.ConditionalFormattings;
            AverageConditionalFormatting cfRule1 = conditionalFormattings.AddAverageConditionalFormatting(worksheet["$D$5:$D$18"], ConditionalFormattingAverageCondition.AboveOrEqual);
            cfRule1.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            cfRule1.Formatting.Font.Color = Color.Red;
            AverageConditionalFormatting cfRule2 = conditionalFormattings.AddAverageConditionalFormatting(worksheet["$E$5:$E$18"], ConditionalFormattingAverageCondition.BelowOrEqual, 1);
            cfRule2.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0x9F, 0xFB, 0x69);
            cfRule2.Formatting.Font.Color = Color.BlueViolet;
            worksheet["B2"].Value = "In the report below determine cost values that are above the average in the first quarter and one standard deviation below the average in the second quarter.";
            worksheet.Visible = true;
            #endregion #AverageConditionalFormatting
        }

        static void AddRangeConditionalFormatting(IWorkbook workbook) {
            #region #RangeConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            RangeConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddRangeConditionalFormatting(worksheet["$G$5:$G$18"], ConditionalFormattingRangeCondition.Outside, "7", "19");
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            cfRule.Formatting.Font.Color = Color.Red;
            worksheet["B2"].Value = "In the report below identify price values that are less than $7 and greater than $19.";
            worksheet.Visible = true;
            #endregion #RangeConditionalFormatting
        }

        static void AddRankConditionalFormatting(IWorkbook workbook) {
            #region #RankConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            RankConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddRankConditionalFormatting(worksheet["$G$5:$G$18"], ConditionalFormattingRankCondition.TopByRank, 3);
            cfRule.Formatting.Fill.BackgroundColor = Color.DarkOrchid;
            cfRule.Formatting.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
            cfRule.Formatting.Font.Color = Color.White;
            worksheet["B2"].Value = "In the report below identify the top three price values.";
            worksheet.Visible = true;
            #endregion #RankConditionalFormatting
        }

        static void AddTextConditionalFormatting(IWorkbook workbook) {
            #region #TextConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            TextConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddTextConditionalFormatting(worksheet["$B$5:$B$18"], ConditionalFormattingTextCondition.Contains, "Bradbury");
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xE1, 0x95, 0xC2);
            worksheet["B2"].Value = "Quickly find books written by Ray Bradbury in the report below.";
            worksheet.Visible = true;
            #endregion #TextConditionalFormatting
        }


        static void AddSpecialConditionalFormatting(IWorkbook workbook) {
            #region #SpecialConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            SpecialConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddSpecialConditionalFormatting(worksheet["$B$5:$B$18"], ConditionalFormattingSpecialCondition.ContainUniqueValue);
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            worksheet["B2"].Value = "Quickly identify unique values in the list of authors.";
            worksheet.Visible = true;
            #endregion #SpecialConditionalFormatting
        }


        static void AddTimePeriodConditionalFormatting(IWorkbook workbook) {
            #region #TimePeriodConditionalFormatting
            workbook.Calculate();
            Worksheet worksheet = workbook.Worksheets["cfTasks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            TimePeriodConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddTimePeriodConditionalFormatting(worksheet["$C$5:$C$9"], ConditionalFormattingTimePeriod.Today);
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xF2, 0xAE, 0xE3);
            worksheet["B2"].Value = "Determine the today's task in the TO DO list.";
            worksheet.Visible = true;
            #endregion #TimePeriodConditionalFormatting
        }


        static void AddExpressionConditionalFormatting(IWorkbook workbook) {
            #region #ExpressionConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ExpressionConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddExpressionConditionalFormatting(worksheet["$G$5:$G$18"], ConditionalFormattingExpressionCondition.GreaterThan, "=AVERAGE($G$5:$G$18)");
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xFA, 0xF7, 0xAA);
            cfRule.Formatting.Font.Color = Color.Red;
            worksheet["B2"].Value = "In the report below identify price values that are above the average.";
            worksheet.Visible = true;
            #endregion #ExpressionConditionalFormatting
        }


        static void AddFormulaExpressionConditionalFormatting(IWorkbook workbook) {
            #region #FormulaExpressionConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            FormulaExpressionConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddFormulaExpressionConditionalFormatting(worksheet.Range["$B$5:$H$18"], "=MOD(ROW(),2)=1");
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xBC, 0xDA, 0xF7);
            worksheet["B2"].Value = "Shade alternate rows in light blue without applying a new style.";
            worksheet.Visible = true;
            #endregion #FormulaExpressionConditionalFormatting
        }


        static void AddColorScale2ConditionalFormatting(IWorkbook workbook) {
            #region #ColorScale2ConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingValue minPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "0");
            ConditionalFormattingValue maxPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "100");
            ColorScale2ConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddColorScale2ConditionalFormatting(worksheet.Range["$D$5:$E$18"], minPoint, Color.FromArgb(255, 0x9D, 0xE9, 0xFA), maxPoint, Color.FromArgb(255, 0xFF, 0xF6, 0xA9));
            worksheet["B2"].Value = "Examine cost distribution using a gradation of two colors. Blue represents the lower values and yellow represents the higher values.";
            worksheet.Visible = true;
            #endregion #ColorScale2ConditionalFormatting
        }


        static void AddColorScale2ConditionalFormatting_Extremum(IWorkbook workbook) {
            #region #ColorScale2ConditionalFormatting_Extremum
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingValue minPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            ConditionalFormattingValue maxPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            ColorScale2ConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddColorScale2ConditionalFormatting(worksheet.Range["$D$5:$E$18"], minPoint, Color.FromArgb(255, 0x9D, 0xE9, 0xFA), maxPoint, Color.FromArgb(255, 0xFF, 0xF6, 0xA9));
            worksheet["B2"].Value = "Examine cost distribution using a gradation of two colors. Blue represents the lower values and yellow represents the higher values.";
            worksheet.Visible = true;
            #endregion #ColorScale2ConditionalFormatting_Extremum
        }


        static void AddColorScale3ConditionalFormatting(IWorkbook workbook) {
            #region #ColorScale3ConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingValue minPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Formula, "=MIN($E$5:$F$18)");
            ConditionalFormattingValue midPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percentile, "50");
            ConditionalFormattingValue maxPoint = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Number, "=MAX($E$5:$F$18)");
            ColorScale3ConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddColorScale3ConditionalFormatting(worksheet.Range["$D$5:$E$18"], minPoint, Color.Red, midPoint, Color.Yellow, maxPoint, Color.SkyBlue);
            worksheet["B2"].Value = "Examine cost distribution using a gradation of three colors. Red represents the lower values, yellow represents the medium values and sky blue represents the higher values.";
            worksheet.Visible = true;
            #endregion #ColorScale3ConditionalFormatting
        }

        static void AddDataBarConditionalFormatting(IWorkbook workbook) {
            #region #DataBarConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingValue lowBound1 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            ConditionalFormattingValue highBound1 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            DataBarConditionalFormatting cfRule1 = worksheet.ConditionalFormattings.AddDataBarConditionalFormatting(worksheet.Range["$F$5:$F$18"], lowBound1, highBound1, Color.Green);
            cfRule1.BorderColor = Color.Green;
            cfRule1.NegativeBarColor = Color.Red;
            cfRule1.NegativeBarBorderColor = Color.Red;
            cfRule1.AxisPosition = ConditionalFormattingDataBarAxisPosition.Middle;
            cfRule1.AxisColor = Color.DarkBlue;
            ConditionalFormattingValue lowBound2 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "0");
            ConditionalFormattingValue highBound2 = worksheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Percent, "100");
            DataBarConditionalFormatting cfRule2 = worksheet.ConditionalFormattings.AddDataBarConditionalFormatting(worksheet.Range["$H$5:$H$18"], lowBound2, highBound2, Color.SkyBlue);
            cfRule2.BorderColor = Color.SkyBlue;
            cfRule2.GradientFill = false;
            cfRule2.ShowValue = false;
            worksheet["B2"].Value = "Compare values in the \"Cost Trend\" and \"Markup\" columns using data bars.";
            worksheet.Visible = true;
            #endregion #DataBarConditionalFormatting
        }

        static void AddIconSetConditionalFormatting(IWorkbook workbook) {
            #region #IconSetConditionalFormatting
            Worksheet worksheet = workbook.Worksheets["cfBooks"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            ConditionalFormattingIconSetValue minPoint = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($F$5:$F$18)", ConditionalFormattingValueOperator.GreaterOrEqual);
            ConditionalFormattingIconSetValue midPoint = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual);
            ConditionalFormattingIconSetValue maxPoint = worksheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.01", ConditionalFormattingValueOperator.GreaterOrEqual);
            IconSetConditionalFormatting cfRule = worksheet.ConditionalFormattings.AddIconSetConditionalFormatting(worksheet.Range["$F$5:$F$18"], IconSetType.Arrows3, new ConditionalFormattingIconSetValue[] { minPoint, midPoint, maxPoint });
            cfRule.IsCustom = true;
            ConditionalFormattingCustomIcon cfCustomIcon = new ConditionalFormattingCustomIcon();
            cfCustomIcon.IconSet = IconSetType.TrafficLights13;
            cfCustomIcon.IconIndex = 1;
            cfRule.SetCustomIcon(1, cfCustomIcon);
            cfRule.ShowValue = false;
            worksheet["B2"].Value = "In the report below identify upward and downward cost trends.";
            worksheet.Visible = true;
            #endregion #IconSetConditionalFormatting
        }
    }
}

using System.Drawing;
using DevExpress.Spreadsheet;

namespace DevExpress.XtraSpreadsheet.Demos {
    public static class TopTradingPartners {
        public static void ApplyTopImportsConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            RankConditionalFormatting cfRule2 = conditionalFormattings.AddRankConditionalFormatting(sheet["Table[Imports]"], ConditionalFormattingRankCondition.TopByRank, 5);
            cfRule2.Formatting.Fill.BackgroundColor = Color.FromArgb(0xFC, 0xB3, 0xB3);
        }
        public static void ApplyImportsYearlyChangeConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ExpressionConditionalFormatting cfRule =
            conditionalFormattings.AddExpressionConditionalFormatting(sheet["Table[Imports 1Y Chg]"], ConditionalFormattingExpressionCondition.GreaterThan, "0");
            cfRule.Formatting.Font.Color = Color.Green;
            ExpressionConditionalFormatting cfRule2 =
            conditionalFormattings.AddExpressionConditionalFormatting(sheet["Table[Imports 1Y Chg]"], ConditionalFormattingExpressionCondition.LessThan, "0");
            cfRule2.Formatting.Font.Color = Color.Red;
        }
        public static void ApplyTopExportsConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            RankConditionalFormatting cfRule1 = conditionalFormattings.AddRankConditionalFormatting(sheet["Table[Exports]"], ConditionalFormattingRankCondition.TopByRank, 5);
            cfRule1.Formatting.Fill.BackgroundColor = Color.FromArgb(0xAD, 0xF7, 0xC3);
        }
        public static void ApplyExportsYearlyChangeConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ExpressionConditionalFormatting cfRule =
            conditionalFormattings.AddExpressionConditionalFormatting(sheet["Table[Exports 1Y Chg]"], ConditionalFormattingExpressionCondition.GreaterThan, "0");
            cfRule.Formatting.Font.Color = Color.Green;
            ExpressionConditionalFormatting cfRule2 =
            conditionalFormattings.AddExpressionConditionalFormatting(sheet["Table[Exports 1Y Chg]"], ConditionalFormattingExpressionCondition.LessThan, "0");
            cfRule2.Formatting.Font.Color = Color.Red;
        }


        public static void ApplyAsiaCountriesConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            FormulaExpressionConditionalFormatting cfRule = conditionalFormattings.AddFormulaExpressionConditionalFormatting(sheet["Table[Country]"], "=$B6=\"Asia\"");
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 0xBC, 0xDA, 0xF7);
        }

        public static void ApplyBalanceChangeConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ConditionalFormattingValue lowBound1 = sheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto);
            ConditionalFormattingValue highBound1 = sheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto);
            DataBarConditionalFormatting cfRule1 = conditionalFormattings.AddDataBarConditionalFormatting(sheet["Table[Balance 1Y Chg]"], lowBound1, highBound1, Color.FromArgb(0x9E, 0xAD, 0xFF));
            cfRule1.BorderColor = Color.FromArgb(0x9E, 0xAD, 0xFF);
            cfRule1.NegativeBarColor = Color.FromArgb(0xFF, 0x9E, 0xE7);
            cfRule1.NegativeBarBorderColor = Color.FromArgb(0xFF, 0x9E, 0xE7);
            cfRule1.AxisPosition = ConditionalFormattingDataBarAxisPosition.Middle;
            cfRule1.AxisColor = Color.Black;
        }
        public static void ApplyBalanceTrendConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ConditionalFormattingIconSetValue minPoint = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($F$6:$F$22)", ConditionalFormattingValueOperator.GreaterOrEqual);
            ConditionalFormattingIconSetValue midPoint = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual);
            ConditionalFormattingIconSetValue maxPoint = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.0001", ConditionalFormattingValueOperator.GreaterOrEqual);
            IconSetConditionalFormatting cfRule = conditionalFormattings.AddIconSetConditionalFormatting(sheet["Table[Balance]"], IconSetType.Triangles3, new ConditionalFormattingIconSetValue[] { minPoint, midPoint, maxPoint });
        }
    }
}

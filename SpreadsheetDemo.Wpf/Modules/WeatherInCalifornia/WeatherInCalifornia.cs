using System;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace DevExpress.XtraSpreadsheet.Demos {
    public static class WeatherInCalifornia {
        public static void ApplyTemperatureConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ConditionalFormattingValue minPoint = conditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            ConditionalFormattingValue midPoint = conditionalFormattings.CreateValue(ConditionalFormattingValueType.Percentile, "50");
            ConditionalFormattingValue maxPoint = conditionalFormattings.CreateValue(ConditionalFormattingValueType.MinMax);
            conditionalFormattings.AddColorScale3ConditionalFormatting(sheet.Range["$C$4:$C$60"], minPoint, Color.FromArgb(255, 0x65, 0x92, 0xAF), midPoint, Color.FromArgb(255, 0xF2, 0xA1, 0x6A), maxPoint, Color.FromArgb(255, 0xFF, 0xD5, 0x55));
            ExpressionConditionalFormatting cfRule = conditionalFormattings.AddExpressionConditionalFormatting(sheet.Range["$C$4:$C$60"], ConditionalFormattingExpressionCondition.GreaterThan, "40");
            cfRule.Formatting.Font.Color = Color.White;
        }
        public static void ApplyHumidityConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ConditionalFormattingValue lowBound = conditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto);
            ConditionalFormattingValue highBound = conditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto);
            DataBarConditionalFormatting cfRule = conditionalFormattings.AddDataBarConditionalFormatting(sheet.Range["$E$4:$E$60"], lowBound, highBound, Color.FromArgb(255, 0xD6, 0xD6, 0xD6));
            cfRule.GradientFill = false;
        }
        public static void ApplyPressureConditionalFormatting(Worksheet sheet) {
            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            ConditionalFormattingIconSetValue minPoint = conditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "-1", ConditionalFormattingValueOperator.GreaterOrEqual);
            ConditionalFormattingIconSetValue midPoint = conditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual);
            ConditionalFormattingIconSetValue maxPoint = conditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "1", ConditionalFormattingValueOperator.GreaterOrEqual);
            IconSetConditionalFormatting cfRule = conditionalFormattings.AddIconSetConditionalFormatting(sheet.Range["$I$4:$I$60"], IconSetType.Triangles3, new ConditionalFormattingIconSetValue[] { minPoint, midPoint, maxPoint });
            cfRule.ShowValue = false;
        }
   }
}

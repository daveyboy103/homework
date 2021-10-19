using System;
using DataModel;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = new Request();
            var dimension = request.AddDimension(new Dimension{DimensionId = "11"});
            
            dimension.AddColumn("report_name");
            dimension.AddColumn("capitalunit");
            dimension.AddColumn("capitalunit");
            dimension.AddColumn("desk");
            dimension.AddColumn("fundreference");
            dimension.AddColumn("location");
            dimension.AddColumn("strat");
            dimension.AddColumn("strategy_category");
            dimension.AddColumn("strategy_subcategory");
            dimension.AddColumn("strategy_description");
            dimension.AddColumn("strategy_owner");
            dimension.AddColumn("tradequerysource");
            dimension.AddColumn("ccy");
            dimension.AddColumn("value.date");
            dimension.AddColumn("deskgroupe");
            dimension.AddColumn("capitalunit.isclosed");

            var row = dimension.AddRow(new Row() { DimensionId = "11" });

            row.AddDimension("All MT Dynamic PNL_fu_RMBASIS");
            row.AddDimension("RM BASIS");
            row.AddDimension("RM Basis");
            row.AddDimension("Management Trading");
            row.AddDimension("BC69");
            row.AddDimension("JERSEY CHANNEL ISLANDS");
            row.AddDimension("QRBUS");
            row.AddDimension("Drics");
            row.AddDimension(string.Empty);
            row.AddDimension(string.Empty);
            row.AddDimension("RM - Janis Drics");
            row.AddDimension("Existing");
            row.AddDimension("USD");
            row.AddDimension( "29/09/2021 11:50:00");
            row.AddDimension( "RM");
            row.AddDimension( "0");
            
            row = dimension.AddRow(new Row() { DimensionId = "12" });

            row.AddDimension("All MT Dynamic PNL_fu_RMBASIS");
            row.AddDimension("RM BASIS");
            row.AddDimension("RM Basis");
            row.AddDimension("Management Trading");
            row.AddDimension("BC69");
            row.AddDimension("JERSEY CHANNEL ISLANDS");
            row.AddDimension("QRBAU");
            row.AddDimension("QRBAU");
            row.AddDimension(string.Empty);
            row.AddDimension(string.Empty);
            row.AddDimension("RM - Cedric Levard");
            row.AddDimension("Existing");
            row.AddDimension("AUD");
            row.AddDimension( "29/09/2021 11:50:00");
            row.AddDimension( "RM");
            row.AddDimension( "0");

            var measure = new Measure();
            measure.AddKeyColumn("key");
            measure.AddValueColumn("value");
            request.AddMeasure(measure);

            var measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("deltaallparallel");
            measureRow.AddMeasure(-348745.85337790177m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("fx.usd");
            measureRow.AddMeasure(12m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl");
            measureRow.AddMeasure(-61152.874213245959m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl.comp.date");
            measureRow.AddMeasure(-208.50935653183842m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl.comp.rate");
            measureRow.AddMeasure(-106368.38167959411m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl.comp.rate.usd");
            measureRow.AddMeasure(-106368.38167959411m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl.comp.rate.spread");
            measureRow.AddMeasure(43972.388902395091m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pv");
            measureRow.AddMeasure(-122488.36675690848m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pv.t0");
            measureRow.AddMeasure(-122488.36675690848m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("size");
            measureRow.AddMeasure(12881.0m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl.comp.vol");
            measureRow.AddMeasure(1451.6279204848979m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("pnl.comp.vol.vol");
            measureRow.AddMeasure(1451.6279204848979m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "11"});
            measureRow.AddKey("vega.norm");
            measureRow.AddMeasure(-5336.3565863760014m);
            // Dimension 12
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("deltaallparallel");
            measureRow.AddMeasure(-54365.083254043479m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("fx.usd");
            measureRow.AddMeasure(0.724m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("gammaallparallel");
            measureRow.AddMeasure(2.6793425708776337m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pnl");
            measureRow.AddMeasure(21.415525617543608m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pnl.comp.fx.aud");
            measureRow.AddMeasure(21.415525617514504m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pnl.comp.rate");
            measureRow.AddMeasure(-208.50935653183842m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pnl.comp.rate");
            measureRow.AddMeasure(32934.58816668803m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pnl.comp.rate.aud");
            measureRow.AddMeasure(32934.58816668803m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pnl.comp.spread");
            measureRow.AddMeasure(-32934.58816668803m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pv");
            measureRow.AddMeasure(-155048.40547085978m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("pv.t0");
            measureRow.AddMeasure(-155048.40547085978m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("size");
            measureRow.AddMeasure(3050.0m);
            measureRow = measure.AddRow(new MeasureRow{ DimensionId = "12"});
            measureRow.AddKey("deltaallparallel");
            measureRow.AddMeasure(382418.375242873090m);


            Console.ReadKey();
        }
    }
}
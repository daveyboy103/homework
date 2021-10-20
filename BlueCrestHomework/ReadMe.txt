
QueryResult
{
               
                RequestId:string;
                Dimensions
                {
                                Columns:string[];
                                Rows:[
                                {
                                                DimensionId:string;
                                                Dimensions:string[];
                                }]
                }
                Measures
                {
                                KeyColumns:string[];
                               ValueColumns:string[];
                               Rows:[
                                {
                                                DimensionId:string;
                               Keys:string[];
                               Measures:double[];
                                }]
                }
}
 
Dimension columns are the names of the metadata fields for the Dimension rows array, for instance, if columns are:
"fund","desk","strategy"
 
then rows could be:
"bc1","rates","QRMLX01"
"bc1","credit","QCDLX01"
 
Measure KeyColumns in this case are always "key", and value columns are "value". They are tied to the dimensions array via the DimensionId field. For example, dimension id "1" can have the following measure rows:
Keys:["pnl"], Measures[-20]
Keys:["pv"], Measures[10]
Keys:["pvt0"], Measures[11]
1. Implement method startQuery() in class QueryService so that it reads the data file and returns an 
observable of objects following the format described above.
 
2. Read the results of that call in QueryPage, and show a grid containing 4 columns, 
namely Fund, Desk, Strategy and Pnl, where Pnl is the sum of all measure rows with key "pnl" for the 
dimension Id corresponding to the fund, desk and strategy that are displayed.
 
3. (Bonus) Add columns for as many pnl subcomponents as you wish (pnl.comp.rate, pnl.comp.spread, etc). 
If all subcomponents are added, their sum should tie up to the pnl total.
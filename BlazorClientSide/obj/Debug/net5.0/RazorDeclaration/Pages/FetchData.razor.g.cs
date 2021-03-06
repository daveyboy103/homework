// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorClientSide.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using BlazorClientSide;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/_Imports.razor"
using BlazorClientSide.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/Pages/FetchData.razor"
using DataModel.Dtos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/Pages/FetchData.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/Pages/FetchData.razor"
using System.Collections.ObjectModel;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/fetchdata")]
    public partial class FetchData : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 83 "/Users/davidharrington/Documents/GitHub/homework/BlazorClientSide/Pages/FetchData.razor"
       
    private ObservableCollection<RowMeasureItem> pnlData;
    private IEnumerable<RowMeasureItem> _data;
    private string _buttonCaption = "Show Details";
    private int _counter = 0;

    private bool _showDetails = false;

    protected override async Task OnInitializedAsync()
    {
         _data = await Http.GetFromJsonAsync<RowMeasureItem[]>("https://localhost:5001/api/Query/query/all/enumerable");
        if (_data != null) pnlData = new ObservableCollection<RowMeasureItem>(_data.Select(x => x).Where(x => x.Key == "pnl"));
    }
    

    private double TotalPnl()
    {
        return pnlData.Where(x => x.Key == "pnl").Sum(x => x.Value);
    }

   
    private void ReloadPage()
    {
        if (!_showDetails)
        {
            pnlData = new ObservableCollection<RowMeasureItem>(_data.Select(x => x).Where(x => ContainsTwoDots(x.Key)));
            _showDetails = true;
            _buttonCaption = "Hide Details";
        }
        else
        {
            pnlData = new ObservableCollection<RowMeasureItem>(_data.Select(x => x).Where(x => x.Key == "pnl"));
            _showDetails = false;
            _buttonCaption = "Show Details";
        }
    }
    
    private static bool ContainsTwoDots(string stringToTest)
    {
        if (stringToTest == "pnl") return true;
        if (!stringToTest.StartsWith("pnl.")) return false;
        var dotCount = stringToTest.Count(c => c == '.');
        return dotCount <= 2;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591

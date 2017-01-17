$(document).ready(function () {
   
    CanvasJS.addColorSet("Shades",
               [//colorSet Array

               "#a6061c",
               "#008080",
           
               ]);
    var chart = new CanvasJS.Chart("chartContainer",
	{
	    colorSet: "Shades",
	    //title: {
	    //    text: "PSV STATUS",
	    //    fontFamily: "arial blue"
	    //},
	    animationEnabled: true,
	    legend: {
	        verticalAlign: "bottom",
	        horizontalAlign: "center"
	    },
	    theme: "theme1",
	    data: [
		{
		    type: "pie",
		    indexLabelFontFamily: "Garamond",
		    indexLabelFontSize: 15,
		    indexLabelFontWeight: "bold",
		    startAngle: 0,
		    indexLabelFontColor: "MistyRose",
		    indexLabelLineColor: "darkgrey",
		    indexLabelPlacement: "inside",
		    toolTipContent: "{name}: {y}%",
		    showInLegend: true,
		    indexLabel: "#percent%",
		    dataPoints: [
				{ y: 25, name: "Complete", legendMarkerType: "square" },
				{ y: 75, name: "Incomplete", legendMarkerType: "square" },
		    ]
		}
	    ]
       
	});
    chart.render();
    RemoveLabel();
    function RemoveLabel() {
        $('.canvasjs-chart-credit').addClass('hidden');

    }
});

//$(".PSVtabs").off('click', '.list-item').on('click', '.list-item', function (e) {
//        e.preventDefault();
//        var url = $(this).data().tabPath;
//        var theID = $(this).data().tabContainer;
//        var theDataVal = $(this).parent().data().val;
//        theTempId = theID;
//        //TabManager.getDynamicContent(url, "ProviderProfileTabView", MethodName, providerData);
//        //TabManager.getDynamicContent(url, theID, MethodName, '');
//        //ScrollForInitialLoadProfile(url, theDataVal, theID);

//        $(this).parent().addClass("current");
//        $(this).parent().siblings().removeClass("current");

//        setTimeout(function () {
//            $('.memberProfileTabsContainer').find("[data-tab-container=" + theTempId + "]").parent().addClass("current");
//            $('.memberProfileTabsContainer').find("[data-tab-container=" + theTempId + "]").parent().siblings().removeClass("current");
//            var innerTabName = $('#ProviderProfile').find('ul:first').children('.current').text();
//            $('#ProfileSelectedTabName').text(innerTabName);
//        }, 50);
//    });
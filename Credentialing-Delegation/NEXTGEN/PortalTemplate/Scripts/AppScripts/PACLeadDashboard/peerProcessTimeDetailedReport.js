xleftPos = $('#DetailedViewOfPPT').width() - 200;
var PPTDR = c3.generate({
    bindto: '#DetailedViewOfPPT',
    data: {
        x: 'x',
        columns: [
            ['x', 'Yasmine', 'Dana', 'Tara', 'Stephanie', 'Rachael', 'Leah'],
            ['Highest', 30, 99, 20, 30, 60, 80],
            ['Average', 30, 99, 99, 20, 15, 50],
                        ['Lowest', 13, 10, 10, 20, 10, 50]
        ],
        labels: true,
        type: 'bar',
        colors: {
            Highest: '#10c0a3',
            Average: '#6e8586',
            Lowest: '#0095c6'
        },

    },
    legend: {
        show: true,
        position: 'inset',
        inset: {
            anchor: 'top-left',
            x: xleftPos
        }
    },
    axis: {
        x: {
            type: 'category', // this needed to load string x value                
            tick: {
                rotate: -45,
                multiline: false
            },
            height: 50
        },
    }
});

function getData(DataOfThis) {

    var data1 = {
        "names": ['Brandon','Denise','Mark','Deborah','Jeffrey','Harold','Emily','Dennis','Ralph','Jason','Tammy','Nancy','Paula','Brian','Jack'],
        "Highest": ['Highest', 95, 88, 83, 75, 73, 66, 62, 57, 53, 47, 41, 37, 34, 30, 9],
        "Average": ['Average', 89, 68, 08, 58, 33, 79, 16, 5, 25, 44, 14, 9, 15, 8, 5],
        "Lowest": ['Lowest', 15, 30, 65, 45, 35, 34, 33, 32, 25, 4, 1, 9, 0, 8, 1]
    };
    var data2 = {

        "names": ['Denise','Norma','Barbara','Tammy','Joe','Patricia','Sara','Evelyn','Evelyn','Kathryn','Irene','Christina','Louise','Jason','Kenneth'],
        "Highest": ['Highest', 98, 86, 80, 78, 71, 69, 61, 55, 52, 47, 41, 39, 30, 28, 9],
        "Average": ['Average', 93, 85, 81, 76, 73, 66, 60, 55, 50, 48, 40, 39, 31, 26, 18],
        "Lowest": ['Lowest', 15, 30, 65, 45, 35, 34, 33, 32, 25, 4, 1, 9, 0, 8, 1]

    }
    switch (DataOfThis) {
        case "data1": return data1;
            break;
        case "data2": return data2;
            break;

    }
    return data1;
}

function loadPptData(GetThisData) {
    var result = getData(GetThisData);
    var names = result.names;
    var high = result.Highest;
    var avg = result.Average;
    var low = result.Lowest;
   
    var trTag = "<tr>"
    var trTagEnd = "</tr>"
    var thTag = "<th class='col-lg-1'>"
    var thEndTag = "</th>"
    var tdTag = "<td class='col-lg-1'>";
    var tdTagEnd = "</td>"
    $('#DRPPTtable_Header').append(thTag + 'Type' + thEndTag)
    for (var i = 0; i < names.length; i++) {
        $('#DRPPTtable_Header').append(thTag + names[i] + thEndTag)
    }
    for (var i = 0; i < 16; i++) {
        $('#Highest').append(tdTag + high[i] + tdTagEnd)
    }
    for (var i = 0; i < 16; i++) {
        $('#Average').append(tdTag + avg[i] + tdTagEnd)
    }
    for (var i = 0; i < 16; i++) {
        $('#Lowest').append(tdTag + low[i] + tdTagEnd)
    }


    names.unshift('x');
    PPTDR.load({
        columns: [names, high, avg,low],
    });




}
loadPptData("data1");


$('#showPrevious').click(function () {
    $('#DRPPTtable_Header').html('');
    $('#Highest').html('');
    $('#Average').html('');
    $('#Lowest').html('');
    var data = loadPptData("data1");

})


$('#showNext').click(function () {
    $('#DRPPTtable_Header').html('');
    $('#Highest').html('');
    $('#Average').html('');
    $('#Lowest').html('');
    var data = loadPptData("data2");
})

$('.width_3').height($(window).height())
$('[data-toggle="tooltip"]').tooltip();

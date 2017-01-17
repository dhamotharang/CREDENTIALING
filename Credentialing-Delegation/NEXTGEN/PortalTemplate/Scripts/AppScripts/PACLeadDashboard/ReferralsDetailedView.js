var cat = ['Yasmine', 'Dana', 'Tara', 'Stephanie', 'Rachael', 'Leah', 'Ana', 'Andrea', 'Jaiane'];
xleftPos = $('#DetailedViewOfReferral').width() - 200
var referralDR = c3.generate({
    bindto: '#DetailedViewOfReferral',
    data: {
        x: 'x',
        columns: [
            ['x', 'go', 'go', 'go', 'go', 'go', 'go'],
            ['Standard', 30, 200, 200, 400, 150, 250],
            ['Expedite', 130, 100, 100, 200, 150, 50]
        ],
        labels: true,
        type: 'bar',
        groups: [
            ['Standard', 'Expedite']
        ],
        colors: {
            Standard: '#10c0a3',
            Expedite: '#6e8586'
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
        "names": ['Richard', 'Frances', 'John', 'Debra', 'Lawrence', 'Barbara', 'Benjamin', 'Alice', 'Kimberly', 'Wayne', 'Sharon', 'Stephanie', 'Helen', 'Rachel', 'Sara'],
        "Standard": ['Standard', 91, 82, 70, 60, 58, 50, 46, 40, 39, 31, 34, 20, 18, 15, 7, 1],
        "Expedite": ['Expedite', 93, 84, 77, 66, 58, 51, 48, 40, 39, 31, 26, 29, 16, 11, 8, 2]
    };
    var data2 = {

        "names": ['Ann', 'Sharon', 'Jose', 'Walter', 'Dorothy', 'Angela', 'Anna', 'Pamela', 'Ryan', 'Benjamin', 'Helen', 'Thomas', 'Christine', 'Stephen', 'Harold'],
        "Standard": ['Standard', 99, 82, 72, 66, 55, 57, 47, 42, 37, 36, 34, 21, 19, 13, 5, 3],
        "Expedite": ['Expedite', 98, 85, 76, 64, 55, 54, 47, 40, 39, 30, 33, 31, 19, 14, 8, 4]

    }
    switch (DataOfThis) {
        case "data1": return data1;
            break;
        case "data2": return data2;
            break;

    }
    return data1;
}

function loadReferralData(GetThisData) {
    var result = getData(GetThisData);
    var names = result.names;
    var stadard = result.Standard;
    var expedite = result.Expedite;
    var trTag = "<tr>"
    var trTagEnd = "</tr>"
    var thTag = "<th class='col-lg-1'>"
    var thEndTag = "</th>"
    var tdTag = "<td class='col-lg-1'>";
    var tdTagEnd = "</td>"
    $('#DRReferraltable_Header').append(thTag + 'Type' + thEndTag)
    for (var i = 0; i < names.length; i++) {
        $('#DRReferraltable_Header').append(thTag + names[i] + thEndTag)
    }
    for (var i = 0; i < 16; i++) {
        $('#standardRow').append(tdTag + stadard[i] + tdTagEnd)
    }
    for (var i = 0; i < 16; i++) {
        $('#expediteRow').append(tdTag + expedite[i] + tdTagEnd)
    }


    names.unshift('x');
    referralDR.load({
        columns: [names, stadard, expedite],
    });




}
loadReferralData("data1");


$('#showPrevious').click(function () {
    $('#DRReferraltable_Header').html('');
    $('#standardRow').html('');
    $('#expediteRow').html('');
    var data = loadReferralData("data1");

})


$('#showNext').click(function () {
    $('#DRReferraltable_Header').html('');
    $('#standardRow').html('');
    $('#expediteRow').html('');
    var data = loadReferralData("data2");
})

$('.width_3').height($(window).height())
$('[data-toggle="tooltip"]').tooltip();

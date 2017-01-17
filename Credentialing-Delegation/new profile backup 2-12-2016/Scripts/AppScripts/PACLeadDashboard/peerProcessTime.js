$(document).ready(function () {
    xleftPos = $('#PeerProcessTimeBarGraph').width() - 200;

    var ppt = c3.generate({
        bindto: '#PeerProcessTimeBarGraph',
        data: {
            x: 'x',
            columns: [
               ['x', 'Tara', 'Jalane', 'Ana', 'Yasmine', 'Rachael', 'Leah', 'Stephanie', 'Andrea', 'Dana', 'Jaiane'],
                ['Highest', 93, 76,59,50,46,43,40,32,29,30],
                ['Average', 10,19,29,28,36,23,32,31,28,20],
                            ['Lowest', 0,0,8,3,8,7,15,7,6,1]
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

});

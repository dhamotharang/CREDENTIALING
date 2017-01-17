$(document).ready(function () {
    xleftPos = $('#ReferralBarGraph').width() - 200
    var referral = c3.generate({
        bindto: '#ReferralBarGraph', 
        data: {
            x: 'x',
            columns: [
                ['x', 'Yasmine', 'Dana', 'Tara', 'Stephanie', 'Rachael', 'Leah', 'Ana', 'Andrea', 'Jaiane','Joe'],
                ['Standard', 40, 30,20,12,9,18,15,9,8,6],
                ['Expedite', 49,50,42,41,39,28,27,49,36,15]
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
});
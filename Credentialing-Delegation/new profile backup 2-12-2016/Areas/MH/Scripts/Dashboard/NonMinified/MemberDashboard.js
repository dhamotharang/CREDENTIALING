var chart = c3.generate({
    bindto: '#chart1',
    data: {
        columns: [
            ['Enrolled', 200, 250, 250, 300, 200, 250],
            ['Disenrolled', 130, 100, 100, 200, 150, 50]
        ],
        type: 'bar',
        groups: [['Enrolled'], ['Disenrolled']]
    },
    color: {
        pattern: ['#008000', '#FF0000']
    },
    axis: {
        x: {
            label: {
                text: 'Members Ratio',
                position: 'outer-center',
            },
            type: 'category',
            categories: ['2012', '2013', '2014', '2015', '2016', '2017'],
        },
    }

});

var chart2 = c3.generate({
    bindto: '#chart2',
    data: {
        x: 'x',
        columns:
        [
            ['x', 'Dr. Pariksith Singh', 'Dr. Manjushree', 'Dr. Lex Harris', 'Dr. Maria Scunziano', 'Dr. Gaurav Mathur'],
            ['Enrolled Members', 4000, 3000, 2500, 3000, 4000]
        ],
        type: 'bar'
    },
    color: {
        pattern: ['#4682B4']
    },
    size: {
        width: 500
    },
    axis: {
        rotated: true,
        x: {
            label: {
                text: 'Providers',
                position: 'outer-middle',
            },
            type: 'category'
        }
    }

});
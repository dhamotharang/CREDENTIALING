jQuery(document).ready(function () {

    try {
        //Easy Pie Chart
        $('.easypiechart#returningvisits').easyPieChart({
            barColor: "#85c744",
            trackColor: '#edeef0',
            scaleColor: '#d2d3d6',
            scaleLength: 5,
            lineCap: 'square',
            lineWidth: 2,
            size: 90,
            onStep: function (from, to, percent) {
                $(this.el).find('.percent').text(Math.round(percent));
            }
        });

        $('.easypiechart#newvisitor').easyPieChart({
            barColor: "#f39c12",
            trackColor: '#edeef0',
            scaleColor: '#d2d3d6',
            scaleLength: 5,
            lineCap: 'square',
            lineWidth: 2,
            size: 90,
            onStep: function (from, to, percent) {
                $(this.el).find('.percent').text(Math.round(percent));
            }
        });

        $('.easypiechart#clickrate').easyPieChart({
            barColor: "#e73c3c",
            trackColor: '#edeef0',
            scaleColor: '#d2d3d6',
            scaleLength: 5,
            lineCap: 'square',
            lineWidth: 2,
            size: 90,
            onStep: function (from, to, percent) {
                $(this.el).find('.percent').text(Math.round(percent));
            }
        });

    }
    catch (e) { }

    $("#indexvisits").sparkline([7914 + randValue(), 2795 + randValue(), 3256 + randValue(), 3018 + randValue(), 2832 + randValue(), 5261 + randValue(), 6573 + randValue()], {
        lineWidth: 1.5,
        type: 'line',
        width: '90px',
        height: '45px',
        lineColor: '#556b8d',
        fillColor: 'rgba(85,107,141,0.1)',
        spotColor: false,
        minSpotColor: false,
        highlightLineColor: '#d2d3d6',
        highlightSpotColor: '#556b8d',
        spotRadius: 3,
        maxSpotColor: false
    });

    $("#indexpageviews").sparkline([8263 + randValue(), 6314 + randValue(), 10467 + randValue(), 12123 + randValue(), 11125 + randValue(), 13414 + randValue(), 15519 + randValue()], {
        lineWidth: 1.5,
        type: 'line',
        width: '90px',
        height: '45px',
        lineColor: '#4f8edc',
        fillColor: 'rgba(79,142,220,0.1)',
        spotColor: false,
        minSpotColor: false,
        highlightLineColor: '#d2d3d6',
        highlightSpotColor: '#4f8edc',
        spotRadius: 3,
        maxSpotColor: false
    });

    $("#indexpagesvisit").sparkline([7.41 + randValue(), 6.12 + randValue(), 6.8 + randValue(), 5.21 + randValue(), 6.15 + randValue(), 7.14 + randValue(), 6.19 + randValue()], {
        lineWidth: 1.5,
        type: 'line',
        width: '90px',
        height: '45px',
        lineColor: '#a6b0c2',
        fillColor: 'rgba(166,176,194,0.1)',
        spotColor: false,
        minSpotColor: false,
        highlightLineColor: '#d2d3d6',
        highlightSpotColor: '#a6b0c2',
        spotRadius: 3,
        maxSpotColor: false
    });

    $("#indexavgvisit").sparkline([5.31 + randValue(), 2.18 + randValue(), 1.06 + randValue(), 3.42 + randValue(), 2.51 + randValue(), 1.45 + randValue(), 4.01 + randValue()], {
        lineWidth: 1.5,
        type: 'line',
        width: '90px',
        height: '45px',
        lineColor: '#85c744',
        fillColor: 'rgba(133,199,68,0.1)',
        spotColor: false,
        minSpotColor: false,
        highlightLineColor: '#d2d3d6',
        highlightSpotColor: '#85c744',
        spotRadius: 3,
        maxSpotColor: false
    });

    $("#indexnewvisits").sparkline([70.14 + randValue(), 72.95 + randValue(), 77.56 + randValue(), 78.18 + randValue(), 76.32 + randValue(), 73.61 + randValue(), 74.73 + randValue()], {
        lineWidth: 1.5,
        type: 'line',
        width: '90px',
        height: '45px',
        lineColor: '#efa131',
        fillColor: 'rgba(239,161,49,0.1)',
        spotColor: false,
        minSpotColor: false,
        highlightLineColor: '#d2d3d6',
        highlightSpotColor: '#efa131',
        spotRadius: 3,
        maxSpotColor: false
    });

    $("#indexbouncerate").sparkline([29.14 + randValue(), 27.95 + randValue(), 32.56 + randValue(), 30.18 + randValue(), 28.32 + randValue(), 32.61 + randValue(), 35.73 + randValue()], {
        lineWidth: 1.5,
        type: 'line',
        width: '90px',
        height: '45px',
        lineColor: '#e74c3c',
        fillColor: 'rgba(231,76,60,0.1)',
        spotColor: false,
        minSpotColor: false,
        highlightLineColor: '#d2d3d6',
        highlightSpotColor: '#e74c3c',
        spotRadius: 3,
        maxSpotColor: false
    });

    function randValue() {
        return (Math.floor(Math.random() * (2)));
    }
});
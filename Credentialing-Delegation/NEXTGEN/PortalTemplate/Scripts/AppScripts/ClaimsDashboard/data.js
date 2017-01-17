
var IsMeanProcessTime = true;
var IsHighCostClaims = true;
var IsSpanTime = true;
var IsProceduresClaimed = true;
var IsPenaltyAdjustment = true;

function filterChartsWithAjaxdummy(filterId, chartId) {

    if (chartId == '#MeanProcessTimeReport') {
        if (IsMeanProcessTime) {
            MeanTimeChart.load({
                columns: [
                    ['x', '6040 Anne Lee', '9918 Ann Ray', '9685 Carl Fields', '7055 Janet Boyd', '6483 Stephen Freeman'],
                    ['Previous Year', 60, 200, 56, 12, 78],
                    ['Current Year', 75, 31, 26, 90, 109]
                ]
            });
        } else {
            MeanTimeChart.load({
                columns: [
                    ['x', '6040 Anne Lee', '9918 Ann Ray', '9685 Carl Fields', '7055 Janet Boyd', '6483 Stephen Freeman'],
                    ['Previous Year', 60, 45, 32, 76, 100],
                    ['Current Year', 75, 31, 88, 67, 33]
                ]
            });
        }
        IsMeanProcessTime = !IsMeanProcessTime;
    } else if (chartId == "#HighCostClaimsReport") {

        if (IsHighCostClaims) {
            HighCostClaimsChart.load({
                columns: [
                    ['x', 'P20160802C2 P singh', 'P20160801C6 Michal', 'P20160802C1 V manju', 'P20160801C7 Roy', 'P20160801C5 Joe'],
                    ['Cost', 455, 345, 233, 122, 34]
                ]
            });
        } else {
            HighCostClaimsChart.load({
                columns: [
                    ['x', 'P20160802C2 P singh', 'P20160801C7 Roy', 'P20160801C6 Michal', 'P20160802C1 V manju', 'P20160801C5 Joe'],
                   ['Cost', 643, 345, 123, 24, 10]
                ]
            });
        }
        IsHighCostClaims = !IsHighCostClaims;
    } else if (chartId == "#SpanTimeReport") {
        if (IsSpanTime) {
            SpanTimeChart.load({
                columns: [
                    ['x', '930449304 P Singh', '20412041 V Manju', '510285102 Roy', '660556605 Joe', '362733627 Heny'],
                     ['Current Year', 454, 324, 232, 43, 244]
                ],
            });
        } else {
            SpanTimeChart.load({
                columns: [
                   ['x', '930449304 P Singh', '20412041 V Manju', '510285102 Roy', '660556605 Joe', '362733627 Heny'],
                     ['Current Year', 231, 342, 234, 342, 322]
                ],
            });
        }
        IsSpanTime = !IsSpanTime;
    } else if (chartId == "#ProceduresClaimedReport") {
        if (IsProceduresClaimed) {
            $.ajax({
                url: "/ClaimsDashboard/GetProcedureClaimsReport?type=2",
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#ProceduresClaimedReport').html(result);
                }
            });
        } else {
            $.ajax({
                url: "/ClaimsDashboard/GetProcedureClaimsReport?type=1",
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#ProceduresClaimedReport').html(result);
                }
            });
        }
        IsProceduresClaimed = !IsProceduresClaimed;
    } else if (chartId == "#PenaltyAdjustmentReport") {
        if (IsPenaltyAdjustment) {
            $.ajax({
                url: "/ClaimsDashboard/GetAdjustmentCodeReport?type=2",
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#PenaltyAdjustmentReport').html(result);
                }
            });
        } else {
            $.ajax({
                url: "/ClaimsDashboard/GetAdjustmentCodeReport?type=1",
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#PenaltyAdjustmentReport').html(result);
                }
            });
        }
        IsPenaltyAdjustment = !IsPenaltyAdjustment;
    }
}

var IsType = true;

function ChangeDashboardDataDummy() {

    if (IsType) {
        MeanTimeChart.load({
            columns: [
                ['x', '6040 Anne Lee', '9918 Ann Ray', '9685 Carl Fields', '7055 Janet Boyd', '6483 Stephen Freeman'],
                ['Previous Year', 60, 200, 56, 12, 78],
                ['Current Year', 75, 31, 26, 90, 109]
            ]
        });
        HighCostClaimsChart.load({
            columns: [
                ['x', 'P20160802C2 P singh', 'P20160801C6 Michal', 'P20160802C1 V manju', 'P20160801C7 Roy', 'P20160801C5 Joe'],
                ['Cost', 455, 345, 233, 122, 34]
            ]
        });
        SpanTimeChart.load({
            columns: [
                ['x', '930449304 P Singh', '20412041 V Manju', '510285102 Roy', '660556605 Joe', '362733627 Heny'],
                 ['Current Year', 454, 324, 232, 43, 244]
            ],
        });
        $.ajax({
            url: "/ClaimsDashboard/GetProcedureClaimsReport?type=2",
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#ProceduresClaimedReport').html(result);
            }
        });
        $.ajax({
            url: "/ClaimsDashboard/GetAdjustmentCodeReport?type=2",
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#PenaltyAdjustmentReport').html(result);
            }
        });

        //$('#initiatedAmount').html('<small>$</small>845,895');
        //$('#initiatedCount').html('#23,452');

        //$('#AcceptedCHAmount').html('<small>$</small>43,342');
        //$('#AcceptedCHCount').html('#10,001');

        //$('#RejectedCHAmount').html('<small>$</small>543,345');
        //$('#RejectedCHCount').html('#44,322');

        //$('#AcceptedPayerAmount').html('<small>$</small>23,322');
        //$('#AcceptedPayerCount').html('#23,212');

        //$('#RejectedPayerAmount').html('<small>$</small>23,543');
        //$('#RejectedPayerCount').html('#432');

        ShowTileDataAmount('initiatedAmountNew', 8954127);

        ShowTileDataAmount('AcceptedCHAmountNew', 89562);

        ShowTileDataAmount('RejectedCHAmountNew', 7521);

        ShowTileDataAmount('AcceptedPayerAmountNew', 86352);

        ShowTileDataAmount('RejectedPayerAmountNew', 4744);

        ShowTileDataCount('initiatedCountNew', 758755);

        ShowTileDataCount('AcceptedCHCountNew', 75788);

        ShowTileDataCount('RejectedCHCountNew', 755);

        ShowTileDataCount('AcceptedPayerCountNew', 63525);

        ShowTileDataCount('RejectedPayerCountNew', 785);

        ShowTileDataPercentage('initiated_percent', 'initiated_progress', 26);

        ShowTileDataPercentage('acceptedCH_percent', 'acceptedCH_progress', 31);

        ShowTileDataPercentage('rejectedCH_percent', 'rejectedCH_progress', 4);

        ShowTileDataPercentage('acceptedPayer_percent', 'acceptedPayer_progress', 34);

        ShowTileDataPercentage('rejectedPayer_percent', 'rejectedPayer_progress', 5);


    } else {

        //$('#initiatedAmount').html('<small>$</small>325,654');
        //$('#initiatedCount').html('#32,543');

        //$('#AcceptedCHAmount').html('<small>$</small>43,342');
        //$('#AcceptedCHCount').html('#10,001');

        //$('#RejectedCHAmount').html('<small>$</small>23,002');
        //$('#RejectedCHCount').html('#22,232');

        //$('#AcceptedPayerAmount').html('<small>$</small>7,566');
        //$('#AcceptedPayerCount').html('#11,232');

        //$('#RejectedPayerAmount').html('<small>$</small>23,000');
        //$('#RejectedPayerCount').html('#452');
        ShowTileDataAmount('initiatedAmountNew', 512342);

        ShowTileDataAmount('AcceptedCHAmountNew', 45214);

        ShowTileDataAmount('RejectedCHAmountNew', 8660);

        ShowTileDataAmount('AcceptedPayerAmountNew', 35241);

        ShowTileDataAmount('RejectedPayerAmountNew', 1244);

        ShowTileDataCount('initiatedCountNew', 965244);

        ShowTileDataCount('AcceptedCHCountNew', 75415);

        ShowTileDataCount('RejectedCHCountNew', 577);

        ShowTileDataCount('AcceptedPayerCountNew', 75785);

        ShowTileDataCount('RejectedPayerCountNew', 544);

        ShowTileDataPercentage('initiated_percent', 'initiated_progress', 46);

        ShowTileDataPercentage('acceptedCH_percent', 'acceptedCH_progress', 35);

        ShowTileDataPercentage('rejectedCH_percent', 'rejectedCH_progress', 4);

        ShowTileDataPercentage('acceptedPayer_percent', 'acceptedPayer_progress', 15);

        ShowTileDataPercentage('rejectedPayer_percent', 'rejectedPayer_progress', 5);

        MeanTimeChart.load({
            columns: [
                ['x', '6040 Anne Lee', '9918 Ann Ray', '9685 Carl Fields', '7055 Janet Boyd', '6483 Stephen Freeman'],
                ['Previous Year', 60, 45, 32, 76, 100],
                ['Current Year', 75, 31, 88, 67, 33]
            ]
        });
        HighCostClaimsChart.load({
            columns: [
                ['x', 'P20160802C2 P singh', 'P20160801C7 Roy', 'P20160801C6 Michal', 'P20160802C1 V manju', 'P20160801C5 Joe'],
               ['Cost', 643, 345, 123, 24, 10]
            ]
        });
        SpanTimeChart.load({
            columns: [
               ['x', '930449304 P Singh', '20412041 V Manju', '510285102 Roy', '660556605 Joe', '362733627 Heny'],
                 ['Current Year', 231, 342, 234, 342, 322]
            ],
        });
        $.ajax({
            url: "/ClaimsDashboard/GetProcedureClaimsReport?type=1",
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#ProceduresClaimedReport').html(result);
            }
        });
        $.ajax({
            url: "/ClaimsDashboard/GetAdjustmentCodeReport?type=1",
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#PenaltyAdjustmentReport').html(result);
            }
        });
    }
    IsType = !IsType;
}

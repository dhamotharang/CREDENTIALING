
//--------------------Tracking tree drawer------------------------

function TrackingMapp(TrackerObject) {
    this.containerId = TrackerObject.containerID;
    this.container = $('#' + this.containerId);
    this.containerDimensions = this.CalculateWidthAndHeightOfConatiner();
    this.statusContainerMargin = 3;
    this.xWidthDivision = this.containerDimensions.width / TrackerObject.xBlocks;
    this.yHeightDivision = this.containerDimensions.height / TrackerObject.yBlocks;
    this.statusContainer = TrackerObject.statusContainer;
    this.DrawStatusBlock();
    this.ApplyClickEvent();
    this.DrawLines();
}

//-----------------calculate dimension---------------------
TrackingMapp.prototype.CalculateWidthAndHeightOfConatiner = function () {
    return {
        width: this.container.width(),
        height: this.container.height()
    }
}

//-------------------draw container----------------------
TrackingMapp.prototype.DrawStatusBlock = function () {
    var template;
    var currObject;
    var width = this.xWidthDivision - 2 * this.statusContainerMargin;
    var left;
    var top;
    for (var i = 0; i < this.statusContainer.length; i++) {
        currObject = this.statusContainer[i];
        left = this.xWidthDivision * currObject.x;
        top = this.yHeightDivision * currObject.y;
        template = '<div data-container="' + currObject.statusId + '" class="tracking_status_container ' + currObject.type + '_container" style="width:' + width + 'px;margin:' + this.statusContainerMargin + 'px;top:' + top + 'px;left:' + left + 'px">' +
            '<div class="status_title">' + currObject.text + '</div>' +
            '<div class="status_body clearfix">' +
            '<div class="pull-left type_' + currObject.category + '"></div>' +
            '<div class="pull-right"><div class="status_amount">' +
            '<small>$</small>' + currObject.amount + '</div>' +
            '<div class="status_count"><small>#</small>' + currObject.count + '</div></div></div></div>';
        this.container.append(template);
    }

}

//--------------------- apply click event to status container---------------
TrackingMapp.prototype.ApplyClickEvent = function () {
    var self = this;
    self.container.find('.tracking_status_container').on('click', function () {
        var statusId = $(this).attr('data-container');
        self.GetClaimsList(statusId, self);
        $('.active_tracking_status_container').removeClass('active_tracking_status_container');
        $(this).addClass('active_tracking_status_container');

    });

}


//----------------------------get claims list------------------------------

TrackingMapp.prototype.GetClaimsList = function (StatusId, self) {
    $.ajax({
        type: 'GET',
        url: '/Billing/ClaimsTracking/GetTrakingClaimList?status=' + StatusId,
        success: function (data) {
            var template = '<div class="x_panel border_on_panel"><div class="x_content"><div class="clearfix"><label class="title_heading pull-left" id="claims_list_title"></label><div class="pull-right"><a data-placement="bottom" class="export-button pull-right" data-toggle="tooltip" href="javascript:void(0)" title="" data-original-title="Print"><img src="/Helper/Resources/Images/print.png"></a><a data-placement="bottom" class="export-button pull-right" data-toggle="tooltip" href="javascript:void(0)" title="" data-original-title="Save As PDF"><img src="/Helper/Resources/Images/pdf.png"></a><a data-placement="bottom" class="export-button pull-right" data-toggle="tooltip" href="javascript:void(0)" title="" data-original-title="Save As Excel"><img src="/Helper/Resources/Images/excel.png"></a><a data-placement="bottom" class="export-button pull-right" data-toggle="tooltip" href="javascript:void(0)" title="" data-original-title="Filter"><img src="/Helper/Resources/Images/filter.png"></a></div></div>';
            template = template + data + '</div></div>';
            $('#claims_list').html(template);
            for (var j = 0; j < self.statusContainer.length; j++) {
                var currObject = self.statusContainer[j];
                if (StatusId == currObject.statusId) {
                    $('#claims_list_title').addClass('title_' + currObject.type);
                    $('#claims_list_title').html(currObject.fullTitle + ' Claims' + ' (<small>$</small>' + currObject.amount + '  &nbsp;<small>#</small>' + currObject.count + ')');
                    break;
                }
            }
        }
    });
}

//----------------------------------draw line------------------------------

TrackingMapp.prototype.DrawLines = function () {

    var currObject;
    for (var i = 0; i < this.statusContainer.length; i++) {
        currObject = this.statusContainer[i];
        if (currObject.connectTo) {
            for (var j = 0; j < currObject.connectTo.length; j++) {
                this.container.append(DrawLine(currObject, this.statusContainer[currObject.connectTo[j]], this.xWidthDivision, this.yHeightDivision, this.statusContainerMargin));
            }
        }
        
    }


    //----------------------draw single line------------------------
    function DrawLine(fromObject, toObject, width, height, margin) {
        var lineTemplate;
        var typeOfLine = GetTypeOfLine(fromObject.y);
        if (typeOfLine === 'YFirstLine') {
            lineTemplate = DrawYFirstLine(fromObject, toObject, width, height, margin);
        } else if (typeOfLine === 'XFirstLine') {
            lineTemplate = DrawXFirstLine(fromObject, toObject, width, height, margin);
        }
        return lineTemplate;
    }


    //--------------------get type of line---------------------
    function GetTypeOfLine(y1) {
        var result;
        if (y1 === 0) {
            result = 'YFirstLine';
        } else if (y1 === 1 || y1 === 2) {
            result = 'XFirstLine';
        }
        return result;
    }

    //-------------------draw YFirst line-------------------
    function DrawYFirstLine(fromObject, toObject, width, height, margin) {
        var firstLineTemplate = DrawLineByCoordinates(GetCoordinates(fromObject.x, width, margin), GetCoordinates(fromObject.y, height, margin), GetCoordinates(fromObject.x, width, margin), GetCoordinates(toObject.y, height, margin));
        var secondLineTemplate = DrawLineByCoordinates(GetCoordinates(fromObject.x, width, margin), GetCoordinates(toObject.y, height, margin), GetCoordinates(toObject.x, width, margin), GetCoordinates(toObject.y, height, margin));

        return firstLineTemplate + secondLineTemplate;
    }

    //-------------------draw XFirst line-------------------
    function DrawXFirstLine(fromObject, toObject, width, height, margin) {
        var firstLineTemplate = DrawLineByCoordinates(GetCoordinates(fromObject.x, width, margin), GetCoordinates(fromObject.y, height, margin), GetCoordinates(toObject.x, width, margin), GetCoordinates(fromObject.y, height, margin));
        var secondLineTemplate = DrawLineByCoordinates(GetCoordinates(toObject.x, width, margin), GetCoordinates(fromObject.y, height, margin), GetCoordinates(toObject.x, width, margin), GetCoordinates(toObject.y, height, margin));

        return firstLineTemplate + secondLineTemplate;
    }

    //------------------convert point to coordinates-----------

    function GetCoordinates(number, block, margin) {
        return (number * block) + (block / 2) - 2 * margin;
    }

    function DrawLineByCoordinates(x1, y1, x2, y2) {
        var angle = Math.atan2(y2 - y1, x2 - x1) * (180 / Math.PI);
        var length = Math.sqrt(Math.pow((y2 - y1), 2) + Math.pow((x2 - x1), 2));
        var lineTemplate = '<div class="connection-line" style="left:' + x1 + 'px;top:' + y1 + 'px;transform:rotate(' + angle + 'deg);width:' + length + 'px"></div>';
        return lineTemplate;
    }

}


//-----------Creating service repository class which handles all ajax calls------------------------------
var JQueryServiceRepository = function () { };


//----------------- GET method-----------------------------------------
JQueryServiceRepository.prototype.GetDataFromService = function (url) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'GET',
        url: url,
        processData: false,
        contentType: false,
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}


//----------------- POST method-----------------------------------------
JQueryServiceRepository.prototype.PostDataToService = function (url, data) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}


//----------------- GET JSON DATA method-----------------------------------------
JQueryServiceRepository.prototype.GETJSONDataToService = function (url, data) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'GET',
        url: url,
        data: data,
        processData: false,
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}


//----------------- POST JSON DATA method-----------------------------------------
JQueryServiceRepository.prototype.PostJSONDataToService = function (url, data) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
        processData: false,
        contentType: "application/json",
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}



//----------------- GET JSON DATA method-----------------------------------------
JQueryServiceRepository.prototype.GetMultipleRequestToService = function (data) {

}

//----------------- POST JSON DATA method-----------------------------------------
JQueryServiceRepository.prototype.PostMultipleRequestToService = function (data) {

}




//-----------------Generic Web Workers --------------------------------------------

JQueryServiceRepository.prototype.AjaxWebWorker = function (URL, SingleParam, SearchTerm) {
    var def = $.Deferred(function (dfd) {
        if (window.Worker) {
            var worker = new Worker('');
            worker.addEventListener('message', function (event) {
                def.resolve(JSON.parse(event.data));
            }, false);

            worker.addEventListener('error', function (event) {
                def.reject(item);
            }, false);

            worker.postMessage({
                url: URL,
                singleParam: SingleParam,
                searchTerm: SearchTerm,
                Header: Header
            });
        } else {
            setTimeout(function () {
                try {
                    var result = action(args);
                    dfd.resolve(result);
                } catch (e) {
                    dfd.reject(e);
                }
            }, 1000);
        }
    });
    return def.promise();
};



